const library = {
    $vkGames: {
        bridge: undefined,

        isInitialized: false,

        vkWebAppInit: function (successCallbackPtr, errorCallbackPtr, isTest) {

            if (vkGames.isInitialized) {
                return;
            }

            function setupVkBridge() {
                function invokeSuccess() {
                    vkGames.isInitialized = true;
                    vkGames.bridge = window['vkBridge'];
                    dynCall('v', successCallbackPtr);
                }

                function invokeFailure(error) {
                    dynCall('v', errorCallbackPtr);
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

            if (window['vkBridge'] == null) {
                const sdkScript = document.createElement('script');
                sdkScript.src = 'https://unpkg.com/@vkontakte/vk-bridge/dist/browser.min.js';
                document.head.appendChild(sdkScript);

                sdkScript.onload = setupVkBridge;
                return;
            }

            setupVkBridge();
        },

        throwIfSdkNotInitialized: function () {
            if (!vkGames.isInitialized) {
                throw new Error('SDK is not initialized. Invoke VKGamesSdk.Initialize() coroutine and wait for it to finish.');
            }
        },

        vkWebAppShowRewardedAd: function (rewardedCallbackPtr, errorCallbackPtr) {
            vkGames.bridge.send("VKWebAppShowNativeAds", { ad_format: "reward" })
                .then(function (data) {
                    if (data.result)
                        dynCall('v', rewardedCallbackPtr);
                })
                .catch(function (error) {
                    dynCall('v', errorCallbackPtr);
                    console.log(error);
                });
        },

        vkWebAppShowInterstitialAd: function (openCallbackPtr, errorCallbackPtr) {
            vkGames.bridge.send("VKWebAppShowNativeAds", { ad_format: "interstitial" })
                .then(function (data) {
                    if (data.result)
                        dynCall('v', openCallbackPtr);
                })
                .catch(function (error) {
                    dynCall('v', errorCallbackPtr);
                    console.log(error);
                });
        },

        vkWebAppShowLeaderboardBox: function (playerScore, errorCallbackPtr) {
            vkGames.bridge.send("VKWebAppShowLeaderBoardBox", { user_result: playerScore })
                .then(function (data) {
                    console.log(data.success);
                })
                .catch(function (error) {
                    dynCall('v', errorCallbackPtr);
                    console.log(error);
                });
        },

        vkWebAppShowInviteBox: function (successCallbackPtr, errorCallbackPtr) {
            vkGames.bridge.send("VKWebAppShowInviteBox", {})
                .then(function (data) {
                    if (data.success)
                        dynCall('v', successCallbackPtr);
                })
                .catch(function (error) {
                    dynCall('v', errorCallbackPtr);
                    console.log(error);
                });
        },

        vkWebAppJoinGroup: function (groupId, successCallbackPtr, errorCallbackPtr) {
            vkGames.bridge.send("VKWebAppJoinGroup", { "group_id": groupId })
                .then(function (data) {
                    if (data.result)
                        dynCall('v', successCallbackPtr);
                })
                .catch(function (error) {
                    dynCall('v', errorCallbackPtr);
                    console.log(error);
                });
        },
        
        vkWebAppShowOrderBox: function (itemId, successCallbackPtr, errorCallbackPtr) {
            vkGames.bridge.send('VKWebAppShowOrderBox', { type: 'item', item: itemId})
                .then((data) => {
                    if (data.success) {
                        dynCall('v', successCallbackPtr);
                }})
                .catch((error) => {
                    dynCall('v', errorCallbackPtr);
                    console.log(error);
                });
        },

        vkWebAppSetCloudSaveData: function (key, value, successCallbackPtr, errorCallbackPtr) {
            vkGames.bridge.send("VKWebAppStorageSet", { "key": key, "value": value })
                .then(function (data) {
                    if(data.result)
                        dynCall('v', successCallbackPtr);
                })
                .catch(function (error) {
                    dynCall('v', errorCallbackPtr);
                    console.log(error);
                });
        },

        vkWebAppGetCloudSaveData: function (keysJsonArray, successCallbackPtr, errorCallbackPtr) {
            const jsonArray = JSON.parse(keysJsonArray);
            vkGames.bridge.send("VKWebAppStorageGet", jsonArray)
                .then(function (data) {
                    if(data.keys) {
                        const result = JSON.stringify(data);
                        const bridgeDataUnmanagedStringPtr = vkGames.allocateUnmanagedString(result);
                        dynCall('vi', successCallbackPtr, [bridgeDataUnmanagedStringPtr]);
                        _free(bridgeDataUnmanagedStringPtr);
                    }
                })
                .catch(function (error) {
                    dynCall('v', errorCallbackPtr);
                    console.log(error);
                });
        },

        vkWebAppStorageGetKeys: function (amount, offset, successCallbackPtr, errorCallbackPtr) {
            vkGames.bridge.send("VKWebAppStorageGetKeys", { "count": amount, "offset": offset })
                .then(function (data) {
                    if(data.keys) {
                        var serialized = JSON.stringify(data);
                        var bridgeDataUnmanagedStringPtr = vkGames.allocateUnmanagedString(serialized);
                        dynCall('vi', successCallbackPtr, [bridgeDataUnmanagedStringPtr]);
                        _free(bridgeDataUnmanagedStringPtr);
                    }
                })
                .catch(function (error) {
                    dynCall('v', errorCallbackPtr);
                    console.log(error);
                });
        },

        allocateUnmanagedString: function (string) {
            const stringBufferSize = lengthBytesUTF8(string) + 1;
            const stringBufferPtr = _malloc(stringBufferSize);
            stringToUTF8(string, stringBufferPtr, stringBufferSize);
            return stringBufferPtr;
        }
    },

    // C# calls

    WebAppInit: function (successCallbackPtr, errorCallbackPtr, isTest) {
        isTest = !!isTest;
        vkGames.vkWebAppInit(successCallbackPtr, errorCallbackPtr, isTest);
    },

    IsInitialized: function () {
        return vkGames.isInitialized;
    },

    ShowRewardedAds: function (rewardedCallbackPtr, errorCallbackPtr) {
        vkGames.throwIfSdkNotInitialized();

        vkGames.vkWebAppShowRewardedAd(rewardedCallbackPtr, errorCallbackPtr);
    },

    ShowInterstitialAds: function (openCallbackPtr, errorCallbackPtr) {
        vkGames.throwIfSdkNotInitialized();

        vkGames.vkWebAppShowInterstitialAd(openCallbackPtr, errorCallbackPtr);
    },

    ShowLeaderboardBox: function (playerScore, errorCallbackPtr) {
        vkGames.throwIfSdkNotInitialized();

        vkGames.vkWebAppShowLeaderboardBox(playerScore, errorCallbackPtr);
    },

    ShowInviteBox: function (successCallbackPtr, errorCallbackPtr) {
        vkGames.throwIfSdkNotInitialized();

        vkGames.vkWebAppShowInviteBox(successCallbackPtr, errorCallbackPtr);
    },

    JoinGroup: function (groupId, successCallbackPtr, errorCallbackPtr) {
        vkGames.throwIfSdkNotInitialized();
        
        vkGames.vkWebAppJoinGroup(groupId, successCallbackPtr, errorCallbackPtr);
    },

    ShowOrderBox: function (itemIdPtr, successCallbackPtr, errorCallbackPtr) {
        vkGames.throwIfSdkNotInitialized();

        const itemId = UTF8ToString(itemIdPtr);

        vkGames.vkWebAppShowOrderBox(itemId, successCallbackPtr, errorCallbackPtr);
    },

    StorageSetCloudSaveData: function (keyStringPtr, valueStringPtr, successCallbackPtr, errorCallbackPtr) {
        vkGames.throwIfSdkNotInitialized();

        const key = UTF8ToString(keyStringPtr);
        const value = UTF8ToString(valueStringPtr);

        vkGames.vkWebAppSetCloudSaveData(key, value, successCallbackPtr, errorCallbackPtr);
    },
    
    StorageGetCloudSaveData: function (keysJsonArrayPtr, successCallbackPtr, errorCallbackPtr) {
        vkGames.throwIfSdkNotInitialized();

        const keysJsonArray = UTF8ToString(keysJsonArrayPtr);
        
        vkGames.vkWebAppGetCloudSaveData(keysJsonArray, successCallbackPtr, errorCallbackPtr);
    },

    StorageGetKeys: function (amount, offset, successCallbackPtr, errorCallbackPtr) {
        vkGames.throwIfSdkNotInitialized();

        vkGames.vkWebAppStorageGetKeys(amount, offset, successCallbackPtr, errorCallbackPtr);
    }
}

autoAddDeps(library, '$vkGames');
mergeInto(LibraryManager.library, library);
