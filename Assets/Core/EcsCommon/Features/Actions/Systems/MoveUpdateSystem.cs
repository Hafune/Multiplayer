using System.Runtime.CompilerServices;
using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;
using UnityEngine;

namespace Core.Systems
{
    public class MoveUpdateSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                MoveUpdateComponent,
                MoveDestinationComponent,
                PositionComponent,
                MoveDirectionComponent,
                MoveSpeedValueComponent,
                RigidbodyComponent
            >,
            Exc<
                ActionOnMoveCompleteComponent
            >> _filter;

        private readonly EcsFilterInject<
            Inc<
                MoveUpdateComponent,
                MoveDestinationComponent,
                PositionComponent,
                MoveDirectionComponent,
                MoveSpeedValueComponent,
                RigidbodyComponent,
                ActionOnMoveCompleteComponent
            >> _filterWithMoveComplete;

        private readonly EcsFilterInject<
            Inc<
                EnemyComponent,
                MoveUpdateComponent,
                MoveDestinationComponent,
                MoveSpeedValueComponent,
                TargetComponent
            >> _enemyFilter;

        private readonly ComponentPools _pools;
        private readonly EcsPoolInject<ActionCurrentComponent> _actionCurrentPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
                ProcessMove(i);

            foreach (var i in _filterWithMoveComplete.Value)
                if (ProcessMove(i, _pools.ActionOnMoveComplete.Get(i).actionDistance))
                    _pools.EventMoveComplete.Add(i);

            foreach (var i in _enemyFilter.Value)
            {
                var position = _pools.Rigidbody.Get(i).rigidbody.position;
                ref var moveUpdate = ref _pools.MoveUpdate.Get(i);
                var distanceSqr = (moveUpdate.lastPosition - position).sqrMagnitude;
                moveUpdate.lastPosition = position;
                var speedValue = _pools.MoveSpeedValue.Get(i).value;
                var desiredDistance = speedValue * Time.deltaTime * .3f; //если пройдено меньше трети от запланированного

                if (distanceSqr >= desiredDistance * desiredDistance || speedValue == 0)
                    continue;

                ref var moveDestination = ref _pools.MoveDestination.Get(i);

                if (moveDestination.lastSideDirection == 0)
                    moveDestination.lastSideDirection = i % 2 == 0 ? 1 : -1;

                if (moveDestination.nextCalculateTime - Time.time <= 0)
                {
                    const float ignoreCalculationDelay = .35f;
                    moveDestination.nextCalculateTime = Time.time + ignoreCalculationDelay;
                    float desiredDistanceByTime = speedValue * ignoreCalculationDelay * .25f; //желаемое расстояние хотя бы четверть пути за

                    if ((moveUpdate.sideMoveStartPosition - position).sqrMagnitude < desiredDistanceByTime * desiredDistanceByTime)
                        moveDestination.lastSideDirection = -moveDestination.lastSideDirection;

                    moveUpdate.sideMoveStartPosition = position;
                }

                var lineToTarget = _pools.Rigidbody.Get(_pools.Target.Get(i).entity).rigidbody.position - position;
                var side = Vector2.Perpendicular(lineToTarget);
                moveDestination.position = position + side * moveDestination.lastSideDirection;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool ProcessMove(int i, float distance = 0f)
        {
            var dest = _pools.MoveDestination.Get(i);
            var rb = _pools.Rigidbody.Get(i).rigidbody;
            var position = rb.position;
            var line = dest.position - position;
            var moveUpdate = _pools.MoveUpdate.Get(i);
            var sqrtDistance = dest.distanceToSuccess + distance;
            sqrtDistance *= sqrtDistance;

            if (line.sqrMagnitude <= sqrtDistance)
            {
                _pools.ActionComplete.AddIfNotExist(i);
                return true;
            }

            var velocity = _pools.MoveDirection.Get(i).direction * _pools.MoveSpeedValue.Get(i).value;

            if (moveUpdate.checkWalls)
            {
                var hit = Physics2D.Linecast(
                    position,
                    position + velocity.normalized * moveUpdate.radius,
                    moveUpdate.layer);

                if (hit.distance != 0)
                {
                    _pools.ActionComplete.AddIfNotExist(i);
                    return false;
                }
            }

            rb.linearVelocity = velocity;
            return false;
        }
    }
}