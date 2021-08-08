using Characters.Brains;
using Characters.Visitors;
using Configuration;
using UnityEngine;

namespace Characters.Monsters.ArrowSaurMonster
{
    public class ArrowSaur : ControllableBase
    {
        private AudioSource _biteAudioSource;

        public override void Start()
        {
            _biteAudioSource = GetComponent<AudioSource>();
            base.Start();
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
            biteTarget.TakeDamage(characterStats.meleeDamage);
            characterStats.meleeSound.Play(_biteAudioSource);
        }
    }
}