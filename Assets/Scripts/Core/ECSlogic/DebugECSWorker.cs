using Core.WorldBuilders;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.ECSlogic
{
    public class DebugECSWorker : BaseECSWorker
    {
        public override void InitSystems(IEcsSystems systems)
        {

#if UNITY_EDITOR
            systems
                    .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
                    .Add(new Leopotam.EcsLite.UnityEditor.EcsSystemsDebugSystem())
                    .Inject()
                    .Init();
#endif
        }
    }
}