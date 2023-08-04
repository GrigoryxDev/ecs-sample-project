using Core.ECSGameView.Models;
using Core.ECSGameView.Systems;
using Core.ECSlogic.Models;
using Core.WorldBuilders;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.ECSGameView
{
    public class ViewECSWorker : BaseECSWorker
    {
        private readonly InitView initServices;
        private readonly IReadOnlyGameModel readOnlyGameModel;

        public ViewECSWorker(InitView initServices,IReadOnlyGameModel readOnlyGameModel)
        {
            this.initServices = initServices;
            this.readOnlyGameModel=readOnlyGameModel;
        }


        public override void InitSystems(IEcsSystems systems)
        {
            systems
               .Add(new InputObservableSystem())

               .Add(new CreatePlayerSystem())
               .Add(new CreateCollectableElementSystem())

               .Add(new UpdateMovableSystem())

               .Add(new DestroyExpiredViewSystem())

               .Inject(initServices, readOnlyGameModel)
               .Init();
        }
    }
}