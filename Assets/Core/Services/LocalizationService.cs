using System;
using System.Collections.Generic;
using Core.EcsCommon.ValueComponents;
using I2.Loc;
using Lib;
using UnityEngine;

namespace Core.Services
{
    public class LocalizationService : MonoConstruct, ISerializableService
    {
        public enum MyLocales
        {
            ru,
            en,
            be,
            fr,
            de,
            id,
            it,
            pl,
            pt,
            es,
            tr,
            vi
        }

        public Action OnChange;
        public Action OnInitialize;

        private ServiceData _serviceData = new();
        private PlayerDataService _playerDataService;
        private SdkService _sdkService;
        private bool _wasThrown;
        private bool _isShouldBeInitializeCall;
        private List<string> _locales;
        private readonly Dictionary<string, string> _cache = new();

        public string LocaleIdentifierCode => _serviceData.localeIdentifierCode;

        private void Awake()
        {
            LocalizationManager.OnLocalizeEvent += () =>
            {
                OnChange?.Invoke();

                if (_isShouldBeInitializeCall)
                    OnInitialize?.Invoke();

                _isShouldBeInitializeCall = false;
            };

            _serviceData.localeIdentifierCode = MyEnumUtility<MyLocales>.Name((int)MyLocales.ru);
            _sdkService = context.Resolve<SdkService>();
            _playerDataService = context.Resolve<PlayerDataService>();
            _playerDataService.RegisterService(this, true, true);
        }

        public void SelectNext()
        {
            var locale = _locales.CircularNext(LocalizationManager.CurrentLanguageCode);
            ChangeLocale(locale);
        }

        public void SelectPrevious()
        {
            var locale = _locales.CircularPrevious(LocalizationManager.CurrentLanguageCode);
            ChangeLocale(locale);
        }

        public void ChangeLocale(MyLocales locale) => ChangeLocale(MyEnumUtility<MyLocales>.Name((int)locale));

        public string GetLocalizedString(string tableName, string key)
        {
            if (_cache.TryGetValue(key, out var result))
                return result;

            if (LocalizationManager.TryGetTranslation(tableName + "/" + key.TrimStart('#'), out result))
                _cache[key] = result;
            else
                _cache[key] = result = key;

            return result;
        }

        public string GetLocalizedString(string value)
        {
            if (_cache.TryGetValue(value, out var result))
                return result;

            if (LocalizationManager.TryGetTranslation(value, out result))
                _cache[value] = result;
            else
                _cache[value] = result = value;

            return result;
        }

        public bool HasLocalizedString(string table, string key)
        {
            // Проверяем наличие локали
            if (_cache.TryGetValue(key, out var result))
                return true;

            if (!LocalizationManager.TryGetTranslation(table + "/" + key.TrimStart('#'), out result))
                return false;

            _cache[key] = result;
            return true;
        }

        public void SaveData() => _playerDataService.SerializeData(this, _serviceData);

        public void ReloadData()
        {
            _serviceData = _playerDataService.DeserializeData<ServiceData>(this);

            var locale = !string.IsNullOrEmpty(_serviceData.localeIdentifierCode)
                ? _serviceData.localeIdentifierCode
                : _sdkService.GetLocale().ToLower();

            _isShouldBeInitializeCall = true;
            ChangeLocale(locale, false);
        }

        private void ChangeLocale(string locale, bool dirty = true)
        {
            _locales ??= LocalizationManager.GetAllLanguagesCode();

            if (!_locales.Contains(locale))
                locale = "en";

            if (!_locales.Contains(locale))
            {
                Debug.LogWarning("Unknown Locale: " + locale);
                locale = LocalizationManager.CurrentLanguageCode;
            }

            if (_serviceData.localeIdentifierCode != locale)
            {
                _serviceData.localeIdentifierCode = locale;

                if (dirty)
                    _playerDataService.SetDirty(this);
            }

            if (LocalizationManager.CurrentLanguageCode == locale)
            {
                OnChange?.Invoke();

                if (_isShouldBeInitializeCall)
                    OnInitialize?.Invoke();

                return;
            }

            Debug.Log($"Change Locale From ({LocalizationManager.CurrentLanguageCode}) to ({locale})");
            LocalizationManager.CurrentLanguageCode = locale;
        }

        private class ServiceData
        {
            public string localeIdentifierCode;
        }
    }
}