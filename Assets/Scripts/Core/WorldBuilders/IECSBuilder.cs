using Leopotam.EcsLite;

namespace Core.WorldBuilders
{
    public interface IECSBuilder
    {
        void Init(EcsWorld world);
        void Tick();
        void Destroy();
    }
}