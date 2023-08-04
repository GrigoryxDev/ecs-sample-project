using Core.ECSlogic.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.ECSlogic.Systems
{
    public class EndGameObservableSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<MovePathTarget, MoveTarget>> moveFilter;

        private readonly EcsCustomInject<IReadOnlyGameModel> readOnlyGameModel;
        
        public void Run(IEcsSystems systems)
        {
            if (readOnlyGameModel.Value.GetState.Value == GameStates.End)
            {
                foreach (var item in moveFilter.Value)
                {
                    moveFilter.Pools.Inc1.Del(item);
                    moveFilter.Pools.Inc2.Del(item);
                }
            }
        }
    }
}