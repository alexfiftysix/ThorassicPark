using Monsters.Brains.BrainStates;
using Phase_1.Builder.Buildings;
using UnityEngine;

namespace Monsters.Brains
{
    public abstract class ControllableBase : MonoBehaviour
    {
        private Transform _transform;

        public Vector3 EulerAngles => _transform.eulerAngles;
        public Vector3 Position => _transform.position;
        public Vector2 Direction { get; set; }
        public MonsterStats stats;
        public float TimeSinceLastDecision { get; set; }
        public float MaxDecisionTime { get; set; }
        public float WaitTime { get; set; }
        public float MaxWaitTime { get; set; }
        public Attraction Target { set; get; }
        
        public BrainState state;

        public virtual void Start()
        {
            _transform = transform;
            state.Initialise(this);
        }

        public virtual void Update()
        {
            state.DoActions(this);
        }

        public void Move(Vector2 direction, float speed = 1)
        {
            if (_transform == null) _transform = transform;
            _transform.position += _transform.up * (direction.y * speed * Time.deltaTime)
                                   + _transform.right * (direction.x * speed * Time.deltaTime);
        }

        public void Rotate(float degrees, float speed)
        {
            _transform.Rotate(Vector3.forward * (degrees * speed * Time.deltaTime));
        }

        public void TransitionToState(BrainState nextState)
        {
            if (nextState == state) return;

            state = nextState;
            state.Initialise(this);
        }
    }
}