public class ItemUseContext
{
    public PlayerHealth Health { get; }
    public EquipmentController Equipment { get; }

    public ItemUseContext(PlayerHealth health, EquipmentController equipment)
    {
        Health = health;
        Equipment = equipment;
    }
}