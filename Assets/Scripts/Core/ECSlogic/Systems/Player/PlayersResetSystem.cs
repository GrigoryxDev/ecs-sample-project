using Core.ECSlogic.Components;
using Core.ECSlogic.Extensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.ECSlogic.Systems
{
    public class PlayersResetSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<ClearExistPlayers>> clearExist;
        private readonly EcsFilterInject<Inc<PlayerTag>> playerTags;
        private readonly EcsPoolInject<ExpiredTag> expiredTagPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in clearExist.Value)
            {
                foreach (var playerEntity in playerTags.Value)
                {
                    expiredTagPool.Value.GetOrAdd(playerEntity);
                }
                expiredTagPool.Value.GetOrAdd(entity);
            }
        }
    }
}