using System;

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
    }
}