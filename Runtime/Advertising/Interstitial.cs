using System.Runtime.InteropServices;
using System;
using AOT;

namespace Agava.VKGames
{
    public static class Interstitial
    {
        [DllImport("__Internal")]
        private static extern void ShowInterstitialAds(Action onOpenCallback, Action onErrorCallback);

        private static Action s_onOpenCallback;
        private static Action s_onErrorCallback;

        public static void Show(Action onOpenCallback = null, Action onErrorCallback = null)
        {
            s_onOpenCallback = onOpenCallback;
            s_onErrorCallback = onErrorCallback;

            ShowInterstitialAds(OnSuccessCallback, OnErrorCallback);
        }

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