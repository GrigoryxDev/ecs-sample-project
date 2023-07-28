using Core.ECSlogic.Components;
using Core.ECSlogic.Extensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.ECSlogic.Systems
{
    public class ElementsResetSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<ClearExistElements>> clearExistElements;
        private readonly EcsFilterInject<Inc<CollectableElement>> clearCollectableElements;
        private readonly EcsPoolInject<ExpiredTag> expiredTagPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in clearExistElements.Value)
            {
                foreach (var collectableEntity in clearCollectableElements.Value)
                {
                    expiredTagPool.Value.GetOrAdd(collectableEntity);
                }
                expiredTagPool.Value.GetOrAdd(entity);
            }
        }
    }
}