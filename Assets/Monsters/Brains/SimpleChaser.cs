using GameManagement;
using UnityEngine;
using Visitors;

namespace Monsters.Brains
{
    [CreateAssetMenu(menuName = "Brains/SimpleChaser")]
    public class SimpleChaser : Brain
    {
        public float speed;

        private IChaseable _target;
        private ChaseableManager _chaseableManager;

        public override void Initialise()
        {
            _chaseableManager = FindObjectOfType<ChaseableManager>();
        }

        public override void Act(IControllable controllable)
        {
            // TODO: Don't pass in target to Act() anymore
            if ((Object) _target == null)
            {
                FindTarget();
                return;
            }

            var direction = (_target.Position - controllable.Position).normalized;
            controllable.Move(direction, speed);
        }

        private void FindTarget()
        {
            _target = _chaseableManager.GetRandom();
        }
    }
}