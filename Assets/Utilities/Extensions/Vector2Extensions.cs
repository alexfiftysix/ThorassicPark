using UnityEngine;

namespace Utilities.Extensions
{
    public static class Vector2Extensions
    {
        public static Vector3 ToVector3(this Vector2 vector, float z = -1)
        {
            return new Vector3(vector.x, vector.y, z);
        }

        /// <summary>
        /// Gets angle from this position to a target position in degrees
        /// </summary>
        public static float AngleTo(this Vector2 position, Vector2 target)
        {
            var newDirection = target - position;
            return newDirection.ToAngle();
        }

        /// <summary>
        /// Turns a direction represented as a Vector2 into an angle in degrees
        /// </summary>
        public static float ToAngle(this Vector2 direction)
        {
            return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        }
    }
}
