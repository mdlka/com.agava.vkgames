using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;

namespace Agava.VKGames
{
    public static class Storage
    {
        private static Action s_onSetCloudSaveDataSuccessCallback;
        private static Action s_onSetCloudSaveDataErrorCallback;

        private static Action<string> s_onGetCloudSaveDataSuccessCallback;
        private static Action s_onGetCloudSaveDataErrorCallback;

        private static Action s_onGetDictionaryCloudSaveDataErrorCallback;
        private static Action<IReadOnlyDictionary<string, string>> s_onGetDictionaryCloudSaveDataSuccessCallback;

        private static Action<string[]> s_onGetKeysSuccessCallback;
        private static Action s_onGetKeysErrorCallback;
        
        public static void SetCloudSaveData(string key, string value, Action onSuccessCallback = null, Action onErrorCallback = null)
        {
            s_onSetCloudSaveDataSuccessCallback = onSuccessCallback;
            s_onSetCloudSaveDataErrorCallback = onErrorCallback;

            StorageSetCloudSaveData(key, value, OnSetCloudSaveDataSuccessCallback, OnSetCloudSaveDataErrorCallback);
        }
        
        [DllImport("__Internal")]
        private static extern void StorageSetCloudSaveData(string key, string value, Action successCallback, Action errorCallback);
        
        [MonoPInvokeCallback(typeof(Action))]
        private static void OnSetCloudSaveDataSuccessCallback()
        {
            s_onSetCloudSaveDataSuccessCallback?.Invoke();
        }
        
        [MonoPInvokeCallback(typeof(Action))]
        private static void OnSetCloudSaveDataErrorCallback()
        {
            s_onSetCloudSaveDataErrorCallback?.Invoke();
        }
        
        public static void GetCloudSaveData(string key, Action<string> onSuccessCallback, Action onErrorCallback = null)
        {
            s_onGetCloudSaveDataSuccessCallback = onSuccessCallback;
            s_onGetCloudSaveDataErrorCallback = onErrorCallback;
            
            string jsonKey = JsonUtility.ToJson(new StorageKeys { keys = new[] { key } });

            StorageGetCloudSaveData(jsonKey, OnGetCloudSaveDataSuccessCallback, OnGetCloudSaveDataErrorCallback);
        }

        public static void GetDictionaryCloudSaveData(string[] keys, Action<IReadOnlyDictionary<string, string>> onSuccessCallback, Action onErrorCallback = null)
        {
            s_onGetDictionaryCloudSaveDataSuccessCallback = onSuccessCallback;
            s_onGetDictionaryCloudSaveDataErrorCallback = onErrorCallback;
            
            string jsonKeys = JsonUtility.ToJson(new StorageKeys { keys = keys} );
            
            StorageGetCloudSaveData(jsonKeys, OnGetDictionaryCloudSaveDataSuccessCallback, OnGetDictionaryCloudSaveDataErrorCallback);
        }
        
        [DllImport("__Internal")]
        private static extern void StorageGetCloudSaveData(string key, Action<string> successCallback, Action errorCallback);
        
        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnGetCloudSaveDataSuccessCallback(string value)
        {
            string cloudSaveData = JsonUtility.FromJson<StorageValues>(value).keys[0].value;
            
            s_onGetCloudSaveDataSuccessCallback?.Invoke(cloudSaveData);
        }
        
        [MonoPInvokeCallback(typeof(Action))]
        private static void OnGetCloudSaveDataErrorCallback()
        {
            s_onGetCloudSaveDataErrorCallback?.Invoke();
        }
        
        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnGetDictionaryCloudSaveDataSuccessCallback(string value)
        {
            Dictionary<string, string> cloudSaveData = JsonUtility.FromJson<StorageValues>(value).keys
                .ToDictionary(pair => pair.key, pair => pair.value);
            
            s_onGetDictionaryCloudSaveDataSuccessCallback?.Invoke(cloudSaveData);
        }
        
        [MonoPInvokeCallback(typeof(Action))]
        private static void OnGetDictionaryCloudSaveDataErrorCallback()
        {
            s_onGetDictionaryCloudSaveDataErrorCallback?.Invoke();
        }
        
        public static void GetKeys(int count, int offset, Action<string[]> onSuccessCallback, Action onErrorCallback = null)
        {
            s_onGetKeysSuccessCallback = onSuccessCallback;
            s_onGetKeysErrorCallback = onErrorCallback;
            
            StorageGetKeys(count, offset, OnGetKeysSuccessCallback, OnGetKeysErrorCallback);
        }
        
        [DllImport("__Internal")]
        private static extern void StorageGetKeys(int amount, int offset, Action<string> successCallback, Action errorCallback);

        [MonoPInvokeCallback(typeof(Action))]
        private static void OnGetKeysErrorCallback()
        {
            s_onGetKeysErrorCallback?.Invoke();
        }
        
        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnGetKeysSuccessCallback(string jsonKeys)
        {
            string[] keys = JsonUtility.FromJson<StorageKeys>(jsonKeys).keys;
            
            s_onGetKeysSuccessCallback?.Invoke(keys);
        }
    }
}