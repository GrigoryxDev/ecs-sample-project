using Core.ECSGameView.Helpers;
using Core.ECSlogic.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System;
using System.Numerics;

namespace Core.ECSlogic.Systems
{
    public class MovementSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<MapElement, MoveTarget>, Exc<ExpiredTag>> movableMapElements;

        public void Run(IEcsSystems systems)
        {
            foreach (var item in movableMapElements.Value)
            {
                ref var mapElement = ref movableMapElements.Pools.Inc1.Get(item);
                ref var moveTarget = ref movableMapElements.Pools.Inc2.Get(item);

                const float speed = 1f;
                mapElement.WorldPosition = PositionHelper.MoveTowards(mapElement.WorldPosition, moveTarget.WorldPosition, speed * moveTarget.CurrentT);

                const float deltaT = 0.0166667f;
                moveTarget.CurrentT += deltaT;
                
                var distance = Vector2.Distance(mapElement.WorldPosition, moveTarget.WorldPosition);
                if (distance < float.Epsilon)
                {
                    mapElement.WorldPosition = moveTarget.WorldPosition;
                    mapElement.GridPosition = moveTarget.GridPosition;
                }
            }
        }
    }
}