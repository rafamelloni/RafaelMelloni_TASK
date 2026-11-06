using UnityEngine;

public class ItemInstance 
{
    public ItemData Data { get; }
    public int Quantity { get; private set; }
    public int LoadedAmmo { get; private set; }

    public bool IsFull => Quantity >= Data.MaxStack;

    public ItemInstance(ItemData data, int quantity = 1, int loadedAmmo = 0)
    {
        Data = data;
        Quantity = Mathf.Clamp(quantity, 1, data.MaxStack);
        LoadedAmmo = Mathf.Max(0, loadedAmmo);
    }

    public bool CanStackWith(ItemInstance other)
    {
        return other != null && other.Data == Data && !IsFull;
    }

    public int AddQuantity(int amount)
    {
        int added = Mathf.Min(amount, Data.MaxStack - Quantity);
        Quantity += added;
        return amount - added;
    }

    public void RemoveQuantity(int amount)
    {
        Quantity = Mathf.Max(0, Quantity - amount);
    }

    public void SetLoadedAmmo(int amount)
    {
        LoadedAmmo = Mathf.Max(0, amount);
    }
}
