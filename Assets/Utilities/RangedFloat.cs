using UnityEngine;

// Nicked from https://github.com/heisarzola/Unity-Development-Tools/blob/master/Attributes/Ranged%20Float/RangedFloat.cs
namespace Utilities
{
    [System.Serializable]
    public class RangedFloat
    {
        public float min;
        public float max;

        public void Init()
        {
            min = 0;
            max = 1;
        }

        public RangedFloat()
        {
            Init();
        }

        public RangedFloat(float min, float max)
        {
            this.min = min;
            this.max = max;
        }

        public float GetRandomValue()
        {
            return Random.Range(min, max);
        }

        public override string ToString()
        {
            return $"[Class: {nameof(RangedFloat)}, Min: {min}, Max: {max}]";
        }
    }
}