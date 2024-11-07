using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lim.ScriptableObjects
{
    public enum SceneType
    {
        None = 0,
        Title,
        Main,
        Loading,
        InGame,
        ETC,
        
    }
    
    [CreateAssetMenu(menuName = "ScriptableObject/Data/Scene")]
    public class SceneInfo : ScriptableObject
    {
        public SceneType sceneType;
        public string sceneName;
        public int sceneIndex;

        
        
#if UNITY_EDITOR
        public UnityEditor.SceneAsset sceneAsset;

        public void OnValidate()
        {
            if (sceneAsset == null) return;

            var path = UnityEditor.AssetDatabase.GetAssetPath(sceneAsset);
            var index = SceneUtility.GetBuildIndexByScenePath(path);

            sceneName = sceneAsset.name;
            sceneIndex = index;
            
            UnityEditor.EditorUtility.SetDirty(this);
        }
#endif

    }
}