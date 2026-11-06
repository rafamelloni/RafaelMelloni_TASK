using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Inventory/Item Database")]
public class ItemDataBase : ScriptableObject
{
    [SerializeField] private List<ItemData> items = new List<ItemData>();

    private Dictionary<string, ItemData> itemsById;

    public ItemData GetById(string id)
    {
        if (itemsById == null)
            BuildDictionary();

        itemsById.TryGetValue(id, out ItemData item);
        return item;
    }

    private void BuildDictionary()
    {
        itemsById = new Dictionary<string, ItemData>();

        foreach (ItemData item in items)
            itemsById[item.Id] = item;
    }
}
