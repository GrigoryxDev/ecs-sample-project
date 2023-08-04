
using System;
using System.Collections.Generic;
using Core.ECSlogic.Extensions;

namespace Core.ECSlogic.Services.Pathfinding
{
    /// <summary>
    /// Works only with validated movable grid positions
    /// </summary>
    public class Pathfinder
    {
        private Dictionary<Vector2Int, PathSearchTile> searchableTiles;
        private List<Vector2Int> allGridPositions = new();

        public void UpdateGridPositions(List<Vector2Int> allGridPositions)
        {
            allGridPositions ??= new();
            this.allGridPositions = allGridPositions;
        }

        public List<PathSearchTile> FindPath(Vector2Int startGridPos, Vector2Int endGridPos)
        {
            searchableTiles ??= GetSearchableTiles(allGridPositions);

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

                var neighbors = GetNeighborPathSearchTiles(currentPathSearchTile);
                foreach (var tile in neighbors)
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

        public Dictionary<Vector2Int, PathSearchTile> GetSearchableTiles(List<Vector2Int> allGridPositions)
        {
            var searchableTiles = new Dictionary<Vector2Int, PathSearchTile>();

            foreach (var gridPos in allGridPositions)
            {
                searchableTiles.Add(gridPos, new PathSearchTile(gridPos));
            }

            return searchableTiles;
        }
    }
}