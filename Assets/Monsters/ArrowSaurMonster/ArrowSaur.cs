using Configuration;
using Phase_2.Player;
using UnityEngine;
using Visitors;

namespace Monsters.ArrowSaurMonster
{
    public class ArrowSaur : MonoBehaviour, IMoveable, IRotateable
    {
        public MonsterStats monsterStats;

        private Transform _transform;
        private AudioSource _biteAudioSource;

        private void Start()
        {
            _transform = transform;
            _biteAudioSource = GetComponent<AudioSource>();
        }

        public void OnCollisionEnter2D(Collision2D other)
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

        public void Move(Vector2 direction)
        {
            _transform.position +=
                _transform.up * (direction.y * monsterStats.movementSpeed * Time.deltaTime)
                + _transform.right * (direction.x * monsterStats.movementSpeed * Time.deltaTime);
        }

        public void Rotate(float degrees)
        {
            _transform.Rotate(Vector3.forward * (degrees * monsterStats.turnSpeed * Time.deltaTime));
        }
    }
}