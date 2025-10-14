using System;
using System.Collections;
using System.Collections.Generic;
using Agava.YandexGames;
using Core.EcsCommon.ValueComponents;
using Core.Lib;
using Core.Lib.Services;
using Core.Services;
using Reflex;
using UnityEngine;

namespace Core
{
    public class SdkService : IInitializableService
    {
        public Flags flags = new();

        private Action<string> _onSuccessLoadPlayerData;
        private Action _onRewardedCallback;
        private Action<string> _onErrorCallback;
        private TimeScaleService _timeScaleService;
        private AudioSourceService _audioSourceService;
        private bool _adOpened;
        private bool _rewardSuccess;
        private Action _playAdCallback;
        private float _lastSaveLeaderboardTime;
        private int _loadDataTryCount;

        public PlayerAccountProfileDataResponse PlayerProfile { get; private set; }

        public bool IsInitialized { get; private set; }

        public void InitializeService(Context context)
        {
            _audioSourceService = context.Resolve<AudioSourceService>();
            _timeScaleService = context.Resolve<TimeScaleService>();
        }

        public IEnumerator Initialize(Action callback)
        {
            IEnumerator enumerator = null;

            void SetInitialized()
            {
                IsInitialized = true;
                callback?.Invoke();
            }

            try
            {
#if !UNITY_EDITOR && YANDEX_GAMES
                Debug.Log("YandexGamesSdk.Initialize Start");
                enumerator = YandexGamesSdk.Initialize(() =>
                {
                    Debug.Log("YandexGamesSdk.Initialize Successful");
                    var _ = YandexGamesSdk.Environment?.i18n?.lang ?? string.Empty;
                    PlayerAccount.GetProfileData(OnLoadProfileData, OnLoadProfileDataError);
                    YandexGamesSdk.GetConsoleFlags(value =>
                    {
                        Debug.Log("YandexGamesSdk.GetConsoleFlags Successful");
                        flags = JsonUtility.FromJson<Flags>(value);

                        if (YandexGamesSdk.Environment.app.id != "225267")
                        {
                            Debug.LogError("Wrong ID!!!");
                            return;
                        }

                        if (flags.IsActualDomain != "true")
                        {
                            Debug.LogError("Environment Error");
                            Debug.Log(value);
                        }

                        if (YandexGamesSdk.IsRunningOnYandex)
                            SetInitialized();
                        else
                            Debug.LogError("Running On: " + YandexGamesSdk.GetHostname());
                    });
                }, SetInitialized);
#else
                SetInitialized();
#endif
            }
            catch (Exception e)
            {
                Debug.LogWarning("SDK INIT ERROR");
                Debug.LogWarning(e.Message);
#if UNITY_EDITOR
                if (e.Message == "")
                    SetInitialized();
                else
                    throw;
#else
            SetInitialized();
#endif
            }

            if (enumerator is not null)
                yield return enumerator;
        }

        private void OnLoadProfileData(PlayerAccountProfileDataResponse profileData)
        {
            PlayerProfile = profileData;
            Debug.Log("OnLoadProfileData Successful");
        }

        private void OnLoadProfileDataError(string message) => Debug.LogError("OnLoadProfileData Error: " + message);

        public void LoadPlayerData(Action<string> onSuccess, Action<string> onError)
        {
            _onSuccessLoadPlayerData = onSuccess;

            try
            {
#if !UNITY_EDITOR && YANDEX_GAMES
                Debug.Log("PlayerAccount.GetCloudSaveData Start");
                PlayerAccount.GetCloudSaveData(Success, onError);
#else
                onError("No selected sdk defenition");
#endif
            }
            catch (Exception e)
            {
#if UNITY_EDITOR
                if (e.Message != "")
                    throw;

                onError(e.Message);
#else
                onError(e.Message);
#endif
            }
        }

        public void SavePlayerData(string data, Action onSuccess, Action<string> onError)
        {
            try
            {
#if !UNITY_EDITOR && YANDEX_GAMES
                PlayerAccount.SetCloudSaveData(data, onSuccess, onError);
#else
                onError("No selected sdk defenition");
#endif
            }
            catch (Exception e)
            {
                Debug.LogWarning(e.Message);
                onError(e.Message);
            }
        }

        public bool IsAuthorized()
        {
#if !UNITY_EDITOR
            return PlayerAccount.IsAuthorized;
#else
            return true;
#endif
        }

        public void OpenAuthDialog() => PlayerAccount.Authorize();

        public string GetLocale()
        {
#if !UNITY_EDITOR && YANDEX_GAMES
            return YandexGamesSdk.Environment?.i18n?.lang ?? string.Empty;
#endif
            return MyEnumUtility<LocalizationService.MyLocales>.Name((int)LocalizationService.MyLocales.ru);
        }

        public void PlayAd(Action callback)
        {
            // if (flags.Check == "true")
            // {
            //     callback?.Invoke();
            //     return;
            // }

            if (_adOpened || _playAdCallback != null)
            {
                callback?.Invoke();
                return;
            }

            _playAdCallback = callback;

            try
            {
#if !UNITY_EDITOR && YANDEX_GAMES
                Agava.YandexGames.InterstitialAd.Show(
                    AdBegin,
                    _ => AdEnded(),
                    _ => AdEnded(),
                    AdEnded
                );
#else
                callback?.Invoke();
#endif
            }
            catch (Exception e)
            {
                Debug.Log("ADD SHOW ERROR");
                Debug.Log(e.Message);
                AdEnded();
            }
        }

        public void PlayVideoAd(Action onRewardedCallback, Action<string> onErrorCallback)
        {
            if (_onRewardedCallback != null)
                return;

            _onRewardedCallback = onRewardedCallback;
            _onErrorCallback = onErrorCallback;
            _rewardSuccess = false;

            try
            {
#if !UNITY_EDITOR && YANDEX_GAMES
                VideoAd.Show(AdBegin, VideoAdSuccess, () => VideoAdErrorOrClose("Closed"), VideoAdErrorOrClose);
#else
                onRewardedCallback?.Invoke();
                _onRewardedCallback = null;
#endif
            }
            catch (Exception e)
            {
                VideoAdErrorOrClose(e.Message);
            }
        }

        public void GameReady()
        {
#if !UNITY_EDITOR
            YandexGamesSdk.GameReady();
#endif
        }

        private void VideoAdErrorOrClose(string error)
        {
            _adOpened = false;
            _audioSourceService.AdStopped();
            _timeScaleService.Resume();

            if (_rewardSuccess)
                _onRewardedCallback?.Invoke();
            else
                _onErrorCallback?.Invoke(error);

            _onRewardedCallback = null;
            _onErrorCallback = null;
        }

        private void VideoAdSuccess() => _rewardSuccess = true;

        private void Success(string playerData)
        {
            Debug.Log("Player Data Taken");
            _onSuccessLoadPlayerData(playerData);
        }

        private void AdBegin()
        {
            _adOpened = true;
            _timeScaleService.Pause();
            _audioSourceService.AdPlaying();
        }

        private void AdEnded()
        {
            if (_adOpened)
            {
                _adOpened = false;
                _timeScaleService.Resume();
                _audioSourceService.AdStopped();
                _playAdCallback?.Invoke();
                _playAdCallback = null;
                return;
            }

            _playAdCallback?.Invoke();
            _playAdCallback = null;
        }

        public class Flags
        {
            public string IsActualDomain = "false";

            public string leaderboardName = "";
            // public string Shadows = "false";
            // public string SSAO = "false";
            // public string RewardedPearlsCount = "3";
            // public string Check = "false";
        }
    }
}