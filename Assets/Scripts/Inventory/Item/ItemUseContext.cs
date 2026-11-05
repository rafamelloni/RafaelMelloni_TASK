public class ItemUseContext
{
    public PlayerHealth Health { get; }

    public ItemUseContext(PlayerHealth health)
    {
        Health = health;
    }
}