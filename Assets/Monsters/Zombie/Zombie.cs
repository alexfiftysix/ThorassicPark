using System;
using Configuration;
using GameManagement;
using Monsters.Brains;
using Phase_2.Player;
using UnityEngine;
using Visitors;

namespace Monsters.Zombie
{
    public class Zombie : ControllableBase
    {
        public Brain brain;
        
        public MonsterStats stats;
        public GameObject zombieBase;

        private bool _parkIsBroken;

        // Biting
        private AudioSource _biteAudioSource;
        private float _biteDelay = 1.5f;
        private float _biteTimePassed = 0;

        // Start is called before the first frame update
        public override void Start()
        {
            _biteAudioSource = GetComponent<AudioSource>();
            _parkIsBroken = true;
            
            var gameManager = FindObjectOfType<GameManager>();
            gameManager.OnParkBreaks += OnParkBreaks;
            brain.Initialise();

            base.Start();
        }

        private void Update()
        {
            brain.Act(this, null);

            _biteTimePassed += Time.deltaTime;
        }
        
        private void OnParkBreaks(object sender, EventArgs args)
        {
            _parkIsBroken = true;
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