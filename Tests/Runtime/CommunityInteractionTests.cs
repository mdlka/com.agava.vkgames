using Agava.VKGames;
using System.Collections;
using UnityEngine.TestTools;
using UnityEngine;
using NUnit.Framework;

namespace VKGames.Tests
{
    public class CommunityInteractionTests
    {
        [UnitySetUp]
        public IEnumerator WaitForInitialization()
        {
            if (!VKGamesSdk.Initialized)
                yield return VKGamesSdk.Initialize(isTest: true);
        }

        [UnityTest]
        public IEnumerator ShowShouldInvokeErrorCallbackInviteFriends()
        {
            bool callbackInvoked = false;

            SocialInteraction.InviteFriends(onErrorCallback: () =>
            {
                callbackInvoked = true;
            });

            yield return new WaitForSecondsRealtime(1);

            Assert.IsTrue(callbackInvoked);
        }
    }
}

