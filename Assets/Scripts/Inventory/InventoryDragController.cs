using UnityEngine;
using UnityEngine.UI;

public class InventoryDragController : MonoBehaviour
{
    private const int NoSlot = -1;

    [SerializeField] private InventoryController inventoryController;
    [SerializeField] private Image dragIcon;

    private int draggedSlotIndex = NoSlot;

    private void Awake()
    {
        dragIcon.enabled = false;
    }

    public void BeginDrag(int slotIndex, Sprite icon, Vector2 position)
    {
        draggedSlotIndex = slotIndex;
        dragIcon.sprite = icon;
        dragIcon.enabled = true;
        dragIcon.transform.position = position;
    }

    public void UpdateDrag(Vector2 position)
    {
        if (draggedSlotIndex != NoSlot)
            dragIcon.transform.position = position;
    }

    public void DropOn(int targetSlotIndex)
    {
        if (draggedSlotIndex == NoSlot)
            return;

        inventoryController.Inventory.MoveItem(draggedSlotIndex, targetSlotIndex);
    }

    public void EndDrag()
    {
        draggedSlotIndex = NoSlot;
        dragIcon.enabled = false;
    }
}