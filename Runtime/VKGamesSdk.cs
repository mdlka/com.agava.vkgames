using System;
using AOT;
using System.Runtime.InteropServices;
using System.Collections;

namespace Agava.VKGames
{
    public static class VKGamesSdk
    {
        [DllImport("__Internal")]
        private static extern void WebAppInit(Action onSuccessCallback, Action onErrorCallback, bool isTest);
        [DllImport("__Internal")]
        private static extern bool IsInitialized();

        public static bool Initialized => IsInitialized();

        private static Action s_onSuccessCallback;
        private static Action s_onErrorCallback;

        public static IEnumerator Initialize(Action onSuccessCallback = null, Action onErrorCallback = null, bool isTest = false)
        {
            s_onSuccessCallback = onSuccessCallback;
            s_onErrorCallback = onErrorCallback;

            WebAppInit(OnSuccessCallback, OnErrorCallback, isTest);

            while (!Initialized)
                yield return null;
        }

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

