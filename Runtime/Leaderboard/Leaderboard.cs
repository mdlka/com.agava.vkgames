using System.Runtime.InteropServices;
using System;
using AOT;

namespace Agava.VKGames
{
    public static class Leaderboard
    {
        [DllImport("__Internal")]
        private static extern void ShowLeaderboardBox(int playerScore, Action onErrorCallback);

        private static Action s_onErrorCallback;

        public static void ShowLeaderboard(int playerScore = 0, Action onErrorCallback = null)
        {
            s_onErrorCallback = onErrorCallback;

            ShowLeaderboardBox(playerScore, OnErrorCallback);
        }

        [MonoPInvokeCallback(typeof(Action))]
        private static void OnErrorCallback()
        {
            s_onErrorCallback?.Invoke();
        }
    }

}
