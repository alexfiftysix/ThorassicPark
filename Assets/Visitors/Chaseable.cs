using UnityEngine;

namespace Visitors
{
    public abstract class Chaseable : MonoBehaviour
    {
        public virtual bool TakeDamage(int damage)
        {
            return false;
        }

        public virtual bool IsDead()
        {
            return false;
        }
    }
}