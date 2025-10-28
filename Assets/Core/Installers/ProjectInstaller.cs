using System.Collections.Generic;
using System.Linq;
using Core.Generated;
using Core.Lib;
using Core.Lib.Services;
using Core.Services;
using Leopotam.EcsLite;
using Lib;
using Reflex;
using Reflex.Scripts;
using Reflex.Scripts.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class ProjectInstaller : Installer
    {
        [SerializeField] private Dependencies _projectDependencies;
#if UNITY_EDITOR
        [SerializeField] private Dependencies _projectDependenciesEditor;
#endif
        [SerializeField] private SceneField _nextScene;
        private InitializableServices _initializableServices;
        private DisposableServices _disposableServices;

        public override void InstallBindings(Context context)
        {
#if UNITY_EDITOR
            var projectDependencies = context.Instantiate(_projectDependenciesEditor);
#else
            var projectDependencies = context.Instantiate(_projectDependencies);
#endif

            projectDependencies.BindInstances(context);

            _initializableServices = new InitializableServices();

            foreach (var service in new IInitializableService[]
                     {
                         new AimService(),
                         new ActionBarService(),
                         new GoldService(),
                         new PatchNotesService(),
                         new PlayersService(),
                         new PoolService(),
                         new SdkService(),
                         new TutorialsService(),
                     })
                context.BindInstance(_initializableServices.Add(service));

            var world = new EcsWorld();
            
            foreach (var service in new object[]
                     {
                         _disposableServices = new DisposableServices(),
                         new BossHitPointBarService(),
                         new ComponentPools(world),
                         new DarkScreenService(),
                         new GlobalStateService(),
                         new HeightMapService(),
                         new MyLocationService(),
                         new SettingsService(),
                         new TimeScaleService(),
                         new UILoadingProgressService(),
                         new UiFocusableService(),
                         world,
                     })
                context.BindInstance(service);

            _initializableServices.Initialize(context);
            InitializeDependencies(projectDependencies.transform);
            _initializableServices.Start();
        }

        private void InitializeDependencies(Transform projectDependencies)
        {
            EnableProjectDependencies(projectDependencies);

            if (!string.IsNullOrEmpty(_nextScene))
                SceneManager.LoadScene(_nextScene);
        }

        private void EnableProjectDependencies(Transform projectDependencies)
        {
            DontDestroyOnLoad(projectDependencies);
            projectDependencies.gameObject.SetActive(true);

#if UNITY_EDITOR
            int index = 0;
            foreach (var go in gameObject.scene.GetRootGameObjects().OrderBy(i => i.name))
                go.transform.SetSiblingIndex(index++);
#endif
        }

        private void OnDestroy() => _disposableServices.Dispose();

        private class InitializableServices
        {
            private readonly List<IInitializableService> _list = new(16);

            public T Add<T>(T service) where T : IInitializableService
            {
                _list.Add(service);
                return service;
            }

            public void Initialize(Context context)
            {
                foreach (var service in _list)
                    service.InitializeService(context);
            }

            public void Start()
            {
                foreach (var service in _list)
                    service.Start();
            }
        }
    }
}