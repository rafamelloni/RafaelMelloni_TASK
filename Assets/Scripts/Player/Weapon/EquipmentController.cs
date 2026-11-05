using System;
using UnityEngine;

public class EquipmentController : MonoBehaviour
{
    [SerializeField] private Transform weaponSocket;

    private GameObject equippedModel;

    public ItemInstance EquippedItem { get; private set; }
    public WeaponData EquippedWeapon { get; private set; }

    public event Action OnChanged;

    public void Toggle(ItemInstance item, WeaponData weapon)
    {
        if (EquippedItem == item)
            Unequip();
        else
            Equip(item, weapon);
    }

    public void Equip(ItemInstance item, WeaponData weapon)
    {
        ClearEquippedModel();

        EquippedItem = item;
        EquippedWeapon = weapon;
        equippedModel = Instantiate(weapon.EquippedPrefab, weaponSocket);

        OnChanged?.Invoke();
    }

    public void Unequip()
    {
        ClearEquippedModel();

        EquippedItem = null;
        EquippedWeapon = null;

        OnChanged?.Invoke();
    }

    private void ClearEquippedModel()
    {
        if (equippedModel != null)
            Destroy(equippedModel);

        equippedModel = null;
    }
}