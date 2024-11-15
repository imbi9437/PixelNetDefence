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
        private const float ProgressAsyncValue = 0.9f;
        private const float SceneChangeDelay = 2.0f;

        private Dictionary<SceneType, List<SceneInfo>> _sceneDic = new Dictionary<SceneType, List<SceneInfo>>();
        private SceneInfo loadingSceneInfo;

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
                _sceneDic.TryAdd((SceneType)value, new List<SceneInfo>());
            }

            for (int i = 0; i < SceneInfos.Count; i++)
            {
                _sceneDic[SceneInfos[i].sceneType].Add(SceneInfos[i]);
            }

            loadingSceneInfo = _sceneDic[SceneType.Loading].First();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ChangeSceneWithLoading(SceneInfos[0]).Forget();
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                ChangeSceneWithLoading(SceneInfos[1]).Forget();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                ChangeSceneWithLoading(SceneInfos[2]).Forget();
            }
        }

        private async UniTaskVoid ChangeSceneWithLoading(SceneInfo info)
        {
            await SceneManager.LoadSceneAsync(info.sceneIndex).ToUniTask();
        }

        private async UniTaskVoid ChangeSceneWithoutLoading()
        {
            
        }
    }
}