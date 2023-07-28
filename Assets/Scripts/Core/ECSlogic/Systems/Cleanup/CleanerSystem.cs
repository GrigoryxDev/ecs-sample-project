using Core.ECSlogic.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.ECSlogic.Systems.Cleanup
{
    public class CleanerSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<ExpiredTag>> expireds = default;
        private readonly EcsWorldInject world = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in expireds.Value)
            {
                world.Value.DelEntity(entity);
            }
        }
    }
}