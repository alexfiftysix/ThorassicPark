using UnityEngine;

namespace Characters.Visitors
{
    public interface IChaseable
    {
        void TakeDamage(int damage);
        Vector3 Position { get; }
    }
}