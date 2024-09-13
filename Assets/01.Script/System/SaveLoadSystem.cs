using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using Lim.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Lim.System
{
    public class SaveLoadSystem : MonoSingleton<SaveLoadSystem>
    {
        public class Events
        {
            public EventHandler OnSaveUserDataComplete;
            public EventHandler OnSaveUserDataError;
            public EventHandler<LoadUserDataArgs> OnLoadUserDataComplete;
            public EventHandler OnLoadUserDataError;
        }
        
        private const string SettingFileName = "Settings.json";
        private const string UserDataFileName = "UserData.json";
        private string _directory;

        public UserData Data;

        public static Events SubScribe = new Events();

        protected override void Awake()
        {
            base.Awake();
            _directory = $"{Application.persistentDataPath}/Saves";
        }

        private void OnEnable()
        {
            SubScribe.OnSaveUserDataComplete += (sender, args) => Debug.Log("Save Complete");
            SubScribe.OnLoadUserDataComplete += (sender, args) =>
            {
                Data = args.Data;
                Debug.Log("Load Complete");
            };
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SaveUserData(Data).Forget();
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                LoadUserData().Forget();
            }
        }

        private async UniTaskVoid SaveUserData(UserData data)
        {
            CheckDirectory();
            
            try
            {
                var json = JsonConvert.SerializeObject(data);
                var path = Path.Combine(_directory, UserDataFileName);
            
                await File.WriteAllTextAsync(path,json);
                
                ExecuteEvent(this,SubScribe.OnSaveUserDataComplete);
            }
            catch (Exception e)
            {
                Debug.LogError($"Save UserData Issue : {e}");
                ExecuteEvent(this, SubScribe.OnSaveUserDataError);
            }
        }

        private async UniTaskVoid LoadUserData()
        {
            try
            {
                var path = Path.Combine(_directory, UserDataFileName);

                if (File.Exists(path))
                {
                    var json = await File.ReadAllTextAsync(path);
                    var data = JsonConvert.DeserializeObject<UserData>(json);

                    LoadUserDataArgs args = new LoadUserDataArgs() { Data = data };

                    ExecuteEvent(this, SubScribe.OnLoadUserDataComplete, args);
                }
            }
            catch (Exception e)
            {
                Debug.Log($"Load UserData Issue : {e}");
                ExecuteEvent(this,SubScribe.OnLoadUserDataError);
            }
        }

        private void CheckDirectory()
        {
            if (Directory.Exists(_directory)) return;
            Directory.CreateDirectory(_directory);
        }
    }
    
    [Serializable]
    public class UserData
    {
        public string name;
        public int level;
    }

    [Serializable]
    public class SettingData
    {
        
    }

    public class LoadUserDataArgs : EventArgs
    {
        public UserData Data;
    }

    public class LoadSettingDataArgs : EventArgs
    {
        public SettingData Data;
    }
}