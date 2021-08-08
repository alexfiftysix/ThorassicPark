using Buildings.Helipad;
using Characters.Brains;
using Characters.Visitors;
using UnityEngine;
using UnityEngine.UI;

namespace Characters.Player
{
    public class Player : ControllableBase, IChaseable
    {
        [HideInInspector] public Slider healthBar;
        [HideInInspector] public float viewRadiusSize = 1;
        private int _health = 10;

        public GameObject helipadPointer;
        [HideInInspector] public EscapePoint helipad;
        private const float HelipadPointedSpeed = 15;
        private bool _helipadSpawned;

        public override void Start()
        {
            base.Start();
            
            ChaseableManager.Add(this);
            _helipadSpawned = false;

            healthBar = Instantiate(healthBar, new Vector3(0, -20, 0), Quaternion.identity);
            healthBar.maxValue = _health;
            healthBar.value = _health;

            healthBar.transform.SetParent(GameObject.Find("Canvas").transform, false);
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
            ChaseableManager.Remove(this);
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

        public void TakeDamage(int damage)
        {
            _health -= damage;
            healthBar.value = _health;
            if (_health <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            ChaseableManager.Remove(this);
            GameManager.GameOver();
            Destroy(gameObject);
        }
    }
}