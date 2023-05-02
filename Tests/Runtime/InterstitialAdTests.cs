using System.Collections;
using Agava.VKGames;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace VKGames.Tests
{
    public class InterstitialAdTests
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
            Interstitial.Show(onErrorCallback: () =>
            {
                callbackInvoked = true;
            });

            yield return new WaitForSecondsRealtime(1);

            Assert.IsTrue(callbackInvoked);
        }
    }
}
