using System.Collections;
using Agava.VKGames;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace VKGames.Tests
{
    public class StorageTests
    {
        [UnitySetUp]
        public IEnumerator WaitForInitialization()
        {
            if (!VKGamesSdk.Initialized)
                yield return VKGamesSdk.Initialize(isTest: true);
        }
        
        [UnityTest]
        public IEnumerator ShouldNotGetData(string keys = "key")
        {
            bool callbackInvoked = false;

            Storage.GetCloudSaveData(keys, null, onErrorCallback: () =>
            {
                callbackInvoked = true;
            });

            yield return new WaitForSecondsRealtime(1);

            Assert.IsTrue(callbackInvoked);
        }

        [UnityTest]
        public IEnumerator ShouldNotSetData(string key = "key", string value = "value")
        {
            bool callbackInvoked = false;
            
            Storage.SetCloudSaveData(key, value, onErrorCallback: () =>
            {
                callbackInvoked = true;
            });
            
            yield return new WaitForSecondsRealtime(1);

            Assert.IsTrue(callbackInvoked);
        }
    }
}