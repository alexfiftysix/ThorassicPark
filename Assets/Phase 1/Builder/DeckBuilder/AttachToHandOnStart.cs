using UnityEngine;

namespace Phase_1.Builder.DeckBuilder
{
    public class AttachToHandOnStart : MonoBehaviour
    {
        public int handIndex;
        
        // Start is called before the first frame update
        private void Start()
        {
            var hand = FindObjectOfType<Hand>();
            var card = GetComponent<AttractionCard>();

            hand.SetCard(card, handIndex);
        }
    }
}
