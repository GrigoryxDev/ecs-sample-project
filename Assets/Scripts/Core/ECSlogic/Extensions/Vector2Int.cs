
using System;

namespace Core.ECSlogic.Extensions
{
    public readonly struct Vector2Int : IEquatable<Vector2Int>
    {
        public Vector2Int(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }

        public static Vector2Int operator +(Vector2Int a, Vector2Int b)
            => new Vector2Int(a.X + b.X, a.Y + b.Y);
        public static Vector2Int operator -(Vector2Int a, Vector2Int b)
            => new Vector2Int(a.X - b.X, a.Y - b.Y);
        public static bool operator ==(Vector2Int a, Vector2Int b)
            => a.X == b.X && a.Y == b.Y;
        public static bool operator !=(Vector2Int a, Vector2Int b)
            => !(a == b);

        public bool Equals(Vector2Int other)
            => X == other.X && Y == other.Y;

        public override bool Equals(object obj)
        {
            if (!(obj is Vector2Int))
                return false;

            var other = (Vector2Int)obj;
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
            => HashCode.Combine(X, Y);

        public override string ToString()
            => $"(X:{X}, Y:{Y})";
    }
}