using System.Linq;
using UnityEngine;
using Utilities;
using Utilities.Extensions;
using Visitors;
using Random = UnityEngine.Random;

namespace Dinos
{
    public class ArrowSaur : MonoBehaviour
    {
        [SerializeField] private float speed = 1f;
        [SerializeField] private float rotationSpeed = 5;
        private Transform _transform;
        private Rigidbody2D _rigidbody2D;

        // Target
        [SerializeField] private Chaseable target;
        private const float FindTargetMaxDelay = 2;
        [SerializeField] private int damage = 1;

        [SerializeField] private float bouncePower = 0.1f;
        private float _bounceDelay;

        private AudioSource _biteSound;

        private void Start()
        {
            gameObject.AddTimer(FindTargetMaxDelay, FindTarget);
            gameObject.AddTimer(_bounceDelay, Bounce);
            _transform = transform;
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _bounceDelay = Random.Range(1f, 4f);
            _biteSound = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (!(target is null))
            {
                // TODO: This doesn't work. After a visitor dies, they're still tracked.
                // You could have a "Died" event from the visitor which dinos subscribe to when they start chasing a visitor?
                RotateTowardTarget(target.transform.position);
                MoveForwards();
            }
        }

        public void OnCollisionEnter2D(Collision2D other)
        {
            var chaseable = other.gameObject.GetComponent<Chaseable>();
            if (!(chaseable is null))
            {
                Bite(target);
            }
        }

        private void Bite(Chaseable target)
        {
            target.TakeDamage(damage);
            _biteSound.Play();
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
    
        private void FindTarget()
        {
            target = FindObjectsOfType<Chaseable>().Where(c => !c.IsDead()).ToList().RandomChoice(); // TODO: Change to IEnumerable extension
        }
    }
} 