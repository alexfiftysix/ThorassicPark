using Monsters.Brains;
using UnityEngine;

namespace Visitors
{
    public class Visitor : ControllableBase, IChaseable
    {
        private int _health = 10;

        public void OnCollisionEnter2D(Collision2D other)
        {
            if (GameManager.parkIsBroken && other.gameObject.name == "VisitorEscapePointBuilding")
            {
                Escape();
            }
        }

        private void Escape()
        {
            GameManager.VisitorEscaped();
            Destroy(gameObject);
        }

        public void TakeDamage(int damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            ChaseableManager.Remove(this);
        }
    }
}