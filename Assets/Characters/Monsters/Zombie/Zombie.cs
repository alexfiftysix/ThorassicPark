using Characters.Brains;
using Characters.Visitors;
using Configuration;
using UnityEngine;

namespace Characters.Monsters.Zombie
{
    public class Zombie : ControllableBase
    {
        public GameObject zombieBase;

        // Biting
        private AudioSource _biteAudioSource;
        private float _biteDelay = 1.5f;
        private float _biteTimePassed = 0;

        // Start is called before the first frame update
        public override void Start()
        {
            _biteAudioSource = GetComponent<AudioSource>();
            
            base.Start();
        }

        public override void Update()
        {
            _biteTimePassed += Time.deltaTime;
            
            base.Update();
        }
        
        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer(Configuration.Configuration.Layers[Layer.Visitor])) 
            {
                Zombify(other.gameObject);
            }
            else if (other.gameObject.layer == LayerMask.NameToLayer(Configuration.Configuration.Layers[Layer.Player]))
            {
                Bite(other.gameObject.GetComponentInParent<Player.Player>());
            }
        }

        private void Zombify(GameObject visitor)
        {
            if (_biteTimePassed < _biteDelay) return;

            _biteTimePassed = 0;
            var position = visitor.transform.position;
            characterStats.meleeSound.Play(_biteAudioSource);
            Destroy(visitor);
            Instantiate(zombieBase, position, Quaternion.identity);
        }

        private void Bite(IChaseable player)
        {
            if (_biteTimePassed < _biteDelay) return;

            _biteTimePassed = 0;
            characterStats.meleeSound.Play(_biteAudioSource);
            player.TakeDamage(characterStats.meleeDamage);
        }
    }
}