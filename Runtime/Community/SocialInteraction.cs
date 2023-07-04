using System.Runtime.InteropServices;
using System;
using AOT;

namespace Agava.VKGames
{
    public static class SocialInteraction
    {
        private static Action s_onSuccessCallback;
        private static Action s_onErrorCallback;

        public static void InviteFriends(Action onSuccessCallback = null, Action onErrorCallback = null)
        {
            s_onSuccessCallback = onSuccessCallback;
            s_onErrorCallback = onErrorCallback;

            ShowInviteBox(OnSuccessCallback, OnErrorCallback);
        }
        
        [DllImport("__Internal")]
        private static extern void ShowInviteBox(Action successCallback, Action errorCallback);

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


