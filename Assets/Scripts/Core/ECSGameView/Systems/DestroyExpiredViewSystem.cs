using Core.ECSGameView.Components;
using Core.ECSlogic.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.ECSGameView.Systems
{
    public class DestroyExpiredViewSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<ViewMapElement>, Exc<ExpiredTag>> movableMapElements;

        public void Run(IEcsSystems systems)
        {
            foreach (var item in movableMapElements.Value)
            {
                var viewMapElement = movableMapElements.Pools.Inc1.Get(item);

                viewMapElement.MapView?.SendDestroy();
            }
        }
    }
}