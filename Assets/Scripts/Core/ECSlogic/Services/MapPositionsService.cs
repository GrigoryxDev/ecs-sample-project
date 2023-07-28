using System.Collections.Generic;
using Core.ECSlogic.Components;
using Core.ECSlogic.Extensions;
using Core.ECSlogic.Interfaces.Services;
using Core.ECSlogic.Models;
using Leopotam.EcsLite;

namespace Core.ECSlogic.Services
{
    public class MapPositionsService
    {
        private readonly IRandomService randomService;
        private readonly IMovablePositionsService movablePositionsService;

        private readonly EcsFilter mapElementsFilter;
        private readonly EcsPool<MapElement> mapElements;

        public MapPositionsService(EcsWorld world, InitLogicServices initLogic)
        {
            randomService = initLogic.RandomService;
            movablePositionsService = initLogic.MovablePositionsService;

            mapElements = world.GetPool<MapElement>();
            mapElementsFilter = world.Filter<MapElement>().Exc<ExpiredTag>().End();
        }

        public bool TryGetRndFreeGridPosition(out Vector2Int gridPosition)
        {
            gridPosition = default;
            var gridPositions = GetFreeGridPositions();
            if (gridPositions.Count > 0)
            {
                gridPosition = randomService.GetRandomElement(gridPositions);
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Vector2Int> GetFreeGridPositions()
        {
            var result = new List<Vector2Int>();
            result.AddRange(movablePositionsService.GetAllGridPositions());

            foreach (var entity in mapElementsFilter)
            {
                var mapElement = mapElements.Get(entity);
                result.Remove(mapElement.GridPosition);
            }
            return result;
        }
    }
}