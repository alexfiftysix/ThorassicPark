using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace Visitors
{
    public class Visitor: Chaseable
    {
        public float speed = 0.002f;

        [SerializeField] private Vector2 direction = Vector2.down;

        private Transform _transform;
        private int _health = 10;

        
        
        // Return true if the Visitor has died
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
            return _health < 0;
        }

        private void Start()
        {
            _transform = transform;
        }

        // Update is called once per frame
        void Update()
        {
            var wantsToTurn = MyRandom.CoinFlip(.005f);
            if (wantsToTurn)
            {
                ChooseDirection();
            }

            Move();
        }

        private void Move()
        {
            var oldPosition = (Vector2) transform.position;
            var movement = direction * speed;
            var newPosition = new Vector2(oldPosition.x + movement.x, oldPosition.y + movement.y);
            _transform.position = newPosition;
        }

        private void ChooseDirection()
        {
            direction = _directions.RandomChoice();
        }

        private void Die()
        {
            Destroy(gameObject);
        }

        private readonly List<Vector2> _directions = new List<Vector2>()
        {
            Vector2.up,
            Vector2.down,
            Vector2.left,
            Vector2.right,
            new Vector2(1, 1),
            new Vector2(1, -1),
            new Vector2(-1, 1),
            new Vector2(-1, -1),
            Vector2.zero
        };
    }
}