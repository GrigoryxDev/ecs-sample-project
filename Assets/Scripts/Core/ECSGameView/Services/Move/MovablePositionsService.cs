using System.Collections.Generic;
using System.Linq;
using Core.ECSlogic.Interfaces.Services;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

using Vector2Num = System.Numerics.Vector2;
using Vector2IntNum = Core.ECSlogic.Extensions.Vector2Int;

namespace Core.ECSGameView.Services
{
    public class MovablePositionsService : MonoBehaviour, IMovablePositionsService
    {
        private IRandomService randomService;


        [SerializeField] private Tilemap tilemap;

        private readonly Dictionary<Vector2IntNum, Vector2Num> movablePositionMap = new();
        private readonly List<Vector2IntNum> allGridPositions = new();

        //Simplest way
        //Use different layer, tile colliders, or tile compare
        private readonly HashSet<string> movablePositionNames = new()
        {"tileGrass_roadCornerLL", "tileGrass_roadCornerLR","tileGrass_roadCornerUL",
        "tileGrass_roadCornerUR","tileGrass_roadCrossing","tileGrass_roadCrossingRound",
        "tileGrass_roadEast","tileGrass_roadNorth","tileGrass_roadSplitE",
        "tileGrass_roadSplitN","tileGrass_roadSplitS","tileGrass_roadSplitW"};

        [Inject]
        public void Constructor(IRandomService randomService)
        {
            this.randomService = randomService;
        }

        public void Init()
        {
            foreach (Vector3Int position in tilemap.cellBounds.allPositionsWithin)
            {
                var tile = tilemap.GetTile(position);
                if (tile != null && movablePositionNames.Contains(tile.name))
                {
                    Vector3 worldPosition = tilemap.GetCellCenterWorld(position);

                    var gridPosition = new Vector2IntNum(position.x, position.y);

                    movablePositionMap.Add(gridPosition,
                    new Vector2Num(worldPosition.x, worldPosition.y));

                    allGridPositions.Add(gridPosition);
                }
            }
        }

        public List<Vector2IntNum> GetAllGridPositions()
        {
            return allGridPositions;
        }

        public Vector2IntNum GetRandomGridPosition()
        {
            return randomService.GetRandomElement(allGridPositions);
        }

        public bool IsMovableGridPosition(Vector2IntNum position)
        {
            return movablePositionMap.ContainsKey(position);
        }

        public Vector2Num GetWorldPosition(Vector2IntNum gridPosition)
        {
            return movablePositionMap[gridPosition];
        }
    }
}