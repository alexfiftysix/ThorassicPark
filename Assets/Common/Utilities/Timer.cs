using System;
using UnityEngine;

namespace Common.Utilities
{
    public class Timer : MonoBehaviour
    {
        private RangedFloat _intervalTime;
        private float _maxTime;
        private Action _delegate;
        private float _timePassed;
        public bool isActive = true;

        public void Init(RangedFloat intervalTime, Action @delegate)
        {
            _intervalTime = intervalTime;
            _delegate = @delegate;
            _maxTime = _intervalTime.GetRandomValue();
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
            if (HasPassed() && isActive)
            {
                _delegate();
            }
        }

        private bool HasPassed()
        {
            _timePassed = _timePassed + Time.deltaTime;

            if (_timePassed > _maxTime)
            {
                _timePassed -= _maxTime;
                _maxTime = _intervalTime.GetRandomValue();
                return true;
            }

            return false;
        }
    }
}