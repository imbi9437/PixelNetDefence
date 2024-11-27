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
    public partial class SaveLoadSystem : MonoSingleton<SaveLoadSystem>
    {
        public class Events
        {
            public EventHandler OnSaveUserDataComplete;
            public EventHandler OnSaveUserDataError;
            public EventHandler<LoadUserDataArgs> OnLoadUserDataComplete;
            public EventHandler OnLoadUserDataError;
            
            public EventHandler OnSaveSettingDataComplete;
            public EventHandler OnSaveSettingDataError;
            public EventHandler<LoadSettingDataArgs> OnLoadSettingDataComplete;
            public EventHandler OnLoadSettingDataError;
        }
        
        private const string SettingFileName = "Settings.json";
        private const string UserDataFileName = "UserData.json";
        private string _directory;
        
        public static Events SubScribe = new Events();
        
        protected override void Awake()
        {
            base.Awake();
            _directory = $"{Application.persistentDataPath}/Saves";
        }
        
        

        #region Save Function

        public void SaveUserData(UserData data) => SaveUserDataAsync(data).Forget();
        public void SaveSettingData(SettingData data) => SaveSettingDataAsync(data).Forget();
        
        
        private async UniTaskVoid SaveUserDataAsync(UserData data)
        {
            CheckDirectory();
            
            try
            {
                var json = JsonConvert.SerializeObject(data);
                var encryptJson = EncryptAES.Encrypt(json);
                var path = Path.Combine(_directory, UserDataFileName);
            
                await File.WriteAllBytesAsync(path,encryptJson);
                
                ExecuteEvent(this,SubScribe.OnSaveUserDataComplete);
            }
            catch (Exception e)
            {
                Debug.LogError($"Save UserData Issue : {e}");
                ExecuteEvent(this, SubScribe.OnSaveUserDataError);
            }
        }
        private async UniTaskVoid SaveSettingDataAsync(SettingData data)
        {
            CheckDirectory();
            
            try
            {
                var json = JsonConvert.SerializeObject(data);
                var path = Path.Combine(_directory, SettingFileName);
            
                await File.WriteAllTextAsync(path,json);
                
                ExecuteEvent(this,SubScribe.OnSaveSettingDataComplete);
            }
            catch (Exception e)
            {
                Debug.LogError($"Save SettingData Issue : {e}");
                ExecuteEvent(this, SubScribe.OnSaveSettingDataError);
            }
        }

        #endregion

        #region Load Function

        public void LoadUserData() => LoadUserDataAsync().Forget();
        public void LoadSettingData() => LoadSettingDataAsync().Forget();

        
        private async UniTaskVoid LoadUserDataAsync()
        {
            try
            {
                var path = Path.Combine(_directory, UserDataFileName);
                
                if (File.Exists(path))
                {
                    var json = await File.ReadAllBytesAsync(path);
                    var decryptJson = EncryptAES.Decrypt(json);
                    var data = JsonConvert.DeserializeObject<UserData>(decryptJson);

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
        private async UniTaskVoid LoadSettingDataAsync()
        {
            try
            {
                var path = Path.Combine(_directory, SettingFileName);

                if (File.Exists(path))
                {
                    var json = await File.ReadAllTextAsync(path);
                    var data = JsonConvert.DeserializeObject<SettingData>(json);
                    
                    LoadSettingDataArgs args = new LoadSettingDataArgs() { Data = data };

                    ExecuteEvent(this, SubScribe.OnLoadSettingDataComplete, args);
                }
            }
            catch (Exception e)
            {
                Debug.Log($"Load SettingData Issue : {e}");
                ExecuteEvent(this,SubScribe.OnLoadSettingDataError);
            }
        }
        
        #endregion

        #region Initialzed Function

        public void Initialize()
        {
            CheckDirectory();
            CheckFile();
            
            LoadUserData();
            LoadSettingData();
        }

        #endregion

        #region Generic Function

        private void CheckDirectory()
        {
            if (Directory.Exists(_directory)) return;
            Directory.CreateDirectory(_directory);
        }

        private void CheckFile()
        {
            var userPath = Path.Combine(_directory, UserDataFileName);
            var settingPath = Path.Combine(_directory, SettingFileName);

            if (File.Exists(userPath) == false)
            {
                var data = new UserData();
                SaveUserData(data);
            }

            if (File.Exists(settingPath) == false)
            {
                var data = new SettingData();
                SaveSettingData(data);
            }
        }

        #endregion
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