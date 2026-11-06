using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField, Min(1)] private int slotCount = 24;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private EquipmentController equipment;
    [SerializeField] private Transform dropPoint;

    private ItemUseContext useContext;

    public Inventory Inventory { get; private set; }

    private void Awake()
    {
        Inventory = new Inventory(slotCount);
        useContext = new ItemUseContext(playerHealth, equipment);
    }

    public void UseItem(int slotIndex)
    {
        Inventory.UseItem(slotIndex, useContext);
    }

    public void DropItem(int slotIndex)
    {
        InventorySlot slot = Inventory.GetSlot(slotIndex);

        if (slot.IsEmpty || slot.Item.Data.WorldPrefab == null)
            return;

        ItemInstance item = Inventory.TakeFromSlot(slotIndex);

        if (equipment.EquippedItem == item)
            equipment.Unequip();

        WorldItem worldItem = Instantiate(
            item.Data.WorldPrefab,
            dropPoint.position,
            Quaternion.identity);

        worldItem.Initialize(item.Data, item.Quantity, item.LoadedAmmo);
    }
}
