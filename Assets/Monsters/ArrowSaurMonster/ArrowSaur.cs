using Configuration;
using GameManagement;
using Monsters.Brains;
using UnityEngine;
using Visitors;

namespace Monsters.ArrowSaurMonster
{
    public class ArrowSaur : ControllableBase
    {
        private AudioSource _biteAudioSource;
        private IChaseable _target;
        private ChaseableManager _chaseableManager;

        public Brain brain;
        public MonsterStats monsterStats;

        public override void Start()
        {
            base.Start();
            
            _biteAudioSource = GetComponent<AudioSource>();
            _chaseableManager = FindObjectOfType<ChaseableManager>();
        }

        private void Update()
        {
            if (_target == null)
            {
                _target = _chaseableManager.GetRandom();
                return;
            }

            brain.Act(this, _target);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer(Configuration.Configuration.Layers[Layer.Visitor]))
            {
                Bite(other.gameObject.GetComponent<IChaseable>());
            }
            else if (other.gameObject.layer == LayerMask.NameToLayer(Configuration.Configuration.Layers[Layer.Player]))
            {
                Bite(other.gameObject.GetComponentInParent<IChaseable>());
            }
        }

        private void Bite(IChaseable biteTarget)
        {
            biteTarget.TakeDamage(monsterStats.meleeDamage);
            monsterStats.meleeSound.Play(_biteAudioSource);
        }
    }
}