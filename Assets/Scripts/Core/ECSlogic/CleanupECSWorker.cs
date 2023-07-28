using Core.ECSlogic.Systems.Cleanup;
using Core.WorldBuilders;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.ECSlogic
{
    public class CleanupECSWorker : BaseECSWorker
    {
        public override void InitSystems(IEcsSystems systems)
        {
            systems
                .Add(new CleanerSystem())
                .Inject()
                .Init();
        }
    }
}