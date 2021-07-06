using System.Collections.Generic;
using System.Linq;
using GameManagement;
using Phase_1.Builder.Buildings;
using UnityEngine;
using Utilities;
using Utilities.Extensions;

namespace Visitors
{
    public class Visitor: Chaseable
    {
        [SerializeField] private float speed = 1f;
        private Vector2 _direction = Vector2.down;
        private Attraction _target;
        private Transform _transform;

        private int _health = 10;
        private VisitorState _state;
        
        // Wandering
        private const int WanderingTime = 3;
        private Timer _wanderingTimer;

        // Enjoying
        private const float EnjoyingTime = 5;
        private Timer _enjoyingTimer;

        private bool _parkIsBroken = false;

        private void Start()
        {
            _transform = gameObject.transform;
            _wanderingTimer = gameObject.AddTimer(WanderingTime, ChooseTarget);
            _enjoyingTimer = gameObject.AddTimer(EnjoyingTime, StartWandering);
            SetState(VisitorState.Wandering);

            FindObjectOfType<GameManager>().OnPhaseChanged += OnPhaseChanged;
        }

        private void OnPhaseChanged(object sender, Phase newPhase)
        {
            if (newPhase == Phase.RunningFromDinosaurs)
            {
                Debug.Log("Phase changed!!");
                _parkIsBroken = true;
            }
        }

        // Update is called once per frame
        private void Update()
        {
            if (_parkIsBroken)
            {
                // TODO: Go crazy, unless you've latched on to the player
                //       In which case, follow them
            }
            
            if (_state == VisitorState.Wandering)
            {
                var wantsToTurn = MyRandom.CoinFlip(.005f);
                if (wantsToTurn)
                {
                    ChooseDirection();
                }
            }
            else if (_state == VisitorState.WalkingToAttraction)
            {
                TurnToTarget();
            }
            Move();
        }

        
        
        public void OnTriggerStay2D(Collider2D other)
        {
            if (_state == VisitorState.WalkingToAttraction && other.gameObject.GetComponent<ViewRadius>() == _target.viewRadius)
            {
                StartEnjoying();
            }
        }

        // Return true if the Visitor has died
        public override bool TakeDamage(int damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                Die();
                return true;
            }

            return false;
        }

        public override bool IsDead()
        {
            return _health < 0;
        }

        private void ChooseTarget()
        {
            _target = FindObjectsOfType<Attraction>().Where(a => !a.isGhost).ToList().RandomChoice();
            if (_target is null) return;
            SetState(VisitorState.WalkingToAttraction);
        }

        private void Move()
        {
            var oldPosition = (Vector2) transform.position;
            var movement = _direction * (speed * Time.deltaTime);
            var newPosition = new Vector3(oldPosition.x + movement.x, oldPosition.y + movement.y, -1f);
            _transform.position = newPosition;
        }

        private void ChooseDirection()
        {
            _direction = _directions.RandomChoice();
        }

        private void TurnToTarget()
        {
            _direction = _target.transform.position - transform.position;
        }

        private void Die()
        {
            Destroy(gameObject);
        }

        private readonly List<Vector2> _directions = new List<Vector2>()
        {
            Vector2.up,
            Vector2.down,
            Vector2.left,
            Vector2.right,
            new Vector2(1, 1),
            new Vector2(1, -1),
            new Vector2(-1, 1),
            new Vector2(-1, -1),
            Vector2.zero
        };

        private void StartWandering()
        {
            SetState(VisitorState.Wandering);
            _target = null;
        }

        private void StartEnjoying()
        {
            _direction *= 0.5f;
            _enjoyingTimer.Reset();
            SetState(VisitorState.EnjoyingAttraction);
        }

        private void SetState(VisitorState state)
        {
            _wanderingTimer.isActive = state == VisitorState.Wandering;
            _enjoyingTimer.isActive = state == VisitorState.EnjoyingAttraction;
            _state = state;
        }
    }
}