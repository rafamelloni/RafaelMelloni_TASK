using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Inventory/Weapon")]
public class WeaponData : ItemData
{
    [SerializeField] private GameObject equippedPrefab;
    [SerializeField] private ItemData ammoItem;
    [SerializeField, Min(1)] private int magazineSize = 12;
    [SerializeField, Min(1)] private int damage = 25;
    [SerializeField, Min(0.01f)] private float fireInterval = 0.3f;

    [Header("Sounds")]
    [SerializeField] private SoundEffect equipSound = new SoundEffect();
    [SerializeField] private SoundEffect unequipSound = new SoundEffect();

    public GameObject EquippedPrefab => equippedPrefab;
    public ItemData AmmoItem => ammoItem;
    public int MagazineSize => magazineSize;
    public int Damage => damage;
    public float FireInterval => fireInterval;

    public override string UseActionName => "Equip";

    public override bool TryUse(ItemInstance instance, ItemUseContext context)
    {
        bool wasEquipped = context.Equipment.EquippedItem == instance;

        context.Equipment.Toggle(instance, this);

        if (wasEquipped)
            unequipSound.Play();
        else
            equipSound.Play();

        return false;
    }
}