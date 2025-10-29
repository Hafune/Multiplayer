using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;
using UnityEngine;

namespace Core.Systems
{
    public class Magnet2DSystem : IEcsRunSystem
    {
        private readonly float _acceleration = 10f;
        private readonly float _maxSpeed = 15;
        private readonly float _maxFreeSpeed = 5;

        private readonly EcsFilterInject<
            Inc<
                MagnetTag,
                Rigidbody2DComponent,
                EventMagnetAreaTouch>,
            Exc<
                InProgressTag<MagnetTag>
            >> _filter;

        private readonly EcsFilterInject<
            Inc<
                InProgressTag<MagnetTag>,
                Rigidbody2DComponent
            >> _progressFilter;

        private readonly EcsFilterInject<
            Inc<
                Player1UniqueTag,
                ModuleContainerComponent
            >> _playerFilter;

        private readonly EcsFilterInject<Inc<EventMagnetAreaTouch>> _eventTouchFilter;

        private readonly EcsPoolInject<EventMagnetAreaTouch> _eventMagnetAreaTouchPool;
        private readonly EcsPoolInject<Rigidbody2DComponent> _rigidbodyPool;
        private readonly EcsPoolInject<InProgressTag<MagnetTag>> _progressPool;
        private readonly EcsPoolInject<ModuleContainerComponent> _transformCenterPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
                _progressPool.Value.Add(i);

            foreach (var i in _progressFilter.Value)
                UpdateEntity(i);

            foreach (var i in _eventTouchFilter.Value)
                _eventMagnetAreaTouchPool.Value.Del(i);
        }

        private void UpdateEntity(int entity)
        {
            var playerEntity = _playerFilter.Value.GetFirst();
            var transform = _transformCenterPool.Value.Get(playerEntity).transform;
            var rigidbody = _rigidbodyPool.Value.Get(entity).rigidbody2D;

            var position = transform.position;
            var line = (Vector2)position - rigidbody.position;
            var freeVelocity = rigidbody.linearVelocity - Physics2D.gravity * Time.deltaTime;

            if (freeVelocity.magnitude > _maxFreeSpeed)
                freeVelocity = freeVelocity.normalized * _maxFreeSpeed;

            var velocity = freeVelocity +
                           line.normalized * (rigidbody.linearVelocity.magnitude + _acceleration * Time.deltaTime);

            if (velocity.magnitude > _maxSpeed)
                velocity = velocity.normalized * _maxSpeed;

            rigidbody.linearVelocity = velocity;
        }
    }
}