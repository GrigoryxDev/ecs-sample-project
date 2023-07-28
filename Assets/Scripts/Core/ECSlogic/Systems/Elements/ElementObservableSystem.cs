using Core.ECSlogic.Components;
using Core.ECSlogic.Extensions;
using Core.ECSlogic.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.ECSlogic.Systems
{
    public class ElementObservableSystem : IEcsRunSystem
    {
        private readonly EcsCustomInject<MapElementsService> mapElementsService;
        private readonly EcsFilterInject<Inc<CollectableElement>, Exc<ExpiredTag>> collectableElementFilter;

        public void Run(IEcsSystems systems)
        {
            if (!collectableElementFilter.Value.TryGetAnyEntity(out _))
            {
                mapElementsService.Value.CreateRndElementsRequest();
            }
        }
    }
}