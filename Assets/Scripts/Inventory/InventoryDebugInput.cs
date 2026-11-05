using UnityEngine;

public class InventoryDebugInput : MonoBehaviour
{
    [SerializeField] private InventoryController inventoryController;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private ItemData ammo;
    [SerializeField] private ItemData medkit;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            inventoryController.Inventory.AddItem(ammo, 10);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            inventoryController.Inventory.AddItem(medkit);

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            playerHealth.TakeDamage(25);
            Debug.Log($"Vida: {playerHealth.CurrentHealth}");
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
            Debug.Log($"Vida: {playerHealth.CurrentHealth}");
    }
}