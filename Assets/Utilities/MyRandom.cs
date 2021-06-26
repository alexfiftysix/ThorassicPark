using System;

namespace Utilities
{
    public class MyRandom
    {
        public static bool CoinFlip(float chance = 0.5f)
        {
            return UnityEngine.Random.value < chance;
        }

        public static int RandomInt(float min, float max)
        {
            if (min > max)
            {
                throw new Exception($"Min should be smaller than max. Min: {min} | Max: {max}");
            }
            
            var range = max - min;

            return Convert.ToInt32(Math.Round(UnityEngine.Random.value * range + min));
        }
    }
}
