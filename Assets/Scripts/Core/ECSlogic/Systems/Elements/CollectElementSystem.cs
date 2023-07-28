using Core.ECSlogic.Components;
using Core.ECSlogic.Extensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.ECSlogic.Systems
{
    public class CollectElementSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<PlayerTag, MapElement>, Exc<ExpiredTag>> playerTags;
        private readonly EcsFilterInject<Inc<CollectableElement, MapElement>, Exc<ExpiredTag>> collectableElements;
        private readonly EcsPoolInject<ExpiredTag> expiredTagPool;
        private readonly EcsCustomInject<IElementAnalyzeService> elementAnalyzeService;

        public void Run(IEcsSystems systems)
        {
            foreach (var playerEntity in playerTags.Value)
            {
                var playerMapElement = playerTags.Pools.Inc2.Get(playerEntity);
                foreach (var collectableEntity in collectableElements.Value)
                {
                    var collectableMapElement = collectableElements.Pools.Inc2.Get(collectableEntity);
                    if (playerMapElement.GridPosition == collectableMapElement.GridPosition)
                    {
                        var collectableElement = collectableElements.Pools.Inc1.Get(collectableEntity);
                        elementAnalyzeService.Value.Collect(collectableElement.ID, collectableElement.Count);
                        
                        expiredTagPool.Value.GetOrAdd(collectableEntity);
                    }
                }
            }
        }
    }
}