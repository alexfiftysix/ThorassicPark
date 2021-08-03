using UnityEngine;

namespace Visitors
{
    public interface IChaseable
    {
        bool TakeDamage(int damage);
        bool IsDead();
        Vector3 GetPosition();
        bool IsDestroyed { get; }
    }
}