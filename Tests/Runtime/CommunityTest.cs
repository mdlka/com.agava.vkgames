using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Agava.VKGames;

namespace VKGames.Tests
{
    public class CommunityTest : MonoBehaviour
    {
        [UnitySetUp]
        public IEnumerator WaitForInitialization()
        {
            if (!VKGamesSdk.Initialized)
                yield return VKGamesSdk.Initialize(isTest: true);
        }

        [UnityTest]
        public IEnumerator ShowShouldInvokeErrorCallback()
        {
            bool callbackInvoked = false;

            Community.InviteToIJuniorGroup(onErrorCallback: () =>
            {
                callbackInvoked = true;
            });

            yield return new WaitForSecondsRealtime(1);

            Assert.IsTrue(callbackInvoked);
        }
    }
}

