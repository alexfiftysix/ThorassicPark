using UnityEngine;

namespace Monsters.Brains
{
    public interface IMoveable
    {
        void Move(Vector2 direction, float speed);
    }
}