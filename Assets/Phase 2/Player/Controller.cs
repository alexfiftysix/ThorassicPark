using Monsters.Brains;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Phase_2.Player
{
    public class Controller : MonoBehaviour
    {
        public float speed;
        
        private IMoveable _moveable;
        private Vector2 _direction;

        public void Start()
        {
            _moveable = GetComponent<IMoveable>();
            _direction = Vector2.zero;
        }

        /// <summary>
        /// Called by a PlayerInput 
        /// </summary>
        public void OnMove(InputValue inputValue)
        {
            _direction = inputValue.Get<Vector2>();
        }

        public void Update()
        {
            _moveable.Move(_direction, speed);
        }
    }
}