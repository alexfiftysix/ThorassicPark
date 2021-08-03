using System;
using GameManagement;
using Phase_2.Player;
using UnityEngine;
using Visitors;

namespace Monsters.Zombie
{
    // TODO: Could turn this into a brain, and replace Update() with Think() (think called by the relevant Monster)
    public class NonTurningChaser : MonoBehaviour
    {
        private IMoveable _moveable;
        private Vector2 _direction;
        private Transform _transform;
        private IChaseable _target;
        private bool _parkIsBroken;
        private ChaseableManager _chaseableManager;

        public void Start()
        {
            _chaseableManager = FindObjectOfType<ChaseableManager>();
            _moveable = GetComponent<IMoveable>();
            _direction = Vector2.zero;
            _transform = gameObject.transform;

            var gameManager = FindObjectOfType<GameManager>();
            gameManager.OnParkBreaks += OnParkBreaks;
            _parkIsBroken = gameManager.phase != Phase.Building;
        }

        public void Update()
        {
            if (!_parkIsBroken) return;

            if (_target == null || _target.IsDestroyed) // TODO: Doesn't work
            {
                FindTarget();
            }

            TurnToPosition(_target.GetPosition());
            _moveable.Move(_direction);
        }

        private void TurnToPosition(Vector3 position)
        {
            _direction = (position - _transform.position).normalized;
        }

        private void OnParkBreaks(object sender, EventArgs args)
        {
            FindTarget();
            _parkIsBroken = true;
        }

        private void FindTarget()
        {
            _target = _chaseableManager.GetRandom();
        }
    }
}