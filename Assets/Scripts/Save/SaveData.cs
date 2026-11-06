using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SavedSlot
{
    public int slotIndex;
    public string itemId;
    public int quantity;
    public int loadedAmmo;
}

[Serializable]
public class SavedWorldItem
{
    public string itemId;
    public int quantity;
    public int loadedAmmo;
    public Vector3 position;
    public Quaternion rotation;
}

[Serializable]
public class SaveData
{
    public List<SavedSlot> slots = new List<SavedSlot>();
    public int equippedSlotIndex = -1;
    public int health;
    public List<SavedWorldItem> worldItems = new List<SavedWorldItem>();
}