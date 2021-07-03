using System;
using UnityEngine;

namespace Utilities.Extensions
{
    public static class GameObjectExtensions
    {
        public static Timer AddTimer(this GameObject gameObject, float intervalTime, Action @delegate)
        {
            var timer = gameObject.AddComponent<Timer>(); 
            timer.Init(intervalTime, @delegate);
            return timer;
        }
    }
}