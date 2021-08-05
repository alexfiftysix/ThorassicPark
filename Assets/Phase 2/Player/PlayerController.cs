using GameManagement;
using Monsters.Brains;
using Phase_2.Helipad;
using UnityEngine;
using UnityEngine.UI;
using Visitors;

namespace Phase_2.Player
{
    public class PlayerController : ControllableBase, IChaseable
    {
        [HideInInspector] public GameManager manager;

        [HideInInspector] public Slider healthBar;
        private int _health = 10;

        public GameObject helipadPointer;
        [HideInInspector] public EscapePoint helipad;
        private float _helipadPointedSpeed = 15;
        private bool _helipadSpawned = false;

        public bool IsDestroyed { get; private set; }

        public override void Start()
        {
            var chaseableManager = FindObjectOfType<ChaseableManager>();
            chaseableManager.Add(this);


            healthBar = Instantiate(healthBar, new Vector3(0, -20, 0), Quaternion.identity);
            healthBar.maxValue = _health;
            healthBar.value = _health;

            healthBar.transform.SetParent(GameObject.Find("Canvas").transform, false);
        }

        void Update()
        {
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

        public void OnDestroy()
        {
            IsDestroyed = true;
        }

        private void MoveHelipadPointer()
        {
            var newDirection = helipad.transform.position - transform.position;
            var angle = Mathf.Atan2(newDirection.y, newDirection.x) * Mathf.Rad2Deg - 90;
            var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            helipadPointer.transform.rotation = Quaternion.Slerp(
                helipadPointer.transform.rotation,
                rotation,
                _helipadPointedSpeed * Time.deltaTime
            );
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

        private void Die()
        {
            manager.GameOver();
            Destroy(gameObject);
        }
    }
}