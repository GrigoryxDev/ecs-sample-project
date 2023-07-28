using Core.ECSlogic.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
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

                mapElement.WorldPosition = Vector2.Lerp(mapElement.WorldPosition, moveTarget.WorldPosition, moveTarget.CurrentT);
                const float deltaT = 0.0166667f;
                moveTarget.CurrentT += deltaT;

                if (Vector2.Distance(mapElement.WorldPosition, moveTarget.WorldPosition) < 0.1f)
                {
                    mapElement.GridPosition = moveTarget.GridPosition;
                    movableMapElements.Pools.Inc2.Del(item);
                }
            }
        }
    }
}