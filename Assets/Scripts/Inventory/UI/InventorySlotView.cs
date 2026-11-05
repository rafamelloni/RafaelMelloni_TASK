using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text quantityText;

    private InventoryController inventoryController;
    private InventorySlot currentSlot;
    private InventoryDragController dragController;
    private ItemDetailsPanel detailsPanel;
    private int slotIndex;

    public void Initialize(int index, InventoryDragController controller, ItemDetailsPanel details, InventoryController inventory)
    {
        slotIndex = index;
        dragController = controller;
        detailsPanel = details;
        inventoryController = inventory;
    }

    public void Render(InventorySlot slot)
    {
        currentSlot = slot;

        if (slot.IsEmpty)
        {
            iconImage.enabled = false;
            quantityText.text = string.Empty;
            return;
        }

        iconImage.enabled = true;
        iconImage.sprite = slot.Item.Data.Icon;
        quantityText.text = slot.Item.Quantity > 1
            ? slot.Item.Quantity.ToString()
            : string.Empty;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (currentSlot == null || currentSlot.IsEmpty)
            return;

        SetIconAlpha(0.35f);
        dragController.BeginDrag(slotIndex, currentSlot.Item.Data.Icon, eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        dragController.UpdateDrag(eventData.position);
    }

    public void OnDrop(PointerEventData eventData)
    {
        dragController.DropOn(slotIndex);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        SetIconAlpha(1f);
        dragController.EndDrag();
    }

    private void SetIconAlpha(float alpha)
    {
        Color color = iconImage.color;
        color.a = alpha;
        iconImage.color = color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (dragController.IsDragging)
            return;

        if (currentSlot == null || currentSlot.IsEmpty)
        {
            detailsPanel.Clear();
            return;
        }

        detailsPanel.Show(currentSlot.Item.Data, (RectTransform)transform);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        detailsPanel.Clear();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Right)
            return;

        inventoryController.UseItem(slotIndex);

        if (currentSlot.IsEmpty)
            detailsPanel.Clear();
    }
}