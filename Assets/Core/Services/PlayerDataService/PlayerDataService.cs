using System;
using System.Collections;
using System.Collections.Generic;
using Core.Services;
using Lib;
using UnityEngine;

namespace Core
{
    public class PlayerDataService : MonoConstruct, IDataService
    {
        public event Action OnSaveEnd;
        private ServicesValues _servicesValues = new();
        private const string _key = nameof(ServicesValues);
        private SdkService _sdkService;
        private float _lastSaveTime;
        private const float _sdkSaveDelay = 3.25f;
        private const float _saveDelay = 1f;
        private float _currentDelay = 1f;
        private bool _saveAlreadyActive;
        private bool _saveInprogress;
        private bool _sdkInprogress;
        private string _data;
        private readonly HashSet<ISerializableService> _totalServices = new();
        private readonly HashSet<ISerializableService> _dirtyServices = new();
        private readonly HashSet<ISerializableService> _ignoreResetServices = new();
        private readonly HashSet<ISerializableService> _lazySaveServices = new();
        private Action _forceSaveCallback;
        private Action _onSave;
        private Action<string> _onLoadDataFail;
        private readonly WaitForSecondsRealtime _wait = new(.5f);

        public bool IsInitialized { get; private set; }

        private void Awake() => enabled = false;

        private void Update()
        {
            if ((_currentDelay -= Time.unscaledDeltaTime) > 0)
                return;

            Save();
        }

        public void Initialize(Action<string> onLoadDataFail)
        {
            _onLoadDataFail = onLoadDataFail;
            _sdkService = context.Resolve<SdkService>();
            _sdkService.LoadPlayerData(OnLoadSuccess, OnLoadError);
        }

        public void Save(Action forceSaveCallback = null)
        {
            if (_saveInprogress)
            {
                forceSaveCallback?.Invoke();
                return;
            }

            _saveInprogress = true;
            _forceSaveCallback = forceSaveCallback;
            float awaitSeconds = forceSaveCallback != null ? 0 : _lastSaveTime - Time.unscaledTime + _sdkSaveDelay;
            _lastSaveTime = Time.unscaledTime;

            void SaveEnd()
            {
                OnSaveEnd?.Invoke();
                _forceSaveCallback?.Invoke();
                _forceSaveCallback = null;
                enabled = false;
                _currentDelay = _saveDelay;
                _saveInprogress = false;
            }

            if (!_saveInprogress && awaitSeconds > 0)
            {
                if (!_saveAlreadyActive)
                    StartCoroutine(AwaitBeforeSave(awaitSeconds, SaveEnd));
            }
            else
            {
                SaveServicesData(SaveEnd);
            }
        }

        public void Reset()
        {
            _servicesValues = new();

            foreach (var service in _ignoreResetServices)
                service.SaveData();

            foreach (var service in _totalServices)
                if (!_ignoreResetServices.Contains(service))
                    service.ReloadData();

            Save();
        }

        public void SerializeData(object key, object data) => _servicesValues.SerializeData(key, data);

        public void RegisterService(ISerializableService service, bool ignoreReset = false, bool lazySave = false)
        {
            _totalServices.Add(service);

            if (ignoreReset)
                _ignoreResetServices.Add(service);

            if (lazySave)
                _lazySaveServices.Add(service);
        }

        public void SetDirty(ISerializableService service, bool lazy = false)
        {
            _dirtyServices.Add(service);

            if (lazy || _lazySaveServices.Contains(service))
                return;

            enabled = true;
            _currentDelay = _saveDelay;
        }

        public T DeserializeData<T>(object key) where T : new() => _servicesValues.DeserializeData<T>(key);

        private void SaveServicesData(Action callback)
        {
            int saved = 0;

            foreach (var service in _dirtyServices)
            {
#if UNITY_EDITOR
                Debug.Log("SAVE " + service.GetType().Name);
#endif
                service.SaveData();
                saved++;
            }

            _dirtyServices.Clear();

            if (saved != 0)
                PrivateSave(callback);
            else
                callback?.Invoke();
        }

