using System;
using UnityEngine;

namespace Utilities
{
    public class Timer : MonoBehaviour
    {
        private float _intervalTime;
        private Action _delegate;
        private float _timePassed;
        public bool isActive = true;

        public void Init(float intervalTime, Action @delegate)
        {
            _intervalTime = intervalTime;
            _delegate = @delegate;
        }

        public void Reset()
        {
            _timePassed = 0;
        }

        public void Activate()
        {
            Reset();
            isActive = true;
        }
        
        public void DeActivate()
        {
            isActive = false;
        }

        private void Update()
        {
            if (HasPassed(_intervalTime, _timePassed) && isActive)
            {
                _delegate();
            }
        }

        private bool HasPassed(float maxTimeInSeconds, float timeSinceLastInterval)
        {
            _timePassed = timeSinceLastInterval + Time.deltaTime;

            if (_timePassed > maxTimeInSeconds)
            {
                _timePassed -= maxTimeInSeconds;
                return true;
            }

            return false;
        }
    }
}