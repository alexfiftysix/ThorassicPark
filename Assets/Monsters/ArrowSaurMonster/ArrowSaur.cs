using Configuration;
using Monsters.Brains;
using UnityEngine;
using Visitors;

namespace Monsters.ArrowSaurMonster
{
    public class ArrowSaur : ControllableBase
    {
        private AudioSource _biteAudioSource;

        public Brain brain;
        public MonsterStats monsterStats;

        public override void Start()
        {
            _biteAudioSource = GetComponent<AudioSource>();
            brain.Initialise();
            
            base.Start();
        }

        private void Update()
        {
            brain.Act(this);
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