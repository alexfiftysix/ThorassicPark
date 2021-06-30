using System.Collections.Generic;
using System.Linq;
using Phase_1.Builder.Buildings;
using UnityEngine;
using Utilities;

namespace Visitors
{
    public class Visitor: Chaseable
    {
        [SerializeField] private float speed = 0.002f;
        private Vector2 _direction = Vector2.down;

        private int _health = 10;

        private float _wanderingTime = 5f;
        private float _wanderingTimePassed;
        private VisitorState _state = VisitorState.Wandering;
        private Attraction _target;

        private float _enjoyingTime;
        private float _enjoyingTimePassed;
        
        private Transform _transform;

        private void Start()
        {
            _transform = transform;
        }

        // Update is called once per frame
        void Update()
        {
            if (_state == VisitorState.Wandering)
            {
                if (Interval.HasPassed(_wanderingTime, _wanderingTimePassed, out _wanderingTimePassed))
                {
                    _target = ChooseTarget();
                    if (_target is null) return;
                    _state = VisitorState.WalkingToAttraction;
                    return;
                }

                var wantsToTurn = MyRandom.CoinFlip(.005f);
                if (wantsToTurn)
                {
                    ChooseDirection();
                }
            }
            else if (_state == VisitorState.WalkingToAttraction)
            {
                TurnToTarget();
            }
            else if (_state == VisitorState.EnjoyingAttraction)
            {
                if (Interval.HasPassed(_enjoyingTime, _enjoyingTimePassed, out _enjoyingTimePassed))
                {
                    StartWandering();
                }
            }
            Move();
        }

        public void OnTriggerStay2D(Collider2D other)
        {
            if (_state == VisitorState.WalkingToAttraction && other.gameObject.GetComponent<ViewRadius>() == _target.viewRadius)
            {
                StartEnjoying();
            }
        }

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

        private Attraction ChooseTarget()
        {
            return FindObjectsOfType<Attraction>().Where(a => !a.isGhost).ToList().RandomChoice();
        }

        private void Move()
        {
            var oldPosition = (Vector2) transform.position;
            var movement = _direction * speed;
            var newPosition = new Vector3(oldPosition.x + movement.x, oldPosition.y + movement.y, -1f);
            _transform.position = newPosition;
        }

        private void ChooseDirection()
        {
            _direction = _directions.RandomChoice();
        }

        private void TurnToTarget()
        {
            _direction = _target.transform.position - transform.position;
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

        private void StartWandering()
        {
            _wanderingTimePassed = 0;
            _wanderingTime = Random.Range(3f, 8f);
            _state = VisitorState.Wandering;
            _target = null;
        }

        private void StartEnjoying()
        {
            _direction *= 0.5f;
            _enjoyingTimePassed = 0;
            _enjoyingTime = Random.Range(3f, 8f);
            _state = VisitorState.EnjoyingAttraction;
        }
    }
}