using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Inventory/Weapon")]
public class WeaponData : ItemData
{
    [SerializeField] private GameObject equippedPrefab;
    [SerializeField, Min(1)] private int magazineSize = 12;
    [SerializeField, Min(1)] private int damage = 25;
    public override string UseActionName => "Equip";
    public GameObject EquippedPrefab => equippedPrefab;
    public int MagazineSize => magazineSize;
    public int Damage => damage;

    public override bool TryUse(ItemInstance instance, ItemUseContext context)
    {
        context.Equipment.Toggle(instance, this);
        return false;
    }
}