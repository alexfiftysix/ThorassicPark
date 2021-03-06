using Characters.Brains;
using Common.Utilities;
using Common.Utilities.Extensions;
using UnityEngine;

namespace Characters.Player
{
    public class Wanderer : MonoBehaviour
    {
        private IMoveable _moveable;
        private Vector2 _direction;

        public void Start()
        {
            _moveable = GetComponent<IMoveable>();
            _direction = Directions.directions.RandomChoice();
            gameObject.AddTimer(new RangedFloat(1, 3), Turn);
        }

        public void Update()
        {
            _moveable.Move(_direction, 1);
        }

        private void Turn()
        {
            _direction = Directions.directions.RandomChoice();
        }
    }
}