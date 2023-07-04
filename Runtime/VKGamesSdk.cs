using System;
using AOT;
using System.Runtime.InteropServices;
using System.Collections;

namespace Agava.VKGames
{
    public static class VKGamesSdk
    {
        private static Action s_onSuccessCallback;
        private static Action s_onErrorCallback;
        
        public static bool Initialized => IsInitialized();
        
        [DllImport("__Internal")]
        private static extern bool IsInitialized();

        public static IEnumerator Initialize(Action onSuccessCallback = null, Action onErrorCallback = null, bool isTest = false)
        {
            s_onSuccessCallback = onSuccessCallback;
            s_onErrorCallback = onErrorCallback;

            WebAppInit(OnSuccessCallback, OnErrorCallback, isTest);

            while (!Initialized)
                yield return null;
        }
        
        [DllImport("__Internal")]
        private static extern void WebAppInit(Action successCallback, Action errorCallback, bool isTest);

        [MonoPInvokeCallback(typeof(Action))]
        private static void OnSuccessCallback()
        {
            s_onSuccessCallback?.Invoke();
        }

        [MonoPInvokeCallback(typeof(Action))]
        private static void OnErrorCallback()
        {
            s_onErrorCallback?.Invoke();
        }
    }
}

