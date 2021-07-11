using Phase_1.Builder.Buildings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Phase_1.Builder.DeckBuilder
{
    public class AttractionCard : MonoBehaviour
    {
        public Unlockable unlockable;
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI costText;
        public Image image;
        public Image check;
        public Image cross;
        public bool isStatic; // TODO: Allow clicking bigCard to deselect small card also
        public bool isEmpty = true;
        
        private readonly Color _defaultImageColour = new Color(0, 0, 0, 0);
        private Hand _hand;

        private void Start()
        {
            SetAttraction(unlockable);
            SetSelected(false);
            _hand = FindObjectOfType<Hand>(); 

            if (!isStatic)
            {
                cross.gameObject.SetActive(!unlockable.isUnlocked);

                if (_hand.Contains(unlockable.attraction))
                {
                    SetSelected(true);
                }
            }
        }

        public void SetAttraction(Unlockable newAttraction)
        {
            isEmpty = newAttraction is null; 
            var monsterSpriteRenderer = newAttraction is null ? null : newAttraction.attraction.monster.GetComponentInChildren<SpriteRenderer>();
            image.color = monsterSpriteRenderer is null ? _defaultImageColour : monsterSpriteRenderer.color;
            image.sprite = monsterSpriteRenderer is null ? null : monsterSpriteRenderer.sprite;
            image.preserveAspect = true;

            nameText.text = newAttraction is null ? "____" : newAttraction.name;
            costText.text = newAttraction is null ? "$$$" : $"${newAttraction.attraction.cost}";

            unlockable = newAttraction;
            if (newAttraction is null) SetSelected(false);
        }

        public void OnClick()
        {
            if (isStatic || !unlockable.isUnlocked) return;

            var added = _hand.Toggle(unlockable);
            SetSelected(added);
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
