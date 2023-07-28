using Core.ECSlogic.Models;
using Core.ECSlogic.Services;
using Core.ECSlogic.Systems;
using Core.WorldBuilders;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.ECSlogic
{
    public class LogicECSWorker : BaseECSWorker
    {
        private readonly WorldModel worldModel;
        private readonly InitLogicServices initServices;

        public LogicECSWorker(WorldModel worldModel, InitLogicServices initServices)
        {
            this.worldModel = worldModel;
            this.initServices = initServices;
        }

        public override void InitSystems(IEcsSystems systems)
        {
            systems
            .Add(new RestartSystem())

            .Add(new PlayersResetSystem())
            .Add(new ElementsResetSystem())

            .Add(new QuestObservableSystem())

            .Add(new PlayerFactorySystem())
            .Add(new ElementFactorySystem())

            .Add(new MoveInputSystem())
            .Add(new UpdateMovePathSystem())
            .Add(new MovementSystem())

            .Add(new CollectElementSystem())
            .Add(new ElementObservableSystem())


            .Inject(worldModel, initServices.RandomService,
              initServices.QuestDynamicService, initServices.ElementAnalyzeService,
              initServices.MovablePositionsService,
              new MapElementsService(world, initServices),
              new MapElementsBuilder(world, initServices),
              new RandomCollectableBuilder(world, initServices),
              new MapPositionsService(world, initServices))
            .Init();
        }
    }
}