using Core.ECSlogic.Components;
using Core.ECSlogic.Extensions;
using Core.ECSlogic.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.ECSlogic.Systems
{
    public class PlayerFactorySystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CreatePlayer>> createPlayerFilter;
        private readonly EcsPoolInject<ExpiredTag> expiredTagPool;

        private readonly EcsCustomInject<MapElementsBuilder> mapElementsBuilder;
        private readonly EcsCustomInject<MapPositionsService> mapPositionsService;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in createPlayerFilter.Value)
            {
                if (mapPositionsService.Value.TryGetRndFreeGridPosition(out var gridPos))
                {
                    ref var player = ref mapElementsBuilder.Value.Create<PlayerTag>(gridPos);
                }

                expiredTagPool.Value.GetOrAdd(entity);
            }
        }
    }
}