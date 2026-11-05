using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField, Min(1)] private int slotCount = 24;
    [SerializeField] private PlayerHealth playerHealth;

    private ItemUseContext useContext;

    public Inventory Inventory { get; private set; }

    private void Awake()
    {
        Inventory = new Inventory(slotCount);
        useContext = new ItemUseContext(playerHealth);
    }

    public void UseItem(int slotIndex)
    {
        Inventory.UseItem(slotIndex, useContext);
    }
}
