using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetailsPanel : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private float spacing = 12f;
    

    private RectTransform RectTransform => (RectTransform)transform;

    private void Awake()
    {
        Clear();
    }

    public void Show(ItemData item, RectTransform slot)
    {
        iconImage.sprite = item.Icon;
        nameText.text = item.DisplayName;
        descriptionText.text = item.Description;

        gameObject.SetActive(true);
        UIPositioning.PlaceLeftOf(RectTransform, slot, spacing);
    }

    public void Clear()
    {
        gameObject.SetActive(false);
    }
}