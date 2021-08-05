using Configuration;
using GameManagement;
using Monsters.Brains;
using UnityEngine;
using Visitors;

namespace Monsters.ArrowSaurMonster
{
    public class ArrowSaur : MonoBehaviour, IControllable
    {
        private Transform _transform;
        private AudioSource _biteAudioSource;
        private IChaseable _target;
        private ChaseableManager _chaseableManager;

        public Brain brain;
        public MonsterStats monsterStats;
        public Vector3 EulerAngles => _transform.eulerAngles;
        public Vector3 Position => _transform.position;

        private void Start()
        {
            _transform = transform;
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

        public void Move(Vector2 direction, float speed)
        {
            _transform.position += _transform.up * (direction.y * speed * Time.deltaTime)
                                   + _transform.right * (direction.x * speed * Time.deltaTime);
        }

        public void Rotate(float degrees, float speed)
        {
            _transform.Rotate(Vector3.forward * (degrees * speed * Time.deltaTime));
        }
    }
}