using System.Runtime.InteropServices;
using System;
using AOT;

namespace Agava.VKGames
{
    public static class Interstitial
    {
        private static Action s_onOpenCallback;
        private static Action s_onErrorCallback;

        public static void Show(Action onOpenCallback = null, Action onErrorCallback = null)
        {
            s_onOpenCallback = onOpenCallback;
            s_onErrorCallback = onErrorCallback;

            ShowInterstitialAds(OnSuccessCallback, OnErrorCallback);
        }
        
        [DllImport("__Internal")]
        private static extern void ShowInterstitialAds(Action openCallback, Action errorCallback);

        [MonoPInvokeCallback(typeof(Action))]
        private static void OnSuccessCallback()
        {
            s_onOpenCallback?.Invoke();
        }

        [MonoPInvokeCallback(typeof(Action))]
        private static void OnErrorCallback()
        {
            s_onErrorCallback?.Invoke();
        }
    }
}