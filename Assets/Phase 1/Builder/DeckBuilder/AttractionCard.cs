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
        public Deck deck;
        public bool isBigCard; // TODO: Allow clicking bigCard to deselect small card also
        public bool isEmpty = true;

        private readonly Color _defaultImageColour = new Color(0, 0, 0, 0);

        // Start is called before the first frame update
        void Start()
        {
            SetAttraction(attraction);
        
            SetSelected(false);
        }

        public void SetAttraction(Attraction at)
        {
            isEmpty = at is null; 
            var monsterSpriteRenderer = at is null ? null : at.monster.GetComponent<SpriteRenderer>();
            image.color = monsterSpriteRenderer is null ? _defaultImageColour : monsterSpriteRenderer.color;
            image.sprite = monsterSpriteRenderer is null ? null : monsterSpriteRenderer.sprite;
            image.preserveAspect = true;

            nameText.text = at is null ? "____" : at.name;
            costText.text = at is null ? "$$$" : $"${at.cost}";

            attraction = at;
            if (at is null) SetSelected(false);
        }

        public void OnClick()
        {
            if (isBigCard) return;

            var isChosen = deck.AttractionIsChosen(attraction);
            if (isChosen)
            {
                deck.DeSelectAttraction(attraction);
                SetSelected(false);
            }
            else if (deck.ChooseAttraction(attraction))
            {
                SetSelected(true);
            }
        }

        private void SetSelected(bool set)
        {
            if (isBigCard)
            {
                check.gameObject.SetActive(false);
            } else
            {
                check.gameObject.SetActive(set);
            }
        }
    }
}
