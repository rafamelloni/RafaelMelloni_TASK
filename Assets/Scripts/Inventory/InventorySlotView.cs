using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text quantityText;

    private InventorySlot currentSlot;
    private InventoryDragController dragController;
    private int slotIndex;

    public void Initialize(int index, InventoryDragController controller)
    {
        slotIndex = index;
        dragController = controller;
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
}