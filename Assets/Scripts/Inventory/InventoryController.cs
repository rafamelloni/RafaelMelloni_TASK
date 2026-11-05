using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField, Min(1)] private int slotCount = 24;

    public Inventory Inventory { get; private set; }

    private void Awake()
    {
        Inventory = new Inventory(slotCount);
    }
}
