using System;
using System.Collections;
using System.Collections.Generic;
using Lim.Interface;
using UnityEngine;

namespace Lim.Generic
{
    public abstract class MonoSingleton<T> : MonoBehaviour, IEventHandler where T : MonoBehaviour
    {
        private static T _instance;
        private static object _lock = new object();
        private static bool isApplicationQuit = false;
        public bool isDontDestroy = false;

        public static T Instance
        {
            get
            {
                lock (_lock)
                {
                    if (isApplicationQuit) return null;

                    if (_instance == false) _instance = FindObjectOfType<T>();
                    if (_instance != false) return _instance;

                    var obj = new GameObject(typeof(T).Name);
                    _instance = obj.AddComponent<T>();
                    return _instance;
                }
            }
        }

        private void Awake()
        {
            if (_instance) Destroy(this as T);
            else _instance = this as T;

            if (isDontDestroy) DontDestroyOnLoad(gameObject);
        }

        private void OnApplicationQuit()
        {
            isApplicationQuit = true;
        }

        public void ExecuteEvent<T1>(object sender, EventHandler<T1> handler, T1 args) where T1 : EventArgs
        {
            throw new NotImplementedException();
        }
    }
}