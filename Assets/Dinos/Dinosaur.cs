using System.Collections.Generic;
using System.Linq;
using Extensions;
using Phase_1.Builder.Buildings;
using UnityEngine;
using Utilities;
using Visitors;

namespace Dinos
{
    public class Dinosaur : MonoBehaviour
    {
        public float speed = 0.008f;
        [SerializeField] private Attraction pen;

        [SerializeField] private Vector2 direction = Vector2.down;
        [SerializeField] private int damage = 1;
        private Bounds _penBounds;
        [SerializeField] private Chaseable target;

        [SerializeField] private DinoState state;
        private const float TouchDistance = 0.1f;

        private float _findTargetTimeInterval;
        private const int FindTargetMaxInterval = 1;

        private readonly List<Vector2> _directions = new List<Vector2>
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

        // Update is called once per frame
        void Update()
        {
            if (state == DinoState.Chilling)
            {
                if (pen.IsBroken())
                {
                    state = DinoState.Chasing;
                    return;
                }
                
                var wantsToTurn = MyRandom.CoinFlip(.005f);
                if (wantsToTurn)
                {
                    ChooseDirection();
                }
            }
            else if (state == DinoState.Chasing)
            {
                _findTargetTimeInterval += Time.deltaTime;
                if (_findTargetTimeInterval > FindTargetMaxInterval && (target is null || target.IsDead()))
                {
                    _findTargetTimeInterval = 0;
                    target = FindTarget();
                }
                if (target)
                {
                    TurnTowardTarget();
                    TryBite(target);
                }
            }

            Move();
        }

        private void TryBite(Chaseable biteTarget)
        {
            var distanceToTarget = Vector2.Distance(biteTarget.transform.position, transform.position);
            if (distanceToTarget < TouchDistance * 1.1)
            {
                var targetKilled = biteTarget.TakeDamage(damage);
                if (targetKilled) target = null;
            }
        }

        private static Chaseable FindTarget()
        {
            return FindObjectsOfType<Chaseable>().Where(c => !c.IsDead()).ToList().RandomChoice();
        }

        private void TurnTowardTarget()
        {
            if (target.transform.position.y - transform.position.y > TouchDistance)
            {
                direction = Vector2.up;
            }
            else if (target.transform.position.x - transform.position.x > TouchDistance)
            {
                direction = Vector2.right;
            }
            else if (transform.position.y - target.transform.position.y > TouchDistance )
            {
                direction = Vector2.down;
            }
            else if (transform.position.x - target.transform.position.x > TouchDistance)
            {
                direction = Vector2.left;
            }
            else
            {
                direction = Vector2.zero;
            }
        }


        private void Move()
        {
            var oldPosition = transform.position.ToVector2();
            var movement = direction * speed;
            var newPosition = new Vector3(oldPosition.x + movement.x, oldPosition.y + movement.y, -2);

            if (pen.IsBroken() || IsWithinPen(newPosition))
            {
                transform.position = newPosition;
            }
        }

        private bool IsWithinPen(Vector2 dinosaurPosition)
        {
            var min = _penBounds.min;
            var max = _penBounds.max;


            var x = dinosaurPosition.x;
            var y = dinosaurPosition.y;

            return !(x < min.x) && !(x > max.x) && !(y < min.y) && !(y > max.y);
        }

        public void SetPen(GameObject newPen)
        {
            pen = newPen.GetComponent<Attraction>();
            _penBounds = newPen.GetComponent<Renderer>().bounds;
        }

        private void ChooseDirection()
        {
            direction = _directions.RandomChoice();
        }
    }
}