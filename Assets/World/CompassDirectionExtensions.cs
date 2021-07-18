using System;
using System.Numerics;

namespace World
{
    public static class CompassDirectionExtensions
    {
        public static CompassDirection Opposite(this CompassDirection direction)
        {
            return direction switch
            {
                CompassDirection.North => CompassDirection.South,
                CompassDirection.South => CompassDirection.North,
                CompassDirection.East => CompassDirection.West,
                CompassDirection.West => CompassDirection.East,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }

        public static Vector2 ToVector2(this CompassDirection direction)
        {
            return direction switch
            {
                CompassDirection.North => new Vector2(0, 1),
                CompassDirection.South => new Vector2(0, -1),
                CompassDirection.East => new Vector2(1, 0),
                CompassDirection.West => new Vector2(0, 1),
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }
    }
}