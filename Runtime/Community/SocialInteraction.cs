using System.Runtime.InteropServices;
using System;
using AOT;

namespace Agava.VKGames
{
    public static class SocialInteraction
    {
        [DllImport("__Internal")]
        private static extern void ShowInviteBox(Action onSuccessCallback, Action onErrorCallback);

        private static Action s_onRewardedCallback;
        private static Action s_onErrorCallback;

        public static void InviteFriends(Action onRewardedCallback = null, Action onErrorCallback = null)
        {
            s_onRewardedCallback = onRewardedCallback;
            s_onErrorCallback = onErrorCallback;

            ShowInviteBox(OnSuccessCallback, OnErrorCallback);
        }

        [MonoPInvokeCallback(typeof(Action))]
        private static void OnSuccessCallback()
        {
            s_onRewardedCallback?.Invoke();
        }

        [MonoPInvokeCallback(typeof(Action))]
        private static void OnErrorCallback()
        {
            s_onErrorCallback?.Invoke();
        }
    }
}


