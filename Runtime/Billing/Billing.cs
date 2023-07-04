using System;
using System.Runtime.InteropServices;
using AOT;

namespace Agava.VKGames
{
    public static class Billing
    {
        private static Action s_onSuccessCallback;
        private static Action s_onErrorCallback;

        public static void PurchaseItem(string itemId, Action onSuccessCallback = null, Action onErrorCallback = null)
        {
            s_onSuccessCallback = onSuccessCallback;
            s_onErrorCallback = onErrorCallback;

            ShowOrderBox(itemId, OnSuccessCallback, OnErrorCallback);
        }
        
        [DllImport("__Internal")]
        private static extern void ShowOrderBox(string itemId, Action successCallback, Action errorCallback);

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
