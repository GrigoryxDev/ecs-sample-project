using Core.ECSlogic.Components;
using Core.ECSlogic.Extensions;
using Core.ECSlogic.Interfaces.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.ECSlogic.Systems
{
    public class MoveInputSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<InputMoveTarget>, Exc<ExpiredTag>> inputMoveTargets;
        private readonly EcsFilterInject<Inc<PlayerTag, MapElement>, Exc<ExpiredTag>> playerTags;

        private readonly EcsPoolInject<ExpiredTag> expiredTagPool;
        private readonly EcsPoolInject<MovePathTarget> movePathTargetPool;
        private readonly EcsCustomInject<IPathfindingService> pathfindingService;

        public void Run(IEcsSystems systems)
        {
            foreach (var item in inputMoveTargets.Value)
            {
                var inputMoveTarget = inputMoveTargets.Pools.Inc1.Get(item);

                if (playerTags.Value.TryGetAnyEntity(out var playerEntity))
                {
                    var playerMapElement = playerTags.Pools.Inc2.Get(playerEntity);
                    var path = pathfindingService.Value.GetPath(playerMapElement.GridPosition,
                     inputMoveTarget.GridPosition);

                    ref var movePathTarget = ref movePathTargetPool.Value.GetOrAdd(playerEntity);
                    movePathTarget.Path = path;
                    movePathTarget.CurrentIndex = 0;
                }

                expiredTagPool.Value.GetOrAdd(item);
            }
        }
    }
}