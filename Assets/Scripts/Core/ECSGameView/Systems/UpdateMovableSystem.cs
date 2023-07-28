using Core.ECSGameView.Components;
using Core.ECSlogic.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Core.ECSGameView.Helpers;

namespace Core.ECSGameView.Systems
{
    public class UpdateMovableSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<MapElement, ViewMapElement>, Exc<ExpiredTag>> movableMapElements;

        public void Run(IEcsSystems systems)
        {
            foreach (var item in movableMapElements.Value)
            {
                var mapElement = movableMapElements.Pools.Inc1.Get(item);
                var viewMapElement = movableMapElements.Pools.Inc2.Get(item);

                viewMapElement.MapView?.UpdateWorldPosition(mapElement.WorldPosition.AdaptToVector2());
            }
        }
    }
}