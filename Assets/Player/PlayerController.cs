using GameManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Utilities.Extensions;
using Visitors;

namespace Player
{
    public class PlayerController : Chaseable
    {
        [SerializeField] private float speed = 0.01f;
        public GameManager manager;

        public Slider healthBar;
        private int _health = 10;

        [SerializeField] private Vector2 direction = Vector2.zero;

        private Transform _transform;

        private void Start()
        {
            _transform = transform;

            healthBar = Instantiate(healthBar, new Vector3(0, -20, 0), Quaternion.identity);
            healthBar.maxValue = _health;
            healthBar.value = _health;

            healthBar.transform.SetParent(GameObject.Find("Canvas").transform, false);
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
            healthBar.value = _health;
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