        private void PrivateSave(Action callback)
        {
            _servicesValues.version = MigrationService.ACTUAL_VERSION;
            var base64 = ES3Functions.SerializeToBase64(_servicesValues);

            // Debug.Log("SIZE: " + (int)(Encoding.UTF8.GetBytes(base64).Length / 1024.0) + "KB");

            _data = JsonUtility.ToJson(new DataWrapper
            {
                time = DateTime.Now.ToBinary(),
                data = base64
            });

            _onSave = callback;

#if DEBUG_IGNORE_SAVE
            callback?.Invoke();
            return;
#endif

            if (!IsInitialized)
            {
                Debug.LogWarning("TRY SAVE BEFORE INITIALIZE");
                _lastSaveTime = Time.unscaledTime;
                callback?.Invoke();
            }
            else
            {
                Debug.Log("BEGIN DATA SAVED");
                PlayerPrefs.SetString(_key, _data);
                PlayerPrefs.Save();
#if UNITY_EDITOR
                OnSaveSuccess();
                return;
#endif
                if (_sdkInprogress)
                    return;

                _sdkInprogress = true;
                _sdkService.SavePlayerData(_data, OnSaveSuccess,
                    _ => OnSaveError());
            }
        }

        private IEnumerator AwaitBeforeSave(float seconds, Action callback)
        {
            _saveAlreadyActive = true;
            yield return new WaitForSecondsRealtime(seconds);

            SaveServicesData(callback);
            _saveAlreadyActive = false;
        }

        public void OnLoadSuccess(string data = null)
        {
            Debug.Log("Begin Parse Data");
            DataWrapper dataWrapper = null;

            if (PlayerPrefs.HasKey(_key))
                dataWrapper = JsonUtility.FromJson<DataWrapper>(PlayerPrefs.GetString(_key));

            if (!string.IsNullOrEmpty(data))
            {
                var cloudDataWrapper = JsonUtility.FromJson<DataWrapper>(data);

                if (dataWrapper == null)
                    dataWrapper = cloudDataWrapper;
                else if (cloudDataWrapper != null && dataWrapper != null &&
                         DateTime.FromBinary(cloudDataWrapper.time) >= DateTime.FromBinary(dataWrapper.time))
                    dataWrapper = cloudDataWrapper;
            }

            if (dataWrapper?.data != null)
                _servicesValues = ES3Functions.DeserializeFromBase64<ServicesValues>(dataWrapper.data) ?? new();

            _dirtyServices.Clear();

            if (_servicesValues.IsEmpty)
                _servicesValues.version = MigrationService.ACTUAL_VERSION;

            Debug.Log("DataVersion: " + _servicesValues.version);
            Debug.Log("Begin Reload Services");
            foreach (var service in _totalServices)
                service.ReloadData();

            Debug.Log("Player Data Service Initialized Successfully");
            IsInitialized = true;
        }

        private void OnLoadError(string error)
        {
            if (PlayerPrefs.HasKey(_key))
                OnLoadSuccess(PlayerPrefs.GetString(_key));
#if UNITY_EDITOR
            else
                OnLoadSuccess();
#else
            else
                _onLoadDataFail(error);
#endif
        }

        private void OnSaveSuccess()
        {
            _sdkInprogress = false;
            _onSave?.Invoke();

            _lastSaveTime = Time.unscaledTime;
            Debug.Log("DATA SAVED");
        }

        private void OnSaveError() => StartCoroutine(ReSave());

        private IEnumerator ReSave()
        {
            yield return _wait;
            _sdkService.SavePlayerData(_data, OnSaveSuccess,
                _ => OnSaveError());
        }

        public class DataWrapper
        {
            public long time;
            public string data;
        }

        public ReleaseVersion GetDataVersion() => _servicesValues.version;
    }
}