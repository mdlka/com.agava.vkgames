using System.Runtime.InteropServices;
using System;
using AOT;

namespace Agava.VKGames
{
    public static class Community
    {
        private static Action s_onSuccessCallback;
        private static Action s_onErrorCallback;

        public static void InviteToGroup(long groupId = 84861196, Action onSuccessCallback = null, Action onErrorCallback = null)
        {
            s_onSuccessCallback = onSuccessCallback;
            s_onErrorCallback = onErrorCallback;

            JoinGroup(groupId, OnSuccessCallback, OnErrorCallback);
        }
        
        [DllImport("__Internal")]
        private static extern void JoinGroup(long groupId, Action successCallback, Action errorCallback);

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

