const library = {
    $vkSDK: {
        bridge: undefined,

        isInitialized: false,

        vkWebAppInit: function (onInitializedCallback, onErrorCallback, isTest) {

            if (vkSDK.isInitialized) {
                return;
            }
                
            const sdkScript = document.createElement('script');
            sdkScript.src = 'https://unpkg.com/@vkontakte/vk-bridge/dist/browser.min.js';
            document.head.appendChild(sdkScript);

            sdkScript.onload = function () {
                function invokeSuccess() {
                    vkSDK.isInitialized = true;
                    vkSDK.bridge = window['vkBridge'];
                    dynCall('v', onInitializedCallback);
                }

                function invokeFailure(error) {
                    dynCall('v', onErrorCallback);
                    console.error(error);
                }

                if (isTest) {
                    window['vkBridge'] = {
                        send: function () {
                            return new Promise(function (resolve, reject) {
                                setTimeout(function () {
                                    reject(new Error('Error returned for testing purposes.'));
                                }, 0);
                            });
                        }
                    };
                    invokeSuccess();
                } else {
                    window['vkBridge'].send("VKWebAppInit", {})
                        .then(function (data) {
                            if (data.result) {
                                invokeSuccess();
                            } else {
                                invokeFailure(new Error('vkBridge failed to initialize.'));
                            }
                        })
                        .catch(function (error) {
                            invokeFailure(error);
                        });
                }
            }
        },

        throwIfSdkNotInitialized: function () {
            if (!vkSDK.isInitialized) {
                throw new Error('SDK is not initialized. Invoke VKGamesSdk.Initialize() coroutine and wait for it to finish.');
            }
        },

        vkWebSAppShowRewardedAd: function (onRewardedCallback, onErrorCallback) {
            vkSDK.bridge.send("VKWebAppShowNativeAds", { ad_format: "reward" })
                .then(function (data) {
                    if (data.result)
                        dynCall('v', onRewardedCallback);
                })
                .catch(function (error) {
                    dynCall('v', onErrorCallback);
                    console.log(error);
                });
        },

        vkWebAppShowInterstitialAd: function (onOpenCallback, onErrorCallback) {
            vkSDK.bridge.send("VKWebAppShowNativeAds", { ad_format: "interstitial" })
                .then(function (data) {
                    if (data.result)
                        dynCall('v', onOpenCallback);
                })
                .catch(function (error) {
                    dynCall('v', onErrorCallback);
                    console.log(error);
                });
        },

        vkWebAppShowLeaderboardBox: function (playerScore, onErrorCallback) {
            vkSDK.bridge.send("VKWebAppShowLeaderBoardBox", { user_result: playerScore })
                .then(function (data) {
                    console.log(data.success);
                })
                .catch(function (error) {
                    dynCall('v', onErrorCallback);
                    console.log(error);
                });
        },

        vkWebAppShowInviteBox: function (onSuccessCallback, onErrorCallback) {
            vkSDK.bridge.send("VKWebAppShowInviteBox", {})
                .then(function (data) {
                    if (data.success)
                        dynCall('v', onSuccessCallback);
                })
                .catch(function (error) {
                    dynCall('v', onErrorCallback);
                    console.log(error);
                });
        },

        vkWebJoinGroup: function (onSuccessCallback, onErrorCallback) {
            vkSDK.bridge.send("VKWebAppJoinGroup", { "group_id": 84861196 })
                .then(function (data) {
                    if (data.result)
                        dynCall('v', onSuccessCallback);
                })
                .catch(function (error) {
                    dynCall('v', onErrorCallback);
                    console.log(error);
                });
        },

    },

    // C# calls

    WebAppInit: function (onInitializedCallback, onErrorCallback, isTest) {
        isTest = !!isTest;
        vkSDK.vkWebAppInit(onInitializedCallback, onErrorCallback, isTest);
    },

    ShowRewardedAds: function (onRewardedCallback, onErrorCallback) {
        vkSDK.throwIfSdkNotInitialized();

        vkSDK.vkWebSAppShowRewardedAd(onRewardedCallback, onErrorCallback);
    },

    ShowInterstitialAds: function (onOpenCallback, onErrorCallback) {
        vkSDK.throwIfSdkNotInitialized();

        vkSDK.vkWebAppShowInterstitialAd(onOpenCallback, onErrorCallback);
    },

    ShowLeaderboardBox: function (playerScore, onErrorCallback) {
        vkSDK.throwIfSdkNotInitialized();

        vkSDK.vkWebAppShowLeaderboardBox(playerScore, onErrorCallback);
    },

    ShowInviteBox: function (onSuccessCallback, onErrorCallback) {
        vkSDK.throwIfSdkNotInitialized();

        vkSDK.vkWebAppShowInviteBox(onSuccessCallback, onErrorCallback);
    },

    JoinIjuniorGroup: function (onSuccessCallback, onErrorCallback) {
        vkSDK.throwIfSdkNotInitialized();
        
        vkSDK.vkWebJoinGroup(onSuccessCallback, onErrorCallback);
    },

    IsInitialized: function () {
        return vkSDK.isInitialized;
    }
}

autoAddDeps(library, '$vkSDK');
mergeInto(LibraryManager.library, library);
