using Phase_1.Builder.Buildings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AttractionCard : MonoBehaviour
{
    public Attraction attraction;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI costText;
    public Image image;
    public Image check;

    private bool _isSelected = false;

    
    // Start is called before the first frame update
    void Start()
    {
        var monsterSpriteRenderer = attraction.monster.GetComponent<SpriteRenderer>();
        image.color = monsterSpriteRenderer.color;
        image.sprite = monsterSpriteRenderer.sprite;
        image.preserveAspect = true;

        nameText.text = attraction.name;
        costText.text = $"${attraction.cost}";
        
        Select(false);
    }

    public void OnClick()
    {
        Select(!_isSelected);
    }

    private void Select(bool set)
    {
        _isSelected = set;
        check.gameObject.SetActive(_isSelected);
    }
}
