using UnityEngine;
using System;
using System.Collections.Generic;
public class Inventory 
{
    private readonly InventorySlot[] slots;

    public event Action OnChanged;

    public int SlotCount => slots.Length;

    public Inventory(int slotCount)
    {
        slots = new InventorySlot[slotCount];

        for (int i = 0; i < slotCount; i++)
            slots[i] = new InventorySlot();
    }

    public InventorySlot GetSlot(int index)
    {
        return slots[index];
    }

    public void UseItem(int index, ItemUseContext context)
    {
        InventorySlot slot = slots[index];

        if (slot.IsEmpty)
            return;

        if (slot.Item.Data.TryUse(slot.Item, context))
            RemoveFromSlot(index);
    }

    
    public int AddItem(ItemData data, int quantity = 1, int loadedAmmo = 0)
    {
        int remaining = FillExistingStacks(data, quantity);
        remaining = FillEmptySlots(data, remaining, loadedAmmo);

        if (remaining != quantity)
            OnChanged?.Invoke();

        return remaining;
    }

    public void RemoveFromSlot(int index, int amount = 1)
    {
        InventorySlot slot = slots[index];

        if (slot.IsEmpty)
            return;

        slot.Item.RemoveQuantity(amount);

        if (slot.Item.Quantity == 0)
            slot.Clear();

        OnChanged?.Invoke();
    }

    public void MoveItem(int fromIndex, int toIndex)
    {
        if (fromIndex == toIndex)
            return;

        InventorySlot from = slots[fromIndex];
        InventorySlot to = slots[toIndex];

        if (from.IsEmpty)
            return;

        if (!to.IsEmpty && to.Item.CanStackWith(from.Item))


            MergeStacks(from, to);
        else
            SwapItems(from, to);

        OnChanged?.Invoke();
    }

    private int FillExistingStacks(ItemData data, int quantity)
    {
        int remaining = quantity;

        foreach (InventorySlot slot in slots)
        {
            if (remaining == 0)
                break;

            if (!slot.IsEmpty && slot.Item.Data == data && !slot.Item.IsFull)
                remaining = slot.Item.AddQuantity(remaining);
        }

        return remaining;
    }

    private int FillEmptySlots(ItemData data, int quantity, int loadedAmmo)
    {
        int remaining = quantity;

        foreach (InventorySlot slot in slots)
        {
            if (remaining == 0)
                break;

            if (!slot.IsEmpty)
                continue;

            int amount = Mathf.Min(remaining, data.MaxStack);
            slot.Set(new ItemInstance(data, amount, loadedAmmo));
            remaining -= amount;
        }

        return remaining;
    }

    private void MergeStacks(InventorySlot from, InventorySlot to)
    {
        int leftover = to.Item.AddQuantity(from.Item.Quantity);

        if (leftover == 0)
            from.Clear();
        else
            from.Item.RemoveQuantity(from.Item.Quantity - leftover);
    }

    private void SwapItems(InventorySlot from, InventorySlot to)
    {
        ItemInstance temp = to.Item;
        to.Set(from.Item);
        from.Set(temp);
    }

    public ItemInstance TakeFromSlot(int index)
    {
        InventorySlot slot = slots[index];

        if (slot.IsEmpty)
            return null;

        ItemInstance item = slot.Item;
        slot.Clear();
        OnChanged?.Invoke();

        return item;
    }

    public List<SavedSlot> CreateSaveData()
    {
        List<SavedSlot> savedSlots = new List<SavedSlot>();

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].IsEmpty)
                continue;

            ItemInstance item = slots[i].Item;

            savedSlots.Add(new SavedSlot
            {
                slotIndex = i,
                itemId = item.Data.Id,
                quantity = item.Quantity,
                loadedAmmo = item.LoadedAmmo
            });
        }

        return savedSlots;
    }

    public void LoadSaveData(List<SavedSlot> savedSlots, ItemDataBase database)
    {
        foreach (InventorySlot slot in slots)
            slot.Clear();

        foreach (SavedSlot savedSlot in savedSlots)
        {
            if (savedSlot.slotIndex < 0 || savedSlot.slotIndex >= slots.Length)
                continue;

            ItemData data = database.GetById(savedSlot.itemId);

            if (data == null)
                continue;

            slots[savedSlot.slotIndex].Set(
                new ItemInstance(data, savedSlot.quantity, savedSlot.loadedAmmo));
        }

        OnChanged?.Invoke();
    }

    public int FindSlotIndex(ItemInstance item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].Item == item)
                return i;
        }

        return -1;
    }
}
