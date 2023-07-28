using UnityEngine;

namespace Core.ECSGameView.Helpers
{
    public static class PositionExtension
    {
        public static Vector2 AdaptToVector2(this System.Numerics.Vector2 position)
        {
            return new Vector2(position.X, position.Y);
        }

        public static System.Numerics.Vector2 AdaptToNumericsVector2(this Vector2 position)
        {
            return new System.Numerics.Vector2(position.x, position.y);
        }

        public static ECSlogic.Extensions.Vector2Int AdaptToExtensionVector2Int(this Vector2Int position)
        {
            return new ECSlogic.Extensions.Vector2Int(position.x, position.y);
        }
    }
}
