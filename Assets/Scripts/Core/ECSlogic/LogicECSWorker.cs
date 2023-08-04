using Core.ECSlogic.Interfaces.Services;
using Core.ECSlogic.Models;
using Core.ECSlogic.Services;
using Core.ECSlogic.Services.Pathfinding;
using Core.ECSlogic.Systems;
using Core.WorldBuilders;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.ECSlogic
{
    public class LogicECSWorker : BaseECSWorker
    {
        private readonly EcsWorldModel worldModel;
        private readonly InitLogicServices initServices;

        public LogicECSWorker(EcsWorldModel worldModel, InitLogicServices initServices)
        {
            this.worldModel = worldModel;
            this.initServices = initServices;
        }

        public override void InitSystems(IEcsSystems systems)
        {
            IPathfindingService pathfindingService = 
            new PathfindingService(initServices.MovablePositionsService);
            
            systems
            .Add(new EndGameObservableSystem())
            
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


            .Inject(worldModel, pathfindingService, initServices.RandomService,
              initServices.QuestDynamicService, initServices.ElementAnalyzeService,
              initServices.MovablePositionsService,
              initServices.ReadOnlyGameModel,
              new MapElementsService(world, initServices),
              new MapElementsBuilder(world, initServices),
              new RandomCollectableBuilder(world, initServices),
              new MapPositionsService(world, initServices))

            .Init();
        }
    }
}