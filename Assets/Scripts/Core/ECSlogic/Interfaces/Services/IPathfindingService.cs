using System.Collections.Generic;
using System.Numerics;
using Core.ECSlogic.Extensions;

namespace Core.ECSlogic.Interfaces.Services
{
    public interface IPathfindingService
    {
        List<Vector2Int> GetPath(Vector2Int startGridPosition, Vector2Int targetGridPosition);
    }
}