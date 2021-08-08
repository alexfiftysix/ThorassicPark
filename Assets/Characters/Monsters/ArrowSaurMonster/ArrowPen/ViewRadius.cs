using Characters.Visitors;
using UnityEngine;

namespace Characters.Monsters.ArrowSaurMonster.ArrowPen
{
    public class ViewRadius : MonoBehaviour
    {
        public int VisitorCount { get; set; } = 0;
    
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<Visitor>())
            {
                VisitorCount++;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<Visitor>())
            {
                VisitorCount--;
            }
        }
    }
}
