using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Common.Utilities.Extensions
{
    public static class GameObjectExtensions
    {
        public static Timer AddTimer(this GameObject gameObject, float intervalTime, Action @delegate)
        {
            return AddTimer(gameObject, new RangedFloat(intervalTime, intervalTime), @delegate);
        }
        
        public static Timer AddTimer(this GameObject gameObject, RangedFloat intervalTime, Action @delegate)
        {
            var timer = gameObject.AddComponent<Timer>(); 
            timer.Init(intervalTime, @delegate);
            return timer;
        }

        [CanBeNull]
        public static GameObject FindParent(this GameObject gameObject, string name)
        {
            if (gameObject.transform.parent.gameObject.name == name)
            {
                return gameObject.transform.parent.gameObject;
            }
            else if (gameObject.transform.parent == null)
            {
                return null;
            }

            return gameObject.transform.parent.gameObject.FindParent(name);
        }
    }
}