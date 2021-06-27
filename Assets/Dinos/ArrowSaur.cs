using System.Linq;
using UnityEngine;
using Utilities;
using Visitors;
using Random = UnityEngine.Random;

namespace Dinos
{
    public class ArrowSaur : MonoBehaviour
    {
        [SerializeField] private float speed = 1f;
        [SerializeField] private float rotationSpeed = 5;

        [SerializeField] private Chaseable target;
        private const float FindTargetMaxDelay = 1;
        private float _findTargetTimePassed;
        [SerializeField] private int damage = 1;  

        [SerializeField] private float bouncePower = 0.1f;
        private float _bounceDelay;
        private float _bounceTimePassed;

        private Transform _transform;
        private Rigidbody2D _rigidbody2D;

        private void Start()
        {
            _transform = transform;
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _bounceDelay = Random.Range(1f, 4f);
        }

        // Update is called once per frame
        private void Update()
        {
            if (target == null)
            {
                if (Interval.HasPassed(FindTargetMaxDelay, _findTargetTimePassed, out _findTargetTimePassed))
                {
                    target = FindTarget();
                }
            }
            else
            {
                RotateTowardTarget(target.transform.position);
                MoveForwards();
            }

            if (Interval.HasPassed(_bounceDelay, _bounceTimePassed, out _bounceTimePassed))
            {
                Bounce();
                _bounceDelay = Random.Range(0.2f, 2f);
            }
        }

        public void OnCollisionEnter2D(Collision2D other)
        {
            var chaseable = other.gameObject.GetComponent<Chaseable>();
            if (!(chaseable is null))
            {
                chaseable.TakeDamage(damage);
            }
        }

        private void Bounce()
        {
            var force = MyRandom.NormalVector2() * bouncePower;
            _rigidbody2D.AddForce(force, ForceMode2D.Impulse);
        }

        private void RotateTowardTarget(Vector3 targetPosition)
        {
            var newDirection = targetPosition - _transform.position;
            var angle = Mathf.Atan2(newDirection.y, newDirection.x) * Mathf.Rad2Deg - 90;
            var rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }

        private void MoveForwards()
        {
            _transform.position += _transform.up * (speed * Time.deltaTime);
        }
    
        private static Chaseable FindTarget()
        {
            return FindObjectsOfType<Chaseable>().Where(c => !c.IsDead()).ToList().RandomChoice();
        }
    }
}