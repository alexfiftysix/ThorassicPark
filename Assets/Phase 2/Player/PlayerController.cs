using GameManagement;
using Phase_2.Helipad;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Utilities.Extensions;
using Visitors;

namespace Phase_2.Player
{
    public class PlayerController : MonoBehaviour, IChaseable
    {
        [SerializeField] private float speed = 1f;
        [HideInInspector] public GameManager manager;

        [HideInInspector] public Slider healthBar;
        private int _health = 10;

        public GameObject helipadPointer;
        [HideInInspector] public EscapePoint helipad;
        private float _helipadPointedSpeed = 15;
        private bool _helipadSpawned = false;
        
        [SerializeField] private Vector2 direction = Vector2.zero;

        private Transform _transform;

        private void Start()
        {
            var chaseableManager = FindObjectOfType<ChaseableManager>();
            chaseableManager.Add(this);

            _transform = transform;

            healthBar = Instantiate(healthBar, new Vector3(0, -20, 0), Quaternion.identity);
            healthBar.maxValue = _health;
            healthBar.value = _health;

            healthBar.transform.SetParent(GameObject.Find("Canvas").transform, false);
        }

        void Update()
        {
            Move();

            if (!(helipad is null))
            {
                if (!_helipadSpawned)
                {
                    helipadPointer.GetComponent<PointerPivot>().pointer.enabled = true;
                    _helipadSpawned = true;
                }
                MoveHelipadPointer();
            }
        }
    
        private void Move()
        {
            var oldPosition = (Vector2) _transform.position;
            var movement = direction * (speed * Time.deltaTime);
            var newPosition = new Vector2(oldPosition.x + movement.x, oldPosition.y + movement.y);
            _transform.position = newPosition.ToVector3();
        }

        private void OnMove(InputValue inputValue)
        {
            direction = inputValue.Get<Vector2>();
        }

        private void MoveHelipadPointer()
        {
            var newDirection = helipad.transform.position - _transform.position;
            var angle = Mathf.Atan2(newDirection.y, newDirection.x) * Mathf.Rad2Deg - 90;
            var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            helipadPointer.transform.rotation = Quaternion.Slerp(helipadPointer.transform.rotation, rotation, _helipadPointedSpeed * Time.deltaTime);
        }

        public bool TakeDamage(int damage)
        {
            _health -= damage;
            healthBar.value = _health;
            if (_health <= 0)
            {
                Die();
                return true;
            }

            return false;
        }

        public bool IsDead()
        {
            return _health <= 0;
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        private void Die()
        {
            manager.GameOver();
            Destroy(gameObject);
        }
    }
}
