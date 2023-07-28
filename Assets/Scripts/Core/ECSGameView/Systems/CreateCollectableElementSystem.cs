using Core.ECSGameView.Components;
using Core.ECSGameView.Models;
using Core.ECSlogic.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.ECSGameView.Systems
{
    public class CreateCollectableElementSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CollectableElement>, Exc<ExpiredTag>> collectableFilter;
        private readonly EcsPoolInject<ViewMapElement> viewMapElementPool;
        private readonly EcsCustomInject<InitView> initViewServices;


        public void Run(IEcsSystems systems)
        {
            foreach (var item in collectableFilter.Value)
            {
                if (!viewMapElementPool.Value.Has(item))
                {
                    var collectableElement = collectableFilter.Pools.Inc1.Get(item);

                    ref var viewMapElement = ref viewMapElementPool.Value.Add(item);
                    var viewElment = initViewServices.Value.GetMapElementsFactory.GetElementFromPool(collectableElement.ID);
                    viewMapElement.MapView = viewElment;
                }
            }
        }
    }
}