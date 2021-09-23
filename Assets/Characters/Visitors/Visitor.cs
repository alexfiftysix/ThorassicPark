using Characters.Brains;

namespace Characters.Visitors
{
    public class Visitor : ControllableBase, IChaseable
    {
        private int _health = 10;

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