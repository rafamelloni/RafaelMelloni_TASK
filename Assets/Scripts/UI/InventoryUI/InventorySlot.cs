using UnityEngine;

public class InventorySlot 
{
    public ItemInstance Item { get; private set; }
    public bool IsEmpty => Item == null;

    public void Set(ItemInstance item)
    {
        Item = item;
    }

    public void Clear()
    {
        Item = null;
    }
}
