using Core.ECSlogic.Components;
using Core.ECSlogic.Extensions;
using Core.ECSlogic.Interfaces.Services;
using Core.ECSlogic.Models;
using Leopotam.EcsLite;

namespace Core.ECSlogic.Services
{
    public class BaseMapElementsBuilder
    {
        protected readonly EcsWorld world;
        private readonly EcsPool<MapElement> mapElements;
        private readonly IMovablePositionsService movablePositionsService;

        public BaseMapElementsBuilder(EcsWorld world, InitLogicServices initServices)
        {
            this.world = world;
            mapElements = world.GetPool<MapElement>();
            movablePositionsService = initServices.MovablePositionsService;
        }

        protected int CreateMapElement(Vector2Int gridPosition)
        {
            var entity = world.NewEntity();
            ref var mapElement = ref mapElements.Add(entity);
            mapElement.GridPosition = gridPosition;
            mapElement.WorldPosition = movablePositionsService.GetWorldPosition(gridPosition);

            return entity;
        }
    }
}