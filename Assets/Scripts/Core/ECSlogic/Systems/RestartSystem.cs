using Core.ECSlogic.Components;
using Core.ECSlogic.Models;
using Core.ECSlogic.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.ECSlogic.Systems
{
    public class RestartSystem : IEcsRunSystem
    {
        private readonly EcsCustomInject<EcsWorldModel> worldModel;
        private readonly EcsCustomInject<MapElementsService> mapElementsService;

        private readonly EcsPoolInject<CreatePlayer> createPlayerPool;
        private readonly EcsPoolInject<RequestNewQuest> requestNewQuestPool;

        private readonly EcsPoolInject<ClearExistElements> clearExistElementsPool;
        private readonly EcsPoolInject<ClearExistPlayers> clearExistPlayersPool;

        private readonly EcsWorldInject ecsWorld;


        public void Run(IEcsSystems systems)
        {
            if (worldModel.Value.IsNeedRestart)
            {
                worldModel.Value.IsNeedRestart = false;

                ClearOld();

                CreateNew();

                RequestNewQuests();
            }
        }

        private void RequestNewQuests()
        {
            var request = ecsWorld.Value.NewEntity();
            requestNewQuestPool.Value.Add(request);
        }

        private void CreateNew()
        {
            var newPlayer = ecsWorld.Value.NewEntity();
            createPlayerPool.Value.Add(newPlayer);

            mapElementsService.Value.CreateRndElementsRequest();
        }

        private void ClearOld()
        {
            var clearPlayers = ecsWorld.Value.NewEntity();
            clearExistPlayersPool.Value.Add(clearPlayers);

            var clearElements = ecsWorld.Value.NewEntity();
            clearExistElementsPool.Value.Add(clearElements);
        }
    }
}