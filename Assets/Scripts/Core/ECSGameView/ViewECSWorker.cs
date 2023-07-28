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

        public ViewECSWorker(InitView initServices)
        {
            this.initServices = initServices;
        }


        public override void InitSystems(IEcsSystems systems)
        {
            systems
               .Add(new InputObservableSystem())

               .Add(new CreatePlayerSystem())
               .Add(new CreateCollectableElementSystem())

               .Add(new UpdateMovableSystem())

               .Add(new DestroyExpiredViewSystem())

               .Inject(initServices)
               .Init();
        }
    }
}