using Core.ECSlogic.Extensions;

namespace Core.ECSlogic.Services.Pathfinding
{
    public class PathSearchTile
    {
        public int G;
        public int H;
        public int F => G + H;

        public PathSearchTile Previous;
        public Vector2Int GridLocation;

        public PathSearchTile(Vector2Int gridLocation)
        {
            GridLocation = gridLocation;
        }
    }
}