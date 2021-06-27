using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Utilities
{
    public static class MyRandom
    {
        public static bool CoinFlip(float chance = 0.5f)
        {
            return Random.value < chance;
        }

        public static bool Percent(float percent)
        {
            return Random.value * 100 < percent;
        }

        public static int Int(float min, float max)
        {
            if (min > max)
            {
                throw new Exception($"Min should be smaller than max. Min: {min} | Max: {max}");
            }
            
            var range = max - min;

            return Convert.ToInt32(Math.Round(Random.value * range + min));
        }

        public static Vector2 NormalVector2()
        {
            return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        }
    }
}
