
using System;
using System.Collections.Generic;
using Core.ECSlogic.Extensions;
using Core.ECSlogic.Interfaces.Services;

namespace Core.ECSlogic.Services.Pathfinding
{
    public class PathfindingService : IPathfindingService
    {
        private readonly IMovablePositionsService movablePositionsService;
        private readonly Pathfinder pathfinder = new();

        public PathfindingService(IMovablePositionsService movablePositionsService)
        {
            this.movablePositionsService = movablePositionsService;
            pathfinder.UpdateGridPositions(movablePositionsService.GetAllGridPositions());
        }

        public List<Vector2Int> GetPath(Vector2Int startGridPosition, Vector2Int targetGridPosition)
        {
            var resultPath = new List<Vector2Int>();

            if (movablePositionsService.IsMovableGridPosition(startGridPosition) &&
            movablePositionsService.IsMovableGridPosition(targetGridPosition))
            {
                var tilesPath = pathfinder.FindPath(startGridPosition, targetGridPosition);

                foreach (var tile in tilesPath)
                {
                    resultPath.Add(tile.GridLocation);
                }
            }

            return resultPath;
        }
    }
}