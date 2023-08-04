using Core.ECSlogic.Components;
using Core.ECSlogic.Extensions;
using Core.ECSlogic.Interfaces.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.ECSlogic.Systems
{
    public class UpdateMovePathSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<MovePathTarget, MoveTarget, MapElement>, Exc<ExpiredTag>> moveFilter;
        private readonly EcsCustomInject<IMovablePositionsService> movePositionsService;

        public void Run(IEcsSystems systems)
        {
            foreach (var item in moveFilter.Value)
            {
                ref var movePathTarget = ref moveFilter.Pools.Inc1.Get(item);

                if (movePathTarget.Path == null || movePathTarget.Path.Count == 0)
                {
                    DelMoveComponents(item);
                    continue;
                }


                ref var moveTarget = ref moveFilter.Pools.Inc2.Get(item);
                var mapElement = moveFilter.Pools.Inc3.Get(item);

                if (mapElement.GridPosition == moveTarget.GridPosition)
                {
                    if (movePathTarget.CurrentIndex == movePathTarget.Path.Count - 1)
                    {
                        DelMoveComponents(item);
                    }
                    else
                    {
                        movePathTarget.CurrentIndex++;
                        moveTarget.CurrentT=0;
                        moveTarget.GridPosition = movePathTarget.Path[movePathTarget.CurrentIndex];
                        moveTarget.WorldPosition = movePositionsService.Value.GetWorldPosition(moveTarget.GridPosition);
                    }

                }
            }
        }

        private void DelMoveComponents(int entity)
        {
            moveFilter.Pools.Inc1.Del(entity);
            moveFilter.Pools.Inc2.Del(entity);
        }
    }
}