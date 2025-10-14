using System;
using System.Collections;
using Lib;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class AddressableService : MonoConstruct
    {
        public event Action OnNextSceneWillBeLoad;
        public event Action OnClearPools;
        public event Action<float> OnLoadingPercentChange;
        public event Action OnSceneLoaded;
        public event Action OnFadeBegin;
        
        private AsyncOperation _handle;

        private DarkScreenService _darkScreenService;
        
        public string SceneName { get; private set; } = string.Empty;
        public string CurrentSceneName { get; private set; } = string.Empty;

        private void Awake() => _darkScreenService = context.Resolve<DarkScreenService>();

        public void LoadSceneAsync(string sceneName, bool immediate = false)
        {
            SceneName = sceneName;
            OnFadeBegin?.Invoke();

            switch (immediate)
            {
                case false:
                    _darkScreenService.FadeIn(Begin);
                    break;
                case true:
                    Begin();
                    break;
            }
        }

        private void Begin() => StartCoroutine(LoadSceneAsyncPrivate(SceneName));

        private IEnumerator LoadSceneAsyncPrivate(string sceneName)
        {
            yield return new WaitForFixedUpdate();
            OnNextSceneWillBeLoad?.Invoke();
            OnClearPools?.Invoke();
            _handle = SceneManager.LoadSceneAsync(sceneName);
            
            while (!_handle.isDone)
            {
                yield return null;
                OnLoadingPercentChange?.Invoke(_handle.progress);
            }

            OnLoadingPercentChange?.Invoke(_handle.progress);
            CurrentSceneName = SceneName;
            Debug.Log("Loaded Scene: " + sceneName);
            OnSceneLoaded?.Invoke();
        }
    }
}