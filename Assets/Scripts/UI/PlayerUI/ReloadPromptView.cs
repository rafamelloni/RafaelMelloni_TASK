using TMPro;
using UnityEngine;

public class ReloadPromptView : MonoBehaviour
{
    [SerializeField] private WeaponController weapon;
    [SerializeField] private EquipmentController equipment;
    [SerializeField] private InventoryController inventoryController;
    [SerializeField] private TMP_Text promptText;
    [SerializeField, Min(0f)] private float visibleDuration = 1.5f;

    private void Update()
    {
        promptText.enabled = ShouldShow();

        if (!promptText.enabled)
            return;

        WeaponData weaponData = equipment.EquippedWeapon;
        bool hasReserve = inventoryController.Inventory.CountItem(weaponData.AmmoItem) > 0;

        promptText.text = hasReserve ? "Press R to reload" : "No ammo";
    }

    private bool ShouldShow()
    {
        if (equipment.EquippedItem == null)
            return false;

        if (equipment.EquippedItem.LoadedAmmo > 0)
            return false;

        return Time.time - weapon.LastDryFireTime <= visibleDuration;
    }
}