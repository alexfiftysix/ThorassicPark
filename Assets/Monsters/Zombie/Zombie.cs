using System;
using GameManagement;
using Phase_2.Player;
using UnityEngine;
using Utilities.Extensions;
using Visitors;

namespace Monsters.Zombie
{
    public class Zombie : MonoBehaviour
    {
        public MonsterStats stats;
        public GameObject zombieBase;

        private Chaseable _target;
        private Vector2 _direction = Vector2.up;
        private Transform _transform;

        private bool _parkIsBroken = false;

        // Biting
        private AudioSource _biteAudioSource;
        private float _biteDelay = 1.5f;
        private float _biteTimePassed = 0;

        // Start is called before the first frame update
        private void Start()
        {
            var gameManager = FindObjectOfType<GameManager>();
            gameManager.OnParkBreaks += OnParkBreaks;
            _parkIsBroken = gameManager.phase != Phase.Building;

            _transform = zombieBase.transform;
            _biteAudioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (!_parkIsBroken) return;

            _biteTimePassed += Time.deltaTime;

            if (_target == null)
            {
                // TODO: If there are no visitors left, will this blow out?
                FindTarget();
                return;
            }

            TurnToTarget(_target.transform);
            Move();
        }

        private void Move()
        {
            var oldPosition = (Vector2) _transform.position;
            var movement = _direction * (stats.movementSpeed * Time.deltaTime);
            var newPosition = new Vector3(oldPosition.x + movement.x, oldPosition.y + movement.y, -1f);
            _transform.position = newPosition;
        }

        private void OnParkBreaks(object sender, EventArgs args)
        {
            // TODO: You could keep track of these in the gameManager, then you wouldn't need to do all this expensive find.
            //       Or make a new "VisitorManager" which could take a bit of the code-load out of GameManager
            FindTarget();
            _parkIsBroken = true;
        }

        private void FindTarget()
        {
            _target = FindObjectsOfType<Chaseable>().RandomChoice();
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Visitor")) // TODO: String comparison bad 
            {
                Zombify(other.gameObject);
            }
            else if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                Bite(other.gameObject.GetComponentInParent<PlayerController>());
            }
        }

        private void Zombify(GameObject visitor)
        {
            if (_biteTimePassed < _biteDelay) return;

            _biteTimePassed = 0;
            var position = visitor.transform.position;
            Destroy(visitor);
            Instantiate(zombieBase, position, Quaternion.identity);
        }

        private void Bite(PlayerController player)
        {
            if (_biteTimePassed < _biteDelay) return;

            _biteTimePassed = 0;
            stats.meleeSound.Play(_biteAudioSource);
            player.TakeDamage(stats.meleeDamage);
        }

        private void TurnToTarget(Transform target)
        {
            _direction = (target.position - _transform.position).normalized;
        }
    }
}