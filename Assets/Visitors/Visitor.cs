using System;
using System.Linq;
using GameManagement;
using Phase_1.Builder.Buildings;
using Phase_2.Player;
using UnityEngine;
using Utilities;
using Utilities.Extensions;

namespace Visitors
{
    public class Visitor : MonoBehaviour, IChaseable
    {
        [SerializeField] private float speed = 1f;
        private Vector2 _direction = Vector2.down;
        private Attraction _target;
        private Transform _transform;

        private int _health = 10;
        private VisitorState _state;
        private GameManager _gameManager;
        private ChaseableManager _chaseableManager;

        // Wandering
        private const int WanderingTime = 3;
        private Timer _wanderingTimer;
        private const int WanderingTurnTime = 3;
        private Timer _wanderingTurnTimer;

        // Enjoying
        private const float EnjoyingTime = 5;
        private Timer _enjoyingTimer;

        // Running Around
        private const float RunningSpeedMultiplier = 1.4f;
        private const float RunningDirectionChangeDelay = 1f;
        private Timer _runningTurnTimer;

        // Following Player
        private PlayerController _player;
        
        // Escaping
        private bool _escaped;
        private GameObject _escapePoint;

        public bool IsDestroyed { get; private set; }

        public void Start()
        {
            _chaseableManager = FindObjectOfType<ChaseableManager>();
            _chaseableManager.Add(this);
            _transform = GetComponent<Transform>();
            _wanderingTimer = gameObject.AddTimer(WanderingTime, ChooseTarget);
            _enjoyingTimer = gameObject.AddTimer(EnjoyingTime, StartWandering);
            _wanderingTurnTimer = gameObject.AddTimer(WanderingTurnTime, ChooseDirection);
            SetState(VisitorState.Wandering);
            _gameManager = FindObjectOfType<GameManager>();
            _gameManager.OnParkBreaks += OnParkBreaks;

            IsDestroyed = false;
        }

        private void OnParkBreaks(object sender, EventArgs args)
        {
            SetState(VisitorState.FreakingOut);
            Destroy(_wanderingTimer);
            Destroy(_wanderingTurnTimer);
            Destroy(_enjoyingTimer);
            _runningTurnTimer = gameObject.AddTimer(RunningDirectionChangeDelay, ChooseDirection);
            _direction = Directions.directions.RandomChoice();
        }

        // Update is called once per frame
        private void Update()
        {
            if (_state == VisitorState.FollowingPlayer)
            {
                TurnToTarget(_player.transform);
            }
            else if (_state == VisitorState.WalkingToAttraction)
            {
                TurnToTarget(_target.transform);
            }
            else if (_state == VisitorState.HeadingToEscapePoint)
            {
                TurnToTarget(_escapePoint.transform);
            }

            Move();
        }

        public void OnTriggerStay2D(Collider2D other)
        {
            // Using Stay for this because the Visitor may pick an attraction they're already in the viewRadius of 
            if (_state == VisitorState.WalkingToAttraction
                && other.gameObject.GetComponent<ViewRadius>() == _target.viewRadius)
            {
                StartEnjoying();
            }
            else if (_state == VisitorState.FreakingOut && other.gameObject.name == "VisitorCatchRadius")
            {
                // TODO: String check bad
                SetState(VisitorState.FollowingPlayer);
                Destroy(_runningTurnTimer);
                _player = FindObjectOfType<PlayerController>();
            }
            else if (_state == VisitorState.FollowingPlayer && other.gameObject.name == "VisitorEscapePointRadius")
            {
                // TODO: String check bad
                SetState(VisitorState.HeadingToEscapePoint);
                _escapePoint = other.gameObject.FindParent("VisitorEscapePoint");
            }
        }
        

        public void OnCollisionEnter2D(Collision2D other)
        {
            if (_state == VisitorState.HeadingToEscapePoint && !_escaped && other.gameObject.name == "VisitorEscapePointBuilding")
            {
                // TODO: string check bad
                Escape();
            }
        }

        public void OnDestroy()
        {
            IsDestroyed = true;
        }

        private void Escape()
        {
            _escaped = true;
            _gameManager.VisitorEscaped();
            Destroy(gameObject);
        }

        // Return true if the Visitor has died
        public bool TakeDamage(int damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                Die();
                return true;
            }

            return false;
        }

        public bool IsDead()
        {
            return _health < 0;
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        private void ChooseTarget()
        {
            _target = _gameManager.attractions.Where(a => !a.isGhost).RandomChoice();
            if (_target is null) return;
            SetState(VisitorState.WalkingToAttraction);
        }

        private void Move()
        {
            var oldPosition = (Vector2) transform.position;
            var movement = _direction * (speed * Time.deltaTime *
                                         (_state == VisitorState.FreakingOut ? RunningSpeedMultiplier : 1));
            var newPosition = new Vector3(oldPosition.x + movement.x, oldPosition.y + movement.y, 0);
            _transform.position = newPosition;
        }

        private void ChooseDirection()
        {
            if (_state == VisitorState.Wandering || _state == VisitorState.FreakingOut)
            {
                _direction = Directions.directions.RandomChoice();
            }
        }

        private void TurnToTarget(Transform target)
        {
            _direction = target.position - transform.position;
        }

        private void Die()
        {
            Destroy(gameObject);
        }

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
            if (!(_wanderingTimer is null)) _wanderingTimer.isActive = state == VisitorState.Wandering;
            if (!(_enjoyingTimer is null)) _enjoyingTimer.isActive = state == VisitorState.EnjoyingAttraction;
            _state = state;
        }
    }
}