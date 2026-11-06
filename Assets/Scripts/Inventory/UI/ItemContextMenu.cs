using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemContextMenu : MonoBehaviour
{
    [SerializeField] private InventoryController inventoryController;
    [SerializeField] private Button useButton;
    [SerializeField] private TMP_Text useButtonText;
    [SerializeField] private Button dropButton;
    [SerializeField] private float spacing = 24f;

    private int slotIndex;

    private RectTransform RectTransform => (RectTransform)transform;

    private void Awake()
    {
        useButton.onClick.AddListener(UseItem);
        dropButton.onClick.AddListener(DropItem);
        Close();
    }

    private void Update()
    {
        bool clicked = Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1);

        if (clicked && !RectTransformUtility.RectangleContainsScreenPoint(RectTransform, Input.mousePosition))
            Close();
    }

    public void Show(int index, ItemData item, RectTransform slot)
    {
        slotIndex = index;

        string actionName = item.UseActionName;
        bool canUse = actionName != null;

        useButton.gameObject.SetActive(canUse);

        if (canUse)
            useButtonText.text = actionName;

        gameObject.SetActive(true);

        LayoutRebuilder.ForceRebuildLayoutImmediate(RectTransform);
        UIPositioning.PlaceLeftOf(RectTransform, slot, spacing);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    private void UseItem()
    {
        inventoryController.UseItem(slotIndex);
        Close();
    }

    private void DropItem()
    {
        inventoryController.DropItem(slotIndex);
        Close();
    }
}