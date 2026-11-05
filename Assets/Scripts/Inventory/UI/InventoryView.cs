using UnityEngine;

public class InventoryView : MonoBehaviour
{
    [SerializeField] private InventoryController inventoryController;
    [SerializeField] private InventorySlotView slotPrefab;
    [SerializeField] private Transform slotsParent;
    [SerializeField] private InventoryDragController dragController;
    [SerializeField] private ItemDetailsPanel detailsPanel;
    [SerializeField] private ItemContextMenu contextMenu;

    private Inventory inventory;
    private InventorySlotView[] slotViews;

    private void Start()
    {
        inventory = inventoryController.Inventory;
        CreateSlotViews();

        inventory.OnChanged += Refresh;
        Refresh();
    }

    private void OnDestroy()
    {
        if (inventory != null)
            inventory.OnChanged -= Refresh;
    }

    private void CreateSlotViews()
    {
        slotViews = new InventorySlotView[inventory.SlotCount];

        for (int i = 0; i < slotViews.Length; i++)
        {
            slotViews[i] = Instantiate(slotPrefab, slotsParent);
            slotViews[i].Initialize(i, dragController, detailsPanel, contextMenu);
        }
    }

    private void Refresh()
    {
        for (int i = 0; i < slotViews.Length; i++)
            slotViews[i].Render(inventory.GetSlot(i));
    }
}