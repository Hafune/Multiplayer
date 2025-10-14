using System;
using System.Collections;
using System.Collections.Generic;
// using Com.LuisPedroFonseca.ProCamera2D;
using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Lib;
using UnityEngine;
using Core.Lib;
using Cysharp.Threading.Tasks;
using UnityEngine.UIElements;

namespace Core.Services
{
    [Serializable]
    public class GameplayStateService : MonoConstruct, IAbstractGlobalState
    {
        [SerializeField] private Entrances_Row _hubStartPosition;
        public event Action OnEnableState;
        public event Action OnDisableState;
        public event Action OnShowHud;
        public event Action OnHideHud;
        public event Action OnHideAllMenus;
        private bool _hideAllIfMoveOut;

        [NonSerialized] public bool canOpenMap = true;

        [SerializeField] private SceneField _mainMenuScene;
        private readonly List<ConvertToEntity> _playerCharacters = new();
        private GlobalStateService _globalStateService;
        private MyLocationService _mapTransitionsService;
        private InputService _inputService;
        private AddressableService _addressableService;
        // private ProCamera2D _proCamera2D;
        private ComponentPools _pool;
        private SelectCharacterServiceState _selectCharacterService;
        private PlayersService _playersService;
        private PauseResumeByKeyWrapper _groundClick;
        private PauseResumeCoroutineByCountWrapper _inputsWrapper;
        private EcsFilter _playerFilter;
        private SdkService _sdkService;

        public bool IsActiveState => _globalStateService.IsCurrentState(this);

        private void Awake()
        {
            _globalStateService = context.Resolve<GlobalStateService>();
            _mapTransitionsService = context.Resolve<MyLocationService>();
            _selectCharacterService = context.Resolve<SelectCharacterServiceState>();
            // _proCamera2D = context.Resolve<ProCamera2D>();
            _pool = context.Resolve<ComponentPools>();
            _playersService = context.Resolve<PlayersService>();
            _inputService = context.Resolve<InputService>();
            _sdkService = context.Resolve<SdkService>();

            _addressableService = context.Resolve<AddressableService>();
            _addressableService.OnSceneLoaded += () =>
            {
                foreach (var playerCharacter in _playerCharacters)
                    playerCharacter.GetComponent<Rigidbody2D>().simulated = true;
                ResumeInputs(_addressableService);
            };
            _addressableService.OnNextSceneWillBeLoad += () =>
            {
                PauseInputs(_addressableService);
                foreach (var playerCharacter in _playerCharacters)
                    playerCharacter.GetComponent<Rigidbody2D>().simulated = false;
            };

            _playerFilter = context
                .Resolve<EcsWorld>()
                .Filter<Player1UniqueTag>()
                .End();
        }

        public int FindPlayerEntity() => _playerFilter.GetFirst();

        private void Start()
        {
            var inputs = _inputService.GetInputs(0);
            var leftClick = inputs.LeftClick;
            var rightClick = inputs.RightClick;

            var settingsService = context.Resolve<SettingsService>();
            inputs.Settings.performed += _ => settingsService.ToggleView();

            _groundClick = new(() =>
            {
                leftClick.Disable();
                rightClick.Disable();
            }, () =>
            {
                leftClick.Enable();
                rightClick.Enable();
            });

            //Дикий костыль для обхода бага new InputSystem,
            //если включение InputAction происходит в том же кадре что и выключение всего PlayerActions то часть остаётся включенной 
            const string coroutineLockKey = "coroutineLockKey";

            IEnumerator Pause()
            {
                _inputService.DisableGameplayInputs();
                yield return null;
                _groundClick.Pause(coroutineLockKey);
            }

            IEnumerator Resume()
            {
                _inputService.EnableGameplayInputs();
                yield return null;
                _groundClick.Resume(coroutineLockKey);
            }

            _inputsWrapper = new(this, Pause, Resume);
            PauseInputs(this);
        }

        public void PlayerCompleteGame() => _addressableService.LoadSceneAsync(_mainMenuScene, true);

        public void EnableState()
        {
            if (!_globalStateService.ChangeActiveState(this))
                return;

            _sdkService.GameReady();
            for (int i = 0; i < _playersService.MaxPlayersCount; i++)
            {
                // var user = _selectCharacterService.UiUsers[i];
                // if (!user.isReadyToPlay)
                //     continue;

                _playerCharacters.Add(BuildCharacter(_playersService.PlayerMarkerPools[i],
                    _selectCharacterService.SelectedCharacterPrefabs[i]));
            }

            ResumeInputs(this);
            _mapTransitionsService.ChangeLocationByEntrance(_hubStartPosition, ApplyStartPosition);

            OnEnableState?.Invoke();
#if UNITY_EDITOR && POWER_TEST
            context.Resolve<GoldService>().TryChangeValue(10000000);
#endif
        }

        public void DisableState()
        {
            RemoveCharacterAndControls();
            OnDisableState?.Invoke();
        }

        public void PauseInputs(object key)
        {
            _inputsWrapper.Pause();
            LockGround(key);
        }

        public void ResumeInputs(object key)
        {
            _inputsWrapper.Resume();
            UnlockGround(key);
        }

        public void LockGround(object key) => _groundClick.Pause(key);

        public void UnlockGround(object key) => _groundClick.Resume(key);

        public void RestoreConsumables()
        {
            foreach (var playerCharacter in _playerCharacters)
                _pool.EventRestoreConsumables.AddIfNotExist(playerCharacter.RawEntity);
        }

        public void SetPlayerPosition(Entrances entrance)
        {
            _mapTransitionsService.ChangeLocationByEntrance(entrance, p => ApplyPlayerPosition(_playerCharacters[0], p));
            // _proCamera2D.CenterOnTargets();
        }

        public void HideAllMenus() => OnHideAllMenus?.Invoke();

        public void RegisterLockGrounds(VisualElement ele) =>
            ele.RegisterPointerEnterLeaveEvents(() => LockGround(ele), () => UnlockGround(ele));

        public void HideAllIfMoveOut()
        {
            if (_hideAllIfMoveOut)
                return;

            _hideAllIfMoveOut = true;
            WatchPlayerPosition().Forget();
        }

        public void ClearHideAllIfMoveOut() => _hideAllIfMoveOut = false;

        private void ApplyStartPosition(Vector3 position)
        {
            var step = Vector2.right * 5;
            var startPosition = (Vector2)position - step * (_playerCharacters.Count - 1);

            for (int i = 0; i < _playerCharacters.Count; i++)
            {
                ApplyPlayerPosition(_playerCharacters[i], startPosition + step * i);
                // _proCamera2D.AddCameraTarget(_playerCharacters[i].transform, 1f, 1f, 0, new Vector2(0, -18));
            }

            // _proCamera2D.CenterOnTargets();

            OnShowHud?.Invoke();
        }

        private void ApplyPlayerPosition(ConvertToEntity player, Vector3 position)
        {
            player!.transform.position = position;
            var body = player!.transform.GetComponent<Rigidbody2D>();
            body.position = position;
        }

        private void OpenMap()
        {
            if (!canOpenMap)
                return;

            PauseInputs(this);
        }

        private void RemoveCharacterAndControls()
        {
            // _proCamera2D.RemoveAllCameraTargets();

            foreach (var playerCharacter in _playerCharacters)
                Destroy(playerCharacter.gameObject);

            _playerCharacters.Clear();
            PauseInputs(this);
            OnHideHud?.Invoke();
        }

        private ConvertToEntity BuildCharacter(IEcsPool markerPool, ConvertToEntity prefab)
        {
            var player = context.Instantiate(prefab);
            DontDestroyOnLoad(player);
            markerPool.AddDefault(player.RawEntity);
            return player;
        }

        private async UniTaskVoid WatchPlayerPosition()
        {
            var t = _playerCharacters[0].transform;
            var startPosition = _playerCharacters[0].transform.position;

            while (_hideAllIfMoveOut && Vector3.Distance(startPosition, t.position) < 2)
                await UniTask.Yield(PlayerLoopTiming.FixedUpdate);

            if (_hideAllIfMoveOut)
                OnHideAllMenus?.Invoke();
            
            _hideAllIfMoveOut = false;
        }
    }
}