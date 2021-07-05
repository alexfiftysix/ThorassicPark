using Phase_1.Builder.Buildings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Phase_1.Builder.DeckBuilder
{
    public class AttractionCard : MonoBehaviour
    {
        public Attraction attraction;
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI costText;
        public Image image;
        public Image check;
        public Image cross;
        public bool isStatic; // TODO: Allow clicking bigCard to deselect small card also
        public bool isEmpty = true;
        
        private bool _isUnlocked;
        private readonly Color _defaultImageColour = new Color(0, 0, 0, 0);
        private Deck _deck;
        private Hand _hand;

        // Start is called before the first frame update
        void Start()
        {
            SetAttraction(attraction);
            SetSelected(false);
            _deck = FindObjectOfType<Deck>();
            _hand = FindObjectOfType<Hand>(); 

            if (!isStatic)
            {
                _isUnlocked = attraction is null || _deck.IsUnlocked(attraction);
                cross.gameObject.SetActive(!_isUnlocked);
            }
        }

        
        public void SetAttraction(Attraction newAttraction)
        {
            isEmpty = newAttraction is null; 
            var monsterSpriteRenderer = newAttraction is null ? null : newAttraction.monster.GetComponent<SpriteRenderer>();
            image.color = monsterSpriteRenderer is null ? _defaultImageColour : monsterSpriteRenderer.color;
            image.sprite = monsterSpriteRenderer is null ? null : monsterSpriteRenderer.sprite;
            image.preserveAspect = true;

            nameText.text = newAttraction is null ? "____" : newAttraction.name;
            costText.text = newAttraction is null ? "$$$" : $"${newAttraction.cost}";

            this.attraction = newAttraction;
            if (newAttraction is null) SetSelected(false);
        }

        public void OnClick()
        {
            if (isStatic || !_isUnlocked) return;

            var isChosen = _hand.AlreadyInHand(attraction);
            if (isChosen)
            {
                _hand.Remove(attraction);
                SetSelected(false);
            }
            else if (_hand.Add(attraction))
            {
                SetSelected(true);
            }
        }

        private void SetSelected(bool set)
        {
            if (isStatic)
            {
                check.gameObject.SetActive(false);
            } else
            {
                check.gameObject.SetActive(set);
            }
        }
    }
}
