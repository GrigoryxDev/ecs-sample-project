using System.Runtime.CompilerServices;
using UnityEngine;

namespace Core.ECSGameView.Helpers
{
    public static class PositionHelper
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

        public static System.Numerics.Vector2 MoveTowards(System.Numerics.Vector2 current, System.Numerics.Vector2 target, float maxDistanceDelta)
        {
            float toVector_x = target.X - current.X;
            float toVector_y = target.Y - current.Y;

            float sqdist = toVector_x * toVector_x + toVector_y * toVector_y;

            if (sqdist == 0 || (maxDistanceDelta >= 0 && sqdist <= maxDistanceDelta * maxDistanceDelta))
                return target;
            var dist = (float)System.MathF.Sqrt(sqdist);

            return new System.Numerics.Vector2(current.X + toVector_x / dist * maxDistanceDelta,
                current.Y + toVector_y / dist * maxDistanceDelta);
        }
    }
}
