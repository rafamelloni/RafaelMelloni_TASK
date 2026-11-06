using TMPro;
using UnityEngine;

public class AmmoCounterView : MonoBehaviour
{
    [SerializeField] private EquipmentController equipment;
    [SerializeField] private InventoryController inventoryController;
    [SerializeField] private TMP_Text ammoText;

    private void Update()
    {
        WeaponData weapon = equipment.EquippedWeapon;

        ammoText.enabled = weapon != null;

        if (weapon == null)
            return;

        int loaded = equipment.EquippedItem.LoadedAmmo;
        int reserve = inventoryController.Inventory.CountItem(weapon.AmmoItem);

        ammoText.text = $"{loaded} / {reserve}";
    }
}