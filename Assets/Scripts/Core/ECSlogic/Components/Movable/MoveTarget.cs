using System.Numerics;
using Core.ECSlogic.Extensions;

namespace Core.ECSlogic.Components
{
    public struct MoveTarget
    {
        public Vector2Int GridPosition;
        public Vector2 WorldPosition;
        
        public float CurrentT;
    }
}