using UnityEngine;

namespace Phase_1.Builder.DeckBuilder
{
    public class AttachToHandOnAwake : MonoBehaviour
    {
        public int handIndex;
        
        // Start is called before the first frame update
        void Awake()
        {
            var hand = FindObjectOfType<Hand>();
            var card = GetComponent<AttractionCard>();

            hand.SetCard(card, handIndex);
        }
    }
}
