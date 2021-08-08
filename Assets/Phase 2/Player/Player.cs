using GameManagement;
using Monsters.Brains;
using Phase_2.Helipad;
using UnityEngine;
using UnityEngine.UI;
using Visitors;

namespace Phase_2.Player
{
    public class Player : ControllableBase, IChaseable
    {
        [HideInInspector] public GameManager manager;

        [HideInInspector] public Slider healthBar;
        [HideInInspector] public float viewRadiusSize = 1;
        private int _health = 10;

        public GameObject helipadPointer;
        [HideInInspector] public EscapePoint helipad;
        private const float HelipadPointedSpeed = 15;
        private bool _helipadSpawned;

        public bool IsDestroyed { get; private set; }

        private ChaseableManager _chaseableManager;

        public override void Start()
        {
            _chaseableManager = FindObjectOfType<ChaseableManager>();
            _chaseableManager.Add(this);
            _helipadSpawned = false;

            healthBar = Instantiate(healthBar, new Vector3(0, -20, 0), Quaternion.identity);
            healthBar.maxValue = _health;
            healthBar.value = _health;

            healthBar.transform.SetParent(GameObject.Find("Canvas").transform, false);
            
            base.Start();
        }

        public override void Update()
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
            
            base.Update();
        }

        public void OnDestroy()
        {
            IsDestroyed = true;
        }

        private void MoveHelipadPointer()
        {
            // TODO: This shouldn't be a part of the Player class
            var newDirection = helipad.transform.position - transform.position;
            var angle = Mathf.Atan2(newDirection.y, newDirection.x) * Mathf.Rad2Deg - 90;
            var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            helipadPointer.transform.rotation = Quaternion.Slerp(
                helipadPointer.transform.rotation,
                rotation,
                HelipadPointedSpeed * Time.deltaTime
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
            _chaseableManager.Remove(this);
            manager.GameOver();
            Destroy(gameObject);
        }
    }
}