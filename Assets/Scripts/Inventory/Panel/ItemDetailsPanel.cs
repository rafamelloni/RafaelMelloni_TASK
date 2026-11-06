using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetailsPanel : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private float spacing = 12f;
    [SerializeField] private float yHeight;

    private readonly Vector3[] corners = new Vector3[4];

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

    private void PositionNextTo(RectTransform slot)
    {
        slot.GetWorldCorners(corners);

        Vector3 position = corners[1];
        position.x -= spacing * RectTransform.lossyScale.x;
        position.y -= yHeight * RectTransform.lossyScale.y;
        RectTransform.position = position;

        ClampToScreen();
    }

    private void ClampToScreen()
    {
        RectTransform.GetWorldCorners(corners);

        Vector3 offset = Vector3.zero;

        if (corners[0].x < 0f)
            offset.x = -corners[0].x;

        if (corners[0].y < 0f)
            offset.y = -corners[0].y;
        else if (corners[1].y > Screen.height)
            offset.y = Screen.height - corners[1].y;

        RectTransform.position += offset;
    }
}