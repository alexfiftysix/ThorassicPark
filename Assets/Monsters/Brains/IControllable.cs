using UnityEngine;

namespace Monsters.Brains
{
    public interface IControllable : IMoveable, IRotateable
    {
        public Vector3 EulerAngles { get; }
        public Vector3 Position { get; }
        public Vector2 Direction { get; set; }
        public MonsterStats Stats { get; }
    }
}