const library = {

  // Class definition.

  $yandexGames: {
    isInitialized: false,

    isAuthorized: false,

    sdk: undefined,

    leaderboard: undefined,

    playerAccount: undefined,

    billing: undefined,

    isInitializeCalled: false,

    yandexGamesSdkInitialize: function (successCallbackPtr, errorCallbackPtr) {
      if (yandexGames.isInitializeCalled) {
        return;
      }
      yandexGames.isInitializeCalled = true;

      if (window.location.hostname === "localhost") {
        dynCall('v', errorCallbackPtr, []);
        return;
      }
      
      window['YaGames'].init().then(function (sdk) {
        yandexGames.sdk = sdk;

        // The { scopes: false } ensures personal data permission request window won't pop up,
        const playerAccountInitializationPromise = sdk.getPlayer({ scopes: false }).then(function (playerAccount) {
          if (playerAccount.isAuthorized() === true) {
            yandexGames.isAuthorized = true;
          }

          // Always contains permission info. Contains personal data as well if permissions were granted before.
          yandexGames.playerAccount = playerAccount;
        }).catch(function () { throw new Error('PlayerAccount failed to initialize.'); });

        const billingInitializationPromise = sdk.getPayments({ signed: true }).then(function (billing) {
          yandexGames.billing = billing;
        }).catch(function () { throw new Error('Billing failed to initialize.'); });

        Promise.allSettled([playerAccountInitializationPromise, billingInitializationPromise]).then(function () {
          yandexGames.isInitialized = true;
          dynCall('v', successCallbackPtr, []);
        });
      });
    },

    throwIfSdkNotInitialized: function (errorCallbackPtr) {
      if (!yandexGames.isInitialized) {
        if (errorCallbackPtr) {
          yandexGames.invokeErrorCallback(new Error('SDK is not initialized. Invoke YandexGamesSdk.Initialize() coroutine and wait for it to finish.'), errorCallbackPtr);
          return true;
        } else {
          console.log('SDK is not initialized. Invoke YandexGamesSdk.Initialize() coroutine and wait for it to finish.');
          return true;
        }
      }
      return false;
    },

    gameReady: function() {
      yandexGames.sdk.features.LoadingAPI.ready();
    },

    invokeErrorCallback: function (error, errorCallbackPtr) {
      var errorMessage;
      if (error instanceof Error) {
        errorMessage = error.message;
        if (errorMessage === null) { errorMessage = 'SDK API thrown an error with null message.' }
        if (errorMessage === undefined) { errorMessage = 'SDK API thrown an error with undefined message.' }
      } else if (typeof error === 'string') {
        errorMessage = error;
      } else if (error) {
        errorMessage = 'SDK API thrown an unexpected type as error: ' + JSON.stringify(error);
      } else if (error === null) {
        errorMessage = 'SDK API thrown a null as error.';
      } else {
        errorMessage = 'SDK API thrown an undefined as error.';
      }

      const errorUnmanagedStringPtr = yandexGames.allocateUnmanagedString(errorMessage);
      dynCall('vi', errorCallbackPtr, [errorUnmanagedStringPtr]);
      _free(errorUnmanagedStringPtr);
    },

    invokeErrorCallbackIfNotAuthorized: function (errorCallbackPtr) {
      if (!yandexGames.isAuthorized) {
        yandexGames.invokeErrorCallback(new Error('Needs authorization.'), errorCallbackPtr);
        return true;
      }
      return false;
    },

    getYandexGamesSdkEnvironment: function () {
      const environmentJson = JSON.stringify(yandexGames.sdk.environment);
      const environmentJsonUnmanagedStringPtr = yandexGames.allocateUnmanagedString(environmentJson);
      return environmentJsonUnmanagedStringPtr;
    },

    getDeviceType: function () {
      const deviceType = yandexGames.sdk.deviceInfo.type;

      switch (deviceType) {
        case 'desktop':
          return 0;
        case 'mobile':
          return 1;
        case 'tablet':
          return 2;
        case 'tv':
          return 3;
        default:
          console.error('Unexpected ysdk.deviceInfo response from Yandex. Assuming that it is desktop. deviceType = '
            + JSON.stringify(deviceType));
          return 0;
      }
    },

    playerAccountStartAuthorizationPolling: function (delay, successCallbackPtr, errorCallbackPtr) {
      if (yandexGames.isAuthorized) {
        console.error('Already authorized.');
        dynCall('v', errorCallbackPtr, []);
        return;
      }

      function authorizationPollingLoop() {
        if (yandexGames.isAuthorized) {
          dynCall('v', successCallbackPtr, []);
          return;
        }

        yandexGames.sdk.getPlayer({ scopes: false }).then(function (playerAccount) {
          if (playerAccount.isAuthorized() === true) {
            yandexGames.isAuthorized = true;
            yandexGames.playerAccount = playerAccount;
            dynCall('v', successCallbackPtr, []);
          } else {
            setTimeout(authorizationPollingLoop, delay);
          }
        });
      };

      authorizationPollingLoop();
    },

    playerAccountAuthorize: function (successCallbackPtr, errorCallbackPtr) {
      if (yandexGames.isAuthorized) {
        console.error('Already authorized.');
        dynCall('v', successCallbackPtr, []);
        return;
      }

      yandexGames.sdk.auth.openAuthDialog().then(function () {
        yandexGames.sdk.getPlayer({ scopes: false }).then(function (playerAccount) {
          yandexGames.isAuthorized = true;
          yandexGames.playerAccount = playerAccount;
          dynCall('v', successCallbackPtr, []);
        }).catch(function (error) {
          console.error('authorize failed to update playerAccount. Assuming authorization failed. Error was: ' + error.message);
          yandexGames.invokeErrorCallback(error, errorCallbackPtr);
        });
      }).catch(function (error) {
        yandexGames.invokeErrorCallback(error, errorCallbackPtr);
      });
    },

    getPlayerAccountHasPersonalProfileDataPermission: function () {
      var publicNamePermission = undefined;
      if ('_personalInfo' in yandexGames.playerAccount && 'scopePermissions' in yandexGames.playerAccount._personalInfo) {
        publicNamePermission = yandexGames.playerAccount._personalInfo.scopePermissions.public_name;
      }

      switch (publicNamePermission) {
        case 'forbid':
          return false;
        case 'not_set':
          return false;
        case 'allow':
          return true;
        default:
          console.error('Unexpected response from Yandex. Assuming profile data permissions were not granted. playerAccount = '
            + JSON.stringify(yandexGames.playerAccount));
          return false;
      }
    },

    playerAccountRequestPersonalProfileDataPermission: function (successCallbackPtr, errorCallbackPtr) {
      yandexGames.sdk.getPlayer({ scopes: true }).then(function (playerAccount) {
        yandexGames.playerAccount = playerAccount;

        if (yandexGames.getPlayerAccountHasPersonalProfileDataPermission()) {
          dynCall('v', successCallbackPtr, []);
        } else {
          yandexGames.invokeErrorCallback(new Error('User has refused the permission request.'), errorCallbackPtr);
        }
      }).catch(function (error) {
        yandexGames.invokeErrorCallback(error, errorCallbackPtr);
      });
    },

    playerAccountGetProfileData: function (successCallbackPtr, errorCallbackPtr, pictureSize) {
      yandexGames.sdk.getPlayer({ scopes: false }).then(function (playerAccount) {
        yandexGames.playerAccount = playerAccount;

        playerAccount._personalInfo.profilePicture = playerAccount.getPhoto(pictureSize);

        const profileDataJson = JSON.stringify(playerAccount._personalInfo);
        const profileDataUnmanagedStringPtr = yandexGames.allocateUnmanagedString(profileDataJson);
        dynCall('vi', successCallbackPtr, [profileDataUnmanagedStringPtr]);
        _free(profileDataUnmanagedStringPtr);
      }).catch(function (error) {
        yandexGames.invokeErrorCallback(error, errorCallbackPtr);
      });
    },

    playerAccountGetCloudSaveData: function (successCallbackPtr, errorCallbackPtr) {
      yandexGames.playerAccount.getData().then(function (сloudSaveData) {
        const сloudSaveDataUnmanagedStringPtr = yandexGames.allocateUnmanagedString(JSON.stringify(сloudSaveData));
        dynCall('vi', successCallbackPtr, [сloudSaveDataUnmanagedStringPtr]);
        _free(сloudSaveDataUnmanagedStringPtr);
      }).catch(function (error) {
        yandexGames.invokeErrorCallback(error, errorCallbackPtr);
      });
    },

    playerAccountSetCloudSaveData: function (сloudSaveDataJson, successCallbackPtr, errorCallbackPtr) {
      var сloudSaveData = JSON.parse(сloudSaveDataJson);
      yandexGames.playerAccount.setData(сloudSaveData, true).then(function () {
        dynCall('v', successCallbackPtr, []);
      }).catch(function (error) {
        yandexGames.invokeErrorCallback(error, errorCallbackPtr);
      });
    },

    getFlags: function (successCallbackPtr, errorCallbackPtr) {
      yandexGames.sdk.getFlags().then(function (flags) {
        const flagsUnmanagedStringPtr = yandexGames.allocateUnmanagedString(JSON.stringify(flags));
        dynCall('vi', successCallbackPtr, [flagsUnmanagedStringPtr]);
        _free(flagsUnmanagedStringPtr);
      }).catch(function (error) {
        yandexGames.invokeErrorCallback(error, errorCallbackPtr);
      });
    },

    interstitialAdShow: function (openCallbackPtr, closeCallbackPtr, errorCallbackPtr, offlineCallbackPtr) {
      yandexGames.sdk.adv.showFullscreenAdv({
        callbacks: {
          onOpen: function () {
            dynCall('v', openCallbackPtr, []);
          },
          onClose: function (wasShown) {
            dynCall('vi', closeCallbackPtr, [wasShown]);
          },
          onError: function (error) {
            yandexGames.invokeErrorCallback(error, errorCallbackPtr);
          },
          onOffline: function () {
            dynCall('v', offlineCallbackPtr, []);
          },
        }
      });
    },

    videoAdShow: function (openCallbackPtr, rewardedCallbackPtr, closeCallbackPtr, errorCallbackPtr) {
      yandexGames.sdk.adv.showRewardedVideo({
        callbacks: {
          onOpen: function () {
            dynCall('v', openCallbackPtr, []);
          },
          onRewarded: function () {
            dynCall('v', rewardedCallbackPtr, []);
          },
          onClose: function () {
            dynCall('v', closeCallbackPtr, []);
          },
          onError: function (error) {
            yandexGames.invokeErrorCallback(error, errorCallbackPtr);
          },
        }
      });
    },

    stickyAdShow: function() {
      yandexGames.sdk.adv.showBannerAdv();
    },

    stickyAdHide: function() {
      yandexGames.sdk.adv.hideBannerAdv();
    },

    leaderboardSetScore: function (leaderboardName, score, successCallbackPtr, errorCallbackPtr, extraData) {
      if (yandexGames.invokeErrorCallbackIfNotAuthorized(errorCallbackPtr)) {
        console.error('leaderboardSetScore requires authorization.');
        return;
      }

      console.log('yandexGames.sdk.leaderboards.setScore ' + score);
      yandexGames.sdk.leaderboards.setScore(leaderboardName, score, extraData).then(function () {
        console.log('yandexGames.sdk.leaderboards.setScore complete');
        dynCall('v', successCallbackPtr, []);
      }).catch(function (error) {
        console.log('yandexGames.sdk.leaderboards.setScore error');
        console.log(error);
        yandexGames.invokeErrorCallback(error, errorCallbackPtr);
      });
    },

    leaderboardGetEntries: function (leaderboardName, successCallbackPtr, errorCallbackPtr, topPlayersCount, competingPlayersCount, includeSelf, pictureSize) {
      if (yandexGames.invokeErrorCallbackIfNotAuthorized(errorCallbackPtr)) {
        console.error('leaderboardGetEntries requires authorization.');
        return;
      }

      yandexGames.sdk.leaderboards.getEntries(leaderboardName, {
        includeUser: includeSelf, quantityAround: competingPlayersCount, quantityTop: topPlayersCount
      }).then(function (response) {
        response.entries.forEach(function(entry) {
          entry.player.profilePicture = entry.player.getAvatarSrc({ size: pictureSize });
        });

        const entriesJson = JSON.stringify(response);
        const entriesUnmanagedStringPtr = yandexGames.allocateUnmanagedString(entriesJson);
        dynCall('vi', successCallbackPtr, [entriesUnmanagedStringPtr]);
        _free(entriesUnmanagedStringPtr);
      }).catch(function (error) {
        yandexGames.invokeErrorCallback(error, errorCallbackPtr);
      });
    },

    leaderboardGetPlayerEntry: function (leaderboardName, successCallbackPtr, errorCallbackPtr, pictureSize) {
      if (yandexGames.invokeErrorCallbackIfNotAuthorized(errorCallbackPtr)) {
        console.error('leaderboardGetPlayerEntry requires authorization.');
        return;
      }

      yandexGames.sdk.leaderboards.getPlayerEntry(leaderboardName).then(function (response) {
        response.player.profilePicture = response.player.getAvatarSrc({ size: pictureSize });

        const entryJson = JSON.stringify(response);
        const entryJsonUnmanagedStringPtr = yandexGames.allocateUnmanagedString(entryJson);
        dynCall('vi', successCallbackPtr, [entryJsonUnmanagedStringPtr]);
        _free(entryJsonUnmanagedStringPtr);
      }).catch(function (error) {
        if (error.code === 'LEADERBOARD_PLAYER_NOT_PRESENT') {
          const nullUnmanagedStringPtr = yandexGames.allocateUnmanagedString('null');
          dynCall('vi', successCallbackPtr, [nullUnmanagedStringPtr]);
          _free(nullUnmanagedStringPtr);
        } else {
          yandexGames.invokeErrorCallback(error, errorCallbackPtr);
        }
      });
    },

    billingPurchaseProduct: function (productId, successCallbackPtr, errorCallbackPtr, developerPayload) {
      yandexGames.billing.purchase({ id: productId, developerPayload: developerPayload }).then(function (purchaseResponse) {
        purchaseResponse = { purchaseData: purchaseResponse.purchaseData, signature: purchaseResponse.signature };

        const purchasedProductJson = JSON.stringify(purchaseResponse);
        const purchasedProductJsonUnmanagedStringPtr = yandexGames.allocateUnmanagedString(purchasedProductJson);
        dynCall('vi', successCallbackPtr, [purchasedProductJsonUnmanagedStringPtr]);
        _free(purchasedProductJsonUnmanagedStringPtr);
      }).catch(function (error) {
        yandexGames.invokeErrorCallback(error, errorCallbackPtr);
      });
    },

    billingConsumeProduct: function (purchasedProductToken, successCallbackPtr, errorCallbackPtr) {
      yandexGames.billing.consumePurchase(purchasedProductToken).then(function (consumedProduct) {
        dynCall('v', successCallbackPtr, []);
      }).catch(function (error) {
        yandexGames.invokeErrorCallback(error, errorCallbackPtr);
      });
    },

    billingGetProductCatalog: function (successCallbackPtr, errorCallbackPtr) {
      yandexGames.billing.getCatalog().then(function (productCatalogResponse) {
        productCatalogResponse = { products: productCatalogResponse, signature: productCatalogResponse.signature };

        const productCatalogJson = JSON.stringify(productCatalogResponse);
        const productCatalogJsonUnmanagedStringPtr = yandexGames.allocateUnmanagedString(productCatalogJson);
        dynCall('vi', successCallbackPtr, [productCatalogJsonUnmanagedStringPtr]);
        _free(productCatalogJsonUnmanagedStringPtr);
      }).catch(function (error) {
        yandexGames.invokeErrorCallback(error, errorCallbackPtr);
      });
    },

    billingGetPurchasedProducts: function (successCallbackPtr, errorCallbackPtr) {
      yandexGames.billing.getPurchases().then(function (purchasesResponse) {
        purchasesResponse = { purchasedProducts: purchasesResponse, signature: purchasesResponse.signature };

        const purchasedProductsJson = JSON.stringify(purchasesResponse);
        const purchasedProductsJsonUnmanagedStringPtr = yandexGames.allocateUnmanagedString(purchasedProductsJson);
        dynCall('vi', successCallbackPtr, [purchasedProductsJsonUnmanagedStringPtr]);
        _free(purchasedProductsJsonUnmanagedStringPtr);
      }).catch(function (error) {
        yandexGames.invokeErrorCallback(error, errorCallbackPtr);
      });
    },

    shortcutCanSuggest: function(resultCallbackPtr) {
      yandexGames.sdk.shortcut.canShowPrompt().then(function(prompt) {
        dynCall('vi', resultCallbackPtr, [prompt.canShow]);
      });
    },

    shortcutSuggest: function(resultCallbackPtr) {
      yandexGames.sdk.shortcut.showPrompt().then(function(result) {
        dynCall('vi', resultCallbackPtr, [result.outcome === 'accepted']);
      });
    },

    reviewPopupCanOpen: function(resultCallbackPtr) {
      yandexGames.sdk.feedback.canReview().then(function(result, reason) {
        if (!reason) { reason = 'No reason'; }
        const reasonUnmanagedStringPtr = yandexGames.allocateUnmanagedString(reason);
        dynCall('vii', resultCallbackPtr, [result, reasonUnmanagedStringPtr]);
        _free(reasonUnmanagedStringPtr);
      });
    },

    reviewPopupOpen: function(resultCallbackPtr) {
      yandexGames.sdk.feedback.requestReview().then(function(result) {
        dynCall('vi', resultCallbackPtr, [result]);
      });
    },

    allocateUnmanagedString: function (string) {
      const stringBufferSize = lengthBytesUTF8(string) + 1;
      const stringBufferPtr = _malloc(stringBufferSize);
      stringToUTF8(string, stringBufferPtr, stringBufferSize);
      return stringBufferPtr;
    },
  },


  // External C# calls.

  YandexGamesSdkInitialize: function (successCallbackPtr, errorCallbackPtr) {
    yandexGames.yandexGamesSdkInitialize(successCallbackPtr, errorCallbackPtr);
  },

  GetYandexGamesSdkIsInitialized: function () {
    return yandexGames.isInitialized;
  },

  GetYandexGamesSdkEnvironment: function () {
    return yandexGames.throwIfSdkNotInitialized() ? '{}' : yandexGames.getYandexGamesSdkEnvironment();
  },

  GetDeviceType: function () {
    return yandexGames.throwIfSdkNotInitialized() ? -1 : yandexGames.getDeviceType();
  },

  PlayerAccountStartAuthorizationPolling: function (delay, successCallbackPtr, errorCallbackPtr) {
    if (yandexGames.throwIfSdkNotInitialized(errorCallbackPtr)) {
      return;
    }

    yandexGames.playerAccountStartAuthorizationPolling(delay, successCallbackPtr, errorCallbackPtr);
  },

  PlayerAccountAuthorize: function (successCallbackPtr, errorCallbackPtr) {
    if (yandexGames.throwIfSdkNotInitialized(errorCallbackPtr)) {
      return;
    }

    yandexGames.playerAccountAuthorize(successCallbackPtr, errorCallbackPtr);
  },

  GetPlayerAccountIsAuthorized: function () {
    return yandexGames.throwIfSdkNotInitialized() ? false : yandexGames.isAuthorized;
  },

  GetPlayerAccountHasPersonalProfileDataPermission: function () {
    return yandexGames.throwIfSdkNotInitialized() ? false : yandexGames.getPlayerAccountHasPersonalProfileDataPermission();
  },

  PlayerAccountRequestPersonalProfileDataPermission: function (successCallbackPtr, errorCallbackPtr) {
    if (yandexGames.throwIfSdkNotInitialized(errorCallbackPtr)) {
      return;
    }

    yandexGames.playerAccountRequestPersonalProfileDataPermission(successCallbackPtr, errorCallbackPtr);
  },

  PlayerAccountGetProfileData: function (successCallbackPtr, errorCallbackPtr, pictureSizePtr) {
    if (yandexGames.throwIfSdkNotInitialized(errorCallbackPtr)) {
      return;
    }

    const pictureSize = UTF8ToString(pictureSizePtr);

    yandexGames.playerAccountGetProfileData(successCallbackPtr, errorCallbackPtr, pictureSize);
  },

  PlayerAccountGetCloudSaveData: function (successCallbackPtr, errorCallbackPtr) {
    if (yandexGames.throwIfSdkNotInitialized(errorCallbackPtr)) {
      return;
    }

    yandexGames.playerAccountGetCloudSaveData(successCallbackPtr, errorCallbackPtr);
  },

  PlayerAccountSetCloudSaveData: function (сloudSaveDataJsonPtr, successCallbackPtr, errorCallbackPtr) {
    if (yandexGames.throwIfSdkNotInitialized(errorCallbackPtr)) {
      return;
    }

    const сloudSaveDataJson = UTF8ToString(сloudSaveDataJsonPtr);

    yandexGames.playerAccountSetCloudSaveData(сloudSaveDataJson, successCallbackPtr, errorCallbackPtr);
  },

  GetFlags: function (successCallbackPtr, errorCallbackPtr) {
    if (yandexGames.throwIfSdkNotInitialized(errorCallbackPtr)) {
      return;
    }

    yandexGames.getFlags(successCallbackPtr, errorCallbackPtr);
  },

  InterstitialAdShow: function (openCallbackPtr, closeCallbackPtr, errorCallbackPtr, offlineCallbackPtr) {
    if (yandexGames.throwIfSdkNotInitialized(errorCallbackPtr)) {
      return;
    }

    yandexGames.interstitialAdShow(openCallbackPtr, closeCallbackPtr, errorCallbackPtr, offlineCallbackPtr);
  },

  VideoAdShow: function (openCallbackPtr, rewardedCallbackPtr, closeCallbackPtr, errorCallbackPtr) {
    if (yandexGames.throwIfSdkNotInitialized(errorCallbackPtr)) {
      return;
    }

    yandexGames.videoAdShow(openCallbackPtr, rewardedCallbackPtr, closeCallbackPtr, errorCallbackPtr);
  },

  StickyAdShow: function () {
    if (yandexGames.throwIfSdkNotInitialized()) {
      return;
    }

    yandexGames.stickyAdShow();
  },

  StickyAdHide: function () {
    if (yandexGames.throwIfSdkNotInitialized()) {
      return;
    }

    yandexGames.stickyAdHide();
  },

  LeaderboardSetScore: function (leaderboardNamePtr, score, successCallbackPtr, errorCallbackPtr, extraDataPtr) {
    if (yandexGames.throwIfSdkNotInitialized(errorCallbackPtr)) {
      return;
    }

    const leaderboardName = UTF8ToString(leaderboardNamePtr);
    var extraData = UTF8ToString(extraDataPtr);
    if (extraData.length === 0) { extraData = undefined; }

    yandexGames.leaderboardSetScore(leaderboardName, score, successCallbackPtr, errorCallbackPtr, extraData);
  },

  LeaderboardGetEntries: function (leaderboardNamePtr, successCallbackPtr, errorCallbackPtr, topPlayersCount, competingPlayersCount, includeSelf, pictureSizePtr) {
    if (yandexGames.throwIfSdkNotInitialized(errorCallbackPtr)) {
      return;
    }

    const leaderboardName = UTF8ToString(leaderboardNamePtr);
    // Booleans are transferred as either 1 or 0, so using !! to convert them to true or false.
    includeSelf = !!includeSelf;
    const pictureSize = UTF8ToString(pictureSizePtr);

    yandexGames.leaderboardGetEntries(leaderboardName, successCallbackPtr, errorCallbackPtr, topPlayersCount, competingPlayersCount, includeSelf, pictureSize);
  },

  LeaderboardGetPlayerEntry: function (leaderboardNamePtr, successCallbackPtr, errorCallbackPtr, pictureSizePtr) {
    if (yandexGames.throwIfSdkNotInitialized(errorCallbackPtr)) {
      return;
    }

    const leaderboardName = UTF8ToString(leaderboardNamePtr);
    const pictureSize = UTF8ToString(pictureSizePtr);

    yandexGames.leaderboardGetPlayerEntry(leaderboardName, successCallbackPtr, errorCallbackPtr, pictureSize);
  },

  BillingPurchaseProduct: function (productIdPtr, successCallbackPtr, errorCallbackPtr, developerPayloadPtr) {
    if (yandexGames.throwIfSdkNotInitialized(errorCallbackPtr)) {
      return;
    }

    const productId = UTF8ToString(productIdPtr);
    var developerPayload = UTF8ToString(developerPayloadPtr);
    if (developerPayload.length === 0) { developerPayload = undefined; }

    yandexGames.billingPurchaseProduct(productId, successCallbackPtr, errorCallbackPtr, developerPayload);
  },

  BillingConsumeProduct: function (purchasedProductTokenPtr, successCallbackPtr, errorCallbackPtr) {
    if (yandexGames.throwIfSdkNotInitialized(errorCallbackPtr)) {
      return;
    }

    const purchasedProductToken = UTF8ToString(purchasedProductTokenPtr);

    yandexGames.billingConsumeProduct(purchasedProductToken, successCallbackPtr, errorCallbackPtr);
  },

  BillingGetProductCatalog: function (successCallbackPtr, errorCallbackPtr) {
    if (yandexGames.throwIfSdkNotInitialized(errorCallbackPtr)) {
      return;
    }

    yandexGames.billingGetProductCatalog(successCallbackPtr, errorCallbackPtr);
  },

  BillingGetPurchasedProducts: function (successCallbackPtr, errorCallbackPtr) {
    if (yandexGames.throwIfSdkNotInitialized(errorCallbackPtr)) {
      return;
    }

    yandexGames.billingGetPurchasedProducts(successCallbackPtr, errorCallbackPtr);
  },

  ShortcutCanSuggest: function (resultCallbackPtr) {
    if (yandexGames.throwIfSdkNotInitialized()) {
      dynCall('vi', resultCallbackPtr, [false]);
      return;
    }

    yandexGames.shortcutCanSuggest(resultCallbackPtr);
  },

  ShortcutSuggest: function (resultCallbackPtr) {
    if (yandexGames.throwIfSdkNotInitialized()) {
      dynCall('vi', resultCallbackPtr, [false]);
      return;
    }

    yandexGames.shortcutSuggest(resultCallbackPtr);
  },

  ReviewPopupCanOpen: function (resultCallbackPtr) {
    if (yandexGames.throwIfSdkNotInitialized()) {
      const reasonUnmanagedStringPtr = yandexGames.allocateUnmanagedString('SDK not initialized');
      dynCall('vii', resultCallbackPtr, [false, reasonUnmanagedStringPtr]);
      _free(reasonUnmanagedStringPtr);
      return;
    }

    yandexGames.reviewPopupCanOpen(resultCallbackPtr);
  },

  ReviewPopupOpen: function (resultCallbackPtr) {
    if (yandexGames.throwIfSdkNotInitialized()) {
      dynCall('vi', resultCallbackPtr, [false]);
      return;
    }

    yandexGames.reviewPopupOpen(resultCallbackPtr);
  },

  YandexGamesSdkGameReady: function() {
    if (yandexGames.throwIfSdkNotInitialized()) {
      return;
    }

    yandexGames.gameReady();
  },

  YandexGamesSdkIsRunningOnYandex: function() {
    return window.location.hostname.includes('yandex');
  },

  GetHostname: function() {
    return window.location.hostname;
  }
}

autoAddDeps(library, '$yandexGames');
mergeInto(LibraryManager.library, library);