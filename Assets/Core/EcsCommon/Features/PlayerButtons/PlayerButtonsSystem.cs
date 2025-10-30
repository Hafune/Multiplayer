using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;
using Reflex;
using UnityEngine.InputSystem;

namespace Core.Systems
{
    public class PlayerButtonsSystem<USER_ID> : IEcsRunSystem where USER_ID : struct
    {
        private readonly EcsFilterInject<
            Inc<
                USER_ID
            >,
            Exc<
                InProgressTag<USER_ID>
            >> _activateFilter;

        private readonly EcsFilterInject<
            Inc<
                InProgressTag<USER_ID>
            >,
            Exc<
                USER_ID
            >> _deactivateFilter;

        private readonly EcsPoolInject<InProgressTag<USER_ID>> _progressPool;
        private readonly IButtonHandler[] _buttons;

        public PlayerButtonsSystem(Context context, PlayerInputs.PlayerActions actions)
        {
            var world = context.Resolve<EcsWorld>();

            _buttons = new IButtonHandler[]
            {
                new ButtonHandler<ButtonMoveTag>(world, actions.Move),
                new ButtonHandler<MouseLeftTag>(world, actions.MouseLeft),
                new ButtonHandler<MouseRightTag>(world, actions.MouseRight),
                new ButtonHandler<ButtonJumpTag>(world, actions.Space),
                new ButtonHandler<ButtonJumpForwardTag>(world, actions.SpaceForward),
                new ButtonHandler<ButtonForwardFTag>(world, actions.ForwardF),
                // new ButtonHandler<Button1Tag>(world, actions.Button1),
                // new ButtonHandler<Button2Tag>(world, actions.Button2),
                // new ButtonHandler<Button3Tag>(world, actions.Button3),
                // new ButtonHandler<Button4Tag>(world, actions.Button4),
                // new ButtonHandler<MouseLeftTag>(world, actions.LeftClick),
                // new ButtonHandler<MouseRightTag>(world, actions.RightClick),
                // new ButtonHandler<ButtonUseHealing>(world, actions.UseHealing),
                // new ButtonHandler<ButtonTeleport>(world, actions.TeleportToHub),
            };
        }

        public void Run(IEcsSystems systems)
        {
            for (int a = 0, iMax = _buttons.Length; a < iMax; a++)
                _buttons[a].Run();

            foreach (var i in _activateFilter.Value)
            {
                _progressPool.Value.Add(i);

                for (int a = 0, iMax = _buttons.Length; a < iMax; a++)
                    _buttons[a].ActivateInput(i);
            }

            foreach (var i in _deactivateFilter.Value)
            {
                _progressPool.Value.Del(i);

                for (int a = 0, iMax = _buttons.Length; a < iMax; a++)
                    _buttons[a].DeactivateInput(i);
            }
        }

        private interface IButtonHandler
        {
            void Run();
            void ActivateInput(int i);
            void DeactivateInput(int i);
        }

        private class ButtonHandler<B> : IButtonHandler
            where B : struct, IButtonComponent
        {
            private readonly EcsFilter _eventPerformedFilter;
            private readonly EcsFilter _eventCanceledFilter;
            private readonly EcsFilter _controllerFilter;

            private readonly EcsPool<EventButtonPerformed<B>> _eventPerformedPool;
            private readonly EcsPool<EventButtonCanceled<B>> _eventCanceledPool;
            private readonly EcsPool<B> _streamingPool;
            private readonly InputAction _inputAction;
            private bool _isPressed;
            private bool _eventsWasSent;

            internal ButtonHandler(EcsWorld world, InputAction inputAction)
            {
                _inputAction = inputAction;

                _controllerFilter = world.Filter<USER_ID>().End();
                _eventPerformedFilter = world.Filter<EventButtonPerformed<B>>().End();
                _eventCanceledFilter = world.Filter<EventButtonCanceled<B>>().End();

                _eventPerformedPool = world.GetPool<EventButtonPerformed<B>>();
                _eventCanceledPool = world.GetPool<EventButtonCanceled<B>>();
                _streamingPool = world.GetPool<B>();

                inputAction.performed += _ =>
                {
                    if (!_isPressed)
                        _isPressed = true;
                };

                inputAction.canceled += _ =>
                {
                    if (_isPressed)
                        _isPressed = false;
                };
            }

            public void Run()
            {
                foreach (var i in _eventPerformedFilter)
                    _eventPerformedPool.Del(i);

                foreach (var i in _eventCanceledFilter)
                    _eventCanceledPool.Del(i);

                if (!_isPressed && _eventsWasSent)
                {
                    foreach (var i in _controllerFilter)
                    {
                        _eventCanceledPool.Add(i);
                        _streamingPool.DelIfExist(i);
                    }
                    _eventsWasSent = false;
                }
                
                if (_isPressed && !_eventsWasSent)
                {
                    foreach (var i in _controllerFilter)
                    {
                        _eventPerformedPool.Add(i);
                        _streamingPool.Add(i);
                    }
                    _eventsWasSent = true;
                }
            }

            public void ActivateInput(int i)
            {
                if (_inputAction.WasPerformedThisFrame())
                {
                    _eventCanceledPool.DelIfExist(i);
                    _eventPerformedPool.AddIfNotExist(i);
                }

                if (_inputAction.IsPressed())
                    _streamingPool.AddIfNotExist(i);
            }

            public void DeactivateInput(int i)
            {
                if (!_streamingPool.Has(i))
                    return;

                _eventCanceledPool.AddIfNotExist(i);
                _eventPerformedPool.DelIfExist(i);
                _streamingPool.Del(i);
            }
        }
    }
}