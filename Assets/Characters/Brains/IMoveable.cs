using UnityEngine;

namespace Characters.Brains
{
    public interface IMoveable
    {
        void Move(Vector2 direction, float speed);
    }
}