using UnityEngine;

namespace Monsters.Brains
{
    public abstract class ControllableBase : MonoBehaviour, IControllable
    {
        private Transform _transform;

        public Vector3 EulerAngles => _transform.eulerAngles;
        public Vector3 Position => _transform.position;

        public virtual void Start()
        {
            _transform = transform;
        }

        public virtual void Move(Vector2 direction, float speed)
        {
            if (_transform == null) _transform = transform;
            _transform.position += _transform.up * (direction.y * speed * Time.deltaTime)
                                   + _transform.right * (direction.x * speed * Time.deltaTime);
        }

        public virtual void Rotate(float degrees, float speed)
        {
            _transform.Rotate(Vector3.forward * (degrees * speed * Time.deltaTime));
        }
    }
}