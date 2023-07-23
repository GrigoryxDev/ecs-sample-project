using System.Collections.Generic;
using System.Numerics;
using Core.ECSlogic.Extensions;

namespace Core.ECSlogic.Interfaces.Services
{
    public interface IMovablePositionsService : IInitService
    {
        bool IsMovableGridPosition(Vector2Int position);
        List<Vector2Int> GetAllGridPositions();
        Vector2Int GetRandomGridPosition();
        Vector2 GetWorldPosition(Vector2Int gridPosition);
    }
}