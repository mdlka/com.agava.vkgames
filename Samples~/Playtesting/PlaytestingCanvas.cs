using UnityEngine;
using System.Collections;

namespace Agava.VKGames.Samples.Playtesting
{
    public class PlaytestingCanvas : MonoBehaviour
    {
        private IEnumerator Start()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            yield break;
#endif

            yield return VKGamesSdk.Initialize(onSuccessCallback: () => Debug.Log($"Initialized: {VKGamesSdk.Initialized}"));
        }

        public void ShowInterstitialButtonClick()
        {
            Interstitial.Show(onOpenCallback: () => Debug.Log("Interstitial showed"));
        }

        public void ShowRewardedAdsButtonClick()
        {
            VideoAd.Show(onRewardedCallback: () => Debug.Log("RewardedAd showed"));
        }

        public void InviteFriendsButtonClick()
        {
            SocialInteraction.InviteFriends(onRewardedCallback: () => Debug.Log("Friends invited"));
        }

        public void InviteToCommunityButtonClick()
        {
            Community.InviteToIJuniorGroup(onRewardedCallback: () => Debug.Log("Added to community"));
        }

        public void ShowLeaderboardButtonClick()
        {
            Leaderboard.ShowLeaderboard(100);
        }
    }
}
