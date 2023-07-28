using Core.ECSlogic.Components;
using Core.ECSlogic.Extensions;
using Core.ECSlogic.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.ECSlogic.Systems
{
    public class ElementFactorySystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CreateRandomElement>> createRandomElements;
        private readonly EcsPoolInject<ExpiredTag> expiredTagPool;

        private readonly EcsCustomInject<RandomCollectableBuilder> randomCollectableBuilder;
        private readonly EcsCustomInject<MapPositionsService> mapPositionsService;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in createRandomElements.Value)
            {
                var createRequest = createRandomElements.Pools.Inc1.Get(entity);
                for (int i = 0; i < createRequest.Count; i++)
                {
                    if (mapPositionsService.Value.TryGetRndFreeGridPosition(out var gridPos))
                    {
                        ref var newRndElement = ref randomCollectableBuilder.Value.Create(gridPos);
                    }
                    else
                    {
                        break;
                    }
                }

                expiredTagPool.Value.GetOrAdd(entity);
            }
        }
    }
}