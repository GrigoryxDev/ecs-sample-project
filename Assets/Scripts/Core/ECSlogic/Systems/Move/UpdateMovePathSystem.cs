using Core.ECSlogic.Components;
using Core.ECSlogic.Interfaces.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.ECSlogic.Systems
{
    public class UpdateMovePathSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<MoveTarget, MovePathTarget>, Exc<ExpiredTag>> moveFilter;
        private readonly EcsCustomInject<IMovablePositionsService> movePositionsService;

        public void Run(IEcsSystems systems)
        {
            foreach (var item in moveFilter.Value)
            {
                ref var moveTarget = ref moveFilter.Pools.Inc1.Get(item);
                ref var movePathTarget = ref moveFilter.Pools.Inc2.Get(item);

                if (movePathTarget.Path == null || movePathTarget.Path.Count == 0)
                {
                    moveFilter.Pools.Inc2.Del(item);
                    continue;
                }

                if (movePathTarget.CurrentIndex == movePathTarget.Path.Count - 1)
                {
                    moveFilter.Pools.Inc2.Del(item);
                    continue;
                }

                var currentPathPos = movePathTarget.Path[movePathTarget.CurrentIndex];
                if (currentPathPos == moveTarget.GridPosition)
                {
                    movePathTarget.CurrentIndex++;
                    moveTarget.GridPosition = movePathTarget.Path[movePathTarget.CurrentIndex];
                    moveTarget.WorldPosition = movePositionsService.Value.GetWorldPosition(moveTarget.GridPosition);
                }
            }
        }
    }
}