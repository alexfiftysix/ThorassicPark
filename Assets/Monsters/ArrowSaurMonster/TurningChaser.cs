using Extensions;
using GameManagement;
using Monsters.Brains;
using UnityEngine;
using Utilities.Extensions;
using Visitors;

namespace Monsters.ArrowSaurMonster
{
    public class TurningChaser : MonoBehaviour
    {
        private IMoveable _moveable;
        private IRotateable _rotateable;
        private IChaseable _target;
        private ChaseableManager _chaseableManager;
        private Transform _transform;

        private void Start()
        {
            _moveable = GetComponent<IMoveable>();
            _rotateable = GetComponent<IRotateable>();
            _chaseableManager = FindObjectOfType<ChaseableManager>();
            gameObject.AddTimer(2, FindTarget);
            _transform = transform;
        }

        public void Update()
        {
            if (_target == null)
            {
                _target = _chaseableManager.GetRandom();
                return;
            }
            
            // rotation
            var currentAngle = _transform.eulerAngles.z;
            var targetAngle = _transform.position.ToVector2().AngleTo(_target.Position);
            var rotationDegrees = Mathf.DeltaAngle(currentAngle, targetAngle);
            _rotateable.Rotate(rotationDegrees, 1);
            
            // speed
            _moveable.Move(Vector2.up, 1);
        }
        
        private void FindTarget()
        {
            _target = _chaseableManager.GetRandom();
        }
    }
}