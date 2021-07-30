using System.Linq;
using UnityEngine;
using Utilities.Extensions;
using Visitors;

namespace Monsters.ArrowSaurMonster
{
    public class ArrowSaur : MonoBehaviour
    {
        public MonsterStats monsterStats;

        // Target
        private Chaseable _target;
        private const float FindTargetMaxDelay = 2;

        private Transform _transform;
        
        private AudioSource _biteAudioSource;

        private void Start()
        {
            gameObject.AddTimer(FindTargetMaxDelay, FindTarget);
            _transform = transform;
            _biteAudioSource = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (_target != null)
            {
                // You could have a "Died" event from the visitor which dinos subscribe to when they start chasing a visitor?
                RotateTowardTarget(_target.transform.position);
                MoveForwards();
            }
        }

        public void OnCollisionEnter2D(Collision2D other)
        {
            var chaseable = other.gameObject.GetComponent<Chaseable>();
            if (chaseable != null)
            {
                Bite(chaseable);
            }
        }

        private void Bite(Chaseable biteTarget)
        {
            biteTarget.TakeDamage(monsterStats.meleeDamage);
            monsterStats.meleeSound.Play(_biteAudioSource);
        }

        private void RotateTowardTarget(Vector3 targetPosition)
        {
            var newDirection = targetPosition - _transform.position;
            var angle = Mathf.Atan2(newDirection.y, newDirection.x) * Mathf.Rad2Deg - 90;
            var rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, monsterStats.turnSpeed * Time.deltaTime);
        }

        private void MoveForwards()
        {
            _transform.position += _transform.up * (monsterStats.movementSpeed * Time.deltaTime);
        }
    
        private void FindTarget()
        {
            _target = FindObjectsOfType<Chaseable>().Where(c => !c.IsDead()).RandomChoice();
        }
    }
} 