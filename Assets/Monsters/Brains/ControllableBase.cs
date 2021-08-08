using GameManagement;
using Monsters.Brains.BrainStates;
using Phase_1.Builder.Buildings;
using Phase_2.Player;
using UnityEngine;
using Visitors;

namespace Monsters.Brains
{
    public abstract class ControllableBase : MonoBehaviour
    {
        public CharacterStats characterStats;
        public BrainState state;

        private Transform _transform;

        public Vector3 EulerAngles => _transform.eulerAngles;
        public Vector3 Position => _transform.position;
        public Vector2 Direction { get; set; }
        public float TimeSinceLastDecision { get; set; }
        public float MaxDecisionTime { get; set; }
        public float WaitTime { get; set; }
        public float MaxWaitTime { get; set; }
        public Attraction TargetAttraction { set; get; }
        public IChaseable TargetChaseable { set; get; }
        public PlayerController Player { set; get; }
        
        public GameManager GameManager { get; private set; }
        public ChaseableManager ChaseableManager { get; set; }

        public virtual void Start()
        {
            _transform = transform;
            state.Initialise(this);
            GameManager = FindObjectOfType<GameManager>();
            ChaseableManager = FindObjectOfType<ChaseableManager>();
        }

        public virtual void Update()
        {
            state.DoActions(this);
        }

        public void Move(Vector2 direction, float speedMultiplier = 1)
        {
            if (_transform == null) _transform = transform;
            _transform.position +=
                _transform.up * (direction.y * characterStats.movementSpeed * speedMultiplier * Time.deltaTime)
                + _transform.right * (direction.x * characterStats.movementSpeed * speedMultiplier * Time.deltaTime);
        }

        public void Rotate(float degrees, float speedMultiplier = 1)
        {
            _transform.Rotate(Vector3.forward * (degrees * characterStats.turnSpeed * speedMultiplier * Time.deltaTime));
        }

        public void TransitionToState(BrainState nextState)
        {
            if (nextState == state) return;

            state = nextState;
            state.Initialise(this);
        }
    }
}