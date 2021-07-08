using System;
using JetBrains.Annotations;
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