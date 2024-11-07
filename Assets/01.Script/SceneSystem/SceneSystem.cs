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

        public class Event
        {
            public EventHandler<SceneChangeArgs> OnStartLoad;
            public EventHandler<SceneChangeArgs> OnLoading;
            public EventHandler<SceneChangeArgs> OnCompleteLoad;
        }

        private Event Subscribe = new Event();
        private SceneChangeArgs EventArgsCache = new SceneChangeArgs();

        private Dictionary<SceneType, List<SceneInfo>> _sceneDic = new Dictionary<SceneType, List<SceneInfo>>();
        private SceneInfo loadingSceneInfo;

        [SerializeField] private List<SceneInfo> SceneInfo;

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

            for (int i = 0; i < SceneInfo.Count; i++)
            {
                _sceneDic[SceneInfo[i].sceneType].Add(SceneInfo[i]);
            }

            loadingSceneInfo = _sceneDic[SceneType.Loading].First();

            EventArgsCache.CurScene =
                SceneInfo.Find(info => info.sceneIndex == SceneManager.GetActiveScene().buildIndex);

            Subscribe.OnCompleteLoad += (sender, args) =>
            {
                EventArgsCache.PrevScene = EventArgsCache.CurScene;
                EventArgsCache.CurScene = args.TargetScene;
                EventArgsCache.TargetScene = null;
            };
        }
    }

    public class SceneChangeArgs : EventArgs
    {
        public SceneInfo PrevScene;
        public SceneInfo CurScene;
        public SceneInfo TargetScene;
    }
}