using GameManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities.Extensions;
using Visitors;

namespace Player
{
    public class PlayerController : Chaseable
    {
        public float speed = 0.01f;
        private int _health = 10;
        public GameManager manager;

        [SerializeField] private Vector2 direction = Vector2.zero;

        private Transform _transform;

        private void Start()
        {
            _transform = transform;
        }

        void Update()
        {
            Move();
        }
    
        private void Move()
        {
            var oldPosition = (Vector2) transform.position;
            var movement = direction * speed;
            var newPosition = new Vector2(oldPosition.x + movement.x, oldPosition.y + movement.y);
            _transform.position = newPosition.ToVector3();
        }

        private void OnMove(InputValue inputValue)
        {
            direction = inputValue.Get<Vector2>();
        }

        public override bool TakeDamage(int damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                Die();
                return true;
            }

            return false;
        }

        public override bool IsDead()
        {
            return _health <= 0;
        }

        private void Die()
        {
            manager.GameOver();
            Destroy(gameObject);
        }
    }
}
