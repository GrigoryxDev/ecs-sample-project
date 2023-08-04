using System;
using Leopotam.EcsLite;

namespace Core.WorldBuilders
{
    public abstract class BaseECSWorker
    {
        protected IEcsSystems systems;
        protected EcsWorld world;

        public virtual void Init(EcsWorld world)
        {
            this.world = world;
            systems = new EcsSystems(world);

            InitSystems(systems);
        }

        public abstract void InitSystems(IEcsSystems systems);

        public virtual void Tick()
        {
            try
            {
                systems?.Run();
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogException(e);
            }
        }

        public virtual void Destroy()
        {
            if (systems != null)
            {
                systems.Destroy();
                systems.GetWorld().Destroy();
                systems = null;
            }
        }
    }
}