using System;
using Configuration;
using GameManagement;
using Monsters.Brains;
using Phase_2.Player;
using UnityEngine;
using Visitors;

namespace Monsters.Zombie
{
    public class Zombie : MonoBehaviour, IMoveable
    {
        public MonsterStats stats;
        public GameObject zombieBase;

        private Transform _transform;
        private bool _parkIsBroken;

        // Biting
        private AudioSource _biteAudioSource;
        private float _biteDelay = 1.5f;
        private float _biteTimePassed = 0;

        // Start is called before the first frame update
        private void Start()
        {
            _transform = zombieBase.transform;
            _biteAudioSource = GetComponent<AudioSource>();
            _parkIsBroken = false;
            
            var gameManager = FindObjectOfType<GameManager>();
            gameManager.OnParkBreaks += OnParkBreaks;
        }

        private void Update()
        {
            if (!_parkIsBroken) return;

            _biteTimePassed += Time.deltaTime;
        }
        
        private void OnParkBreaks(object sender, EventArgs args)
        {
            _parkIsBroken = true;
        }

        public void Move(Vector2 direction, float speed)
        {
            var oldPosition = (Vector2) _transform.position;
            var movement = direction * (stats.movementSpeed * Time.deltaTime);
            var newPosition = new Vector3(oldPosition.x + movement.x, oldPosition.y + movement.y, -1f);
            _transform.position = newPosition;
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer(Configuration.Configuration.Layers[Layer.Visitor])) 
            {
                Zombify(other.gameObject);
            }
            else if (other.gameObject.layer == LayerMask.NameToLayer(Configuration.Configuration.Layers[Layer.Player]))
            {
                Bite(other.gameObject.GetComponentInParent<PlayerController>());
            }
        }

        private void Zombify(GameObject visitor)
        {
            if (_biteTimePassed < _biteDelay) return;

            _biteTimePassed = 0;
            var position = visitor.transform.position;
            Destroy(visitor);
            Instantiate(zombieBase, position, Quaternion.identity);
        }

        private void Bite(IChaseable player)
        {
            if (_biteTimePassed < _biteDelay) return;

            _biteTimePassed = 0;
            stats.meleeSound.Play(_biteAudioSource);
            player.TakeDamage(stats.meleeDamage);
        }
    }
}