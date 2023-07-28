using Core.ECSlogic.Extensions;
using Core.ECSlogic.Models;
using Leopotam.EcsLite;

namespace Core.ECSlogic.Services
{
    public class MapElementsBuilder : BaseMapElementsBuilder
    {
        public MapElementsBuilder(EcsWorld world, InitLogicServices initServices) : base(world, initServices)
        {
        }

        public virtual ref T Create<T>(Vector2Int gridPosition) where T : struct
        {
            var entity = CreateMapElement(gridPosition);

            ref var component = ref world.GetPool<T>().Add(entity);

            return ref component;
        }
    }
}