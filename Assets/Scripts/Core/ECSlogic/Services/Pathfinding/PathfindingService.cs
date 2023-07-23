
using System;
using System.Collections.Generic;
using Core.ECSlogic.Extensions;
using Core.ECSlogic.Interfaces.Services;

namespace Core.ECSlogic.Services.Pathfinding
{
    public class PathfindingService : IPathfindingService
    {
        private readonly IMovablePositionsService movablePositionsService;

        public PathfindingService(IMovablePositionsService movablePositionsService)
        {
            this.movablePositionsService = movablePositionsService;
        }

        private Dictionary<Vector2Int, PathSearchTile> searchableTiles;


        public List<Vector2Int> GetPath(Vector2Int startGridPosition, Vector2Int targetGridPosition)
        {
            var resultPath = new List<Vector2Int>();

            var tilesPath = FindPath(startGridPosition, targetGridPosition);

            foreach (var tile in tilesPath)
            {
                resultPath.Add(tile.GridLocation);
            }

            return resultPath;
        }

        public List<PathSearchTile> FindPath(Vector2Int startGridPos, Vector2Int endGridPos)
        {
            searchableTiles ??= GetSearchableTiles();

            PriorityQueue<PathSearchTile, int> openList = new();
            HashSet<PathSearchTile> closedList = new();

            var start = searchableTiles[startGridPos];
            var end = searchableTiles[endGridPos];

            openList.Enqueue(start, start.F);

            while (openList.Count > 0)
            {
                PathSearchTile currentPathSearchTile = openList.Dequeue();
                closedList.Add(currentPathSearchTile);

                if (currentPathSearchTile == end)
                {
                    return GetFinishedList(start, end);
                }

                foreach (var tile in GetNeighborPathSearchTiles(currentPathSearchTile))
                {
                    if (closedList.Contains(tile))
                    {
                        continue;
                    }

                    tile.G = GetManhattenDistance(start, tile);
                    tile.H = GetManhattenDistance(end, tile);

                    tile.Previous = currentPathSearchTile;

                    openList.Enqueue(tile, tile.F);
                }
            }

            return new List<PathSearchTile>();
        }

        private List<PathSearchTile> GetFinishedList(PathSearchTile start, PathSearchTile end)
        {
            List<PathSearchTile> finishedList = new List<PathSearchTile>();
            PathSearchTile currentTile = end;

            while (currentTile != start)
            {
                finishedList.Add(currentTile);
                currentTile = currentTile.Previous;
            }

            finishedList.Reverse();

            return finishedList;
        }

        private int GetManhattenDistance(PathSearchTile start, PathSearchTile tile)
        {
            return Math.Abs(start.GridLocation.X - tile.GridLocation.X) +
            Math.Abs(start.GridLocation.Y - tile.GridLocation.Y);
        }

        private List<PathSearchTile> GetNeighborPathSearchTiles(PathSearchTile current)
        {
            List<PathSearchTile> neighbors = new List<PathSearchTile>();

            void TryAdd(Vector2Int gridLocation)
            {
                if (searchableTiles.ContainsKey(gridLocation))
                {
                    neighbors.Add(searchableTiles[gridLocation]);
                }
            }

            var currentPos = current.GridLocation;

            //right
            Vector2Int locationToCheck = new Vector2Int(
                currentPos.X + 1,
                currentPos.Y
            );
            TryAdd(locationToCheck);

            //left
            locationToCheck = new Vector2Int(
                currentPos.X - 1,
                currentPos.Y
            );
            TryAdd(locationToCheck);

            //top
            locationToCheck = new Vector2Int(
                currentPos.X,
                currentPos.Y + 1
            );
            TryAdd(locationToCheck);

            //bottom
            locationToCheck = new Vector2Int(
                currentPos.X,
                currentPos.Y - 1
            );
            TryAdd(locationToCheck);

            return neighbors;
        }

        private Dictionary<Vector2Int, PathSearchTile> GetSearchableTiles()
        {
            var searchableTiles = new Dictionary<Vector2Int, PathSearchTile>();

            var allGridPositions = movablePositionsService.GetAllGridPositions();
            foreach (var gridPos in allGridPositions)
            {
                searchableTiles.Add(gridPos, new PathSearchTile(gridPos));
            }

            return searchableTiles;
        }
    }
}