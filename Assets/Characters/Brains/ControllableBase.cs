using Buildings;
using Characters.Brains.BrainStates;
using Characters.Monsters;
using Characters.Visitors;
using GameManagement;
using UnityEngine;

namespace Characters.Brains
{
    public abstract class ControllableBase : MonoBehaviour
    {
        public CharacterStats characterStats;
        public Brain brain;
        private BrainState _state;

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
        public Player.Player Player { set; get; }
        
        public GameManager GameManager { get; private set; }
        public ChaseableManager ChaseableManager { get; set; }

        public virtual void Start()
        {
            _transform = transform;
            _state = brain.rootNode.startState;
            _state.Initialise(this);
            GameManager = FindObjectOfType<GameManager>();
            ChaseableManager = FindObjectOfType<ChaseableManager>();
        }

        public virtual void Update()
        {
            _state.DoActions(this);
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
            if (nextState == _state) return;

            _state = nextState;
            _state.Initialise(this);
        }
    }
}