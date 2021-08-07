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

        public override void Act(IControllable controllable, IChaseable target)
        {
            // TODO: Don't pass in target to Act() anymore
            if (_target == null)
            {
                // TODO  _target == null does not work?
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