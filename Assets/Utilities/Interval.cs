using UnityEngine;

namespace Utilities
{
    public static class Interval
    {
        public static bool HasPassed(float maxTimeInSeconds, float timeSinceLastInterval, out float newTime)
        {
            newTime = timeSinceLastInterval + Time.deltaTime;

            if (newTime > maxTimeInSeconds)
            {
                newTime = 0;
                return true;
            }

            return false;
        } 
    }
}