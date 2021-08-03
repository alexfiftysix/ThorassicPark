using System;
using UnityEngine;
using Utilities;
using Utilities.Extensions;

namespace Phase_2.Player
{
    public class Brain : MonoBehaviour
    {
        private IMoveable _moveable;
        private Vector2 _direction;

        public void Start()
        {
            _moveable = GetComponent<IMoveable>();
            _direction = Vector2.zero;
            gameObject.AddTimer(new RangedFloat(1, 3), Turn);
        }

        public void Update()
        {
            _moveable.Move(_direction);
        }

        private void Turn()
        {
            _direction = Directions.directions.RandomChoice();
        }
    }
}