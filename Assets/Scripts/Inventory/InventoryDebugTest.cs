using UnityEngine;

public class InventoryDebugTest : MonoBehaviour
{
    [SerializeField] private ItemData ammo;
    [SerializeField] private ItemData medkit;

    private void Start()
    {
        Inventory inventory = GetComponent<InventoryController>().Inventory;
        inventory.OnChanged += () => Debug.Log("Inventario actualizado");

        Debug.Log($"Sobrante: {inventory.AddItem(ammo, 45)}");
        inventory.AddItem(medkit, 2);
        inventory.MoveItem(0, 5);
        PrintSlots(inventory);
    }

    private void PrintSlots(Inventory inventory)
    {
        for (int i = 0; i < inventory.SlotCount; i++)
        {
            InventorySlot slot = inventory.GetSlot(i);

            if (!slot.IsEmpty)
                Debug.Log($"Slot {i}: {slot.Item.Data.DisplayName} x{slot.Item.Quantity}");
        }
    }
}