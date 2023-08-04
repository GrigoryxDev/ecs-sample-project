using Core.ECSGameView.Components;
using Core.ECSGameView.Helpers;
using Core.ECSGameView.Models;
using Core.ECSlogic.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.ECSGameView.Systems
{
    public class CreatePlayerSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<PlayerTag, MapElement>, Exc<ExpiredTag>> playerFilter;
        private readonly EcsPoolInject<ViewMapElement> viewMapElementPool;
        private readonly EcsCustomInject<InitView> initViewServices;


        public void Run(IEcsSystems systems)
        {
            foreach (var item in playerFilter.Value)
            {
                if (!viewMapElementPool.Value.Has(item))
                {
                    ref var viewMapElement = ref viewMapElementPool.Value.Add(item);
                    var viewElment = initViewServices.Value.GetMapElementsFactory.GetPlayerFromPool();

                    var mapElement = playerFilter.Pools.Inc2.Get(item);
                    viewElment.UpdateWorldPosition(mapElement.WorldPosition.AdaptToVector2());

                    viewMapElement.MapView = viewElment;
                }
            }
        }
    }
}