using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Lim.Generic;
using Lim.ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lim.System
{
    public class SceneSystem : MonoSingleton<SceneSystem>
    {
        // 씬 개수가 많지 않아 굳이 Dictionary로 구분 안해도 될듯 추후 씬 추가 되면 그때 하는 걸로
        private Dictionary<SceneType, SceneInfo> _sceneDic = new Dictionary<SceneType, SceneInfo>();
        private SceneType _currentType;

        [SerializeField] private List<SceneInfo> SceneInfos;
        
        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _sceneDic.Clear();

            foreach (var value in Enum.GetValues(typeof(SceneType)))
            {
                _sceneDic.TryAdd((SceneType)value, null);
            }

            foreach (var info in SceneInfos)
            {
                if (_sceneDic[info.sceneType] != null)
                {
                    Debug.LogError("There is Same Type Scene");
                }
                else
                {
                    _sceneDic[info.sceneType] = info;
                }
            }

            _currentType = SceneType.Title;

            SceneManager.sceneLoaded += (arg0, mode) => Debug.Log(arg0.name);
        }

        /// <summary>로딩 씬 이동 없이 바로 이동</summary>
        private async UniTask ChangeScene(SceneType type)
        {
            var operation = SceneManager.LoadSceneAsync(_sceneDic[type].sceneIndex);
            operation.allowSceneActivation = false;

            await UniTask.WaitUntil(() => operation.progress >= 0.9f);

            operation.allowSceneActivation = true;
            
            _currentType = type;
        }
        
        /// <summary>로딩 씬 포함 딜레이 설정 필요</summary>
        private async UniTaskVoid ChangeScene(SceneType type,int time)
        {
            await SceneManager.LoadSceneAsync(_sceneDic[SceneType.Loading].sceneIndex).ToUniTask();

            _currentType = SceneType.Loading;

            var operation = SceneManager.LoadSceneAsync(_sceneDic[type].sceneIndex);
            operation.allowSceneActivation = false;

            var changeTask = UniTask.WaitUntil(() => operation.progress >= 0.9f);
            var timeTask = UniTask.Delay(time * 1000);
            //var changeTask = ChangeScene(type);

            // 이쪽 부분에서 최소 딜레이 설정 안됨
            // 아마 AllowSceneActivation이 True라서 바로 이동하는듯
            // 해결방안 찾기 전까지 함부 재사용 불가
            await UniTask.WhenAll(changeTask, timeTask);

            operation.allowSceneActivation = true;

            _currentType = type;
        }
    }
}