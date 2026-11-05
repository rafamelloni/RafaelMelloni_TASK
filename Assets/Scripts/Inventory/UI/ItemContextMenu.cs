using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemContextMenu : MonoBehaviour
{
    [SerializeField] private InventoryController inventoryController;
    [SerializeField] private Button useButton;
    [SerializeField] private TMP_Text useButtonText;
    [SerializeField] private Button dropButton;

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

    public void Show(int index, ItemData item, Vector2 position)
    {
        slotIndex = index;

        string actionName = item.UseActionName;
        bool canUse = actionName != null;

        useButton.gameObject.SetActive(canUse);

        if (canUse)
            useButtonText.text = actionName;

        gameObject.SetActive(true);
        transform.position = position;
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