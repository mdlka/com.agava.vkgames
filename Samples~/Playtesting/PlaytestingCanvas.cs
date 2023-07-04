using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Agava.VKGames.Samples.Playtesting
{
    public class PlaytestingCanvas : MonoBehaviour
    {
        private const string DataSaveKey = "DataSaveKey";
        
        [SerializeField] private InputField _userDataInputField;
        
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
            SocialInteraction.InviteFriends(onSuccessCallback: () => Debug.Log("Friends invited"));
        }

        public void InviteToCommunityButtonClick()
        {
            Community.InviteToGroup(onSuccessCallback: () => Debug.Log("Added to community"));
        }

        public void ShowLeaderboardButtonClick()
        {
            Leaderboard.ShowLeaderboard(100);
        }

        public void OnGetUserDataButtonClick()
        {
            Storage.GetCloudSaveData(DataSaveKey, onSuccessCallback: value => _userDataInputField.text = value);
        }

        public void OnSetUserDataButtonClick()
        {
            Storage.SetCloudSaveData(DataSaveKey, _userDataInputField.text);
        }
    }
}
