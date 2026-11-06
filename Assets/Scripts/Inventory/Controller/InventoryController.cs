using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField, Min(1)] private int slotCount = 24;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private EquipmentController equipment;
    [SerializeField] private SoundEffect dropSound = new SoundEffect();

    [Header("Drop")]
    [SerializeField] private Transform dropPoint;
    [SerializeField] private LayerMask dropObstacleLayers;
    [SerializeField, Min(0.01f)] private float dropRadius = 0.25f;
    [SerializeField, Min(0.1f)] private float dropDistance = 1.2f;
    [SerializeField, Min(0f)] private float minDropDistance = 0.8f;

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
            GetDropPosition(),
            Quaternion.identity);

        worldItem.Initialize(item.Data, item.Quantity, item.LoadedAmmo);
        dropSound.Play();
    }

    private Vector3 GetDropPosition()
    {
        Vector3 origin = new Vector3(transform.position.x, dropPoint.position.y, transform.position.z);

        Vector3[] directions =
        {
            transform.forward,
            -transform.forward,
            transform.right,
            -transform.right
        };

        foreach (Vector3 direction in directions)
        {
            float freeDistance = Physics.SphereCast(
                origin,
                dropRadius,
                direction,
                out RaycastHit hit,
                dropDistance,
                dropObstacleLayers,
                QueryTriggerInteraction.Ignore)
                ? hit.distance
                : dropDistance;

            if (freeDistance >= minDropDistance)
                return origin + direction * freeDistance;
        }

        return origin + Vector3.up * 1.5f;
    }
}