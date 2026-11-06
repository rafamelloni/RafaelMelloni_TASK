using System.Collections.Generic;
using UnityEngine;

public class GameSaveController : MonoBehaviour
{
    [SerializeField] private InventoryController inventoryController;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private EquipmentController equipment;
    [SerializeField] private ItemDataBase itemDatabase;

    private Inventory inventory;
    private bool hasStarted;
    private bool hasPendingSave;

    private void Start()
    {
        inventory = inventoryController.Inventory;
    }

    private void LateUpdate()
    {
        if (!hasPendingSave)
            return;

        hasPendingSave = false;
        Save();
    }

    private void OnDestroy()
    {
        StopSaving();
    }

    private void OnApplicationQuit()
    {
        if (hasStarted)
            Save();
    }

    public void StartNewGame()
    {
        SaveSystem.Delete();
        BeginSaving();
    }

    public void ContinueGame()
    {
        Load();
        BeginSaving();
    }

    public void SaveNow()
    {
        if (hasStarted)
            Save();
    }

    public void ClearSave()
    {
        StopSaving();
        SaveSystem.Delete();
    }

    private void BeginSaving()
    {
        hasStarted = true;

        inventory.OnChanged += RequestSave;
        playerHealth.OnChanged += RequestSave;
        equipment.OnChanged += RequestSave;
    }

    private void StopSaving()
    {
        if (!hasStarted)
            return;

        hasStarted = false;
        hasPendingSave = false;

        inventory.OnChanged -= RequestSave;
        playerHealth.OnChanged -= RequestSave;
        equipment.OnChanged -= RequestSave;
    }

    private void RequestSave()
    {
        if (hasStarted)
            hasPendingSave = true;
    }

    private void Save()
    {
        SaveData data = new SaveData
        {
            slots = inventory.CreateSaveData(),
            equippedSlotIndex = GetEquippedSlotIndex(),
            health = playerHealth.CurrentHealth,
            worldItems = CreateWorldItemsData()
        };

        SaveSystem.Save(data);
    }

    private void Load()
    {
        if (!SaveSystem.TryLoad(out SaveData data))
            return;

        inventory.LoadSaveData(data.slots, itemDatabase);
        playerHealth.SetHealth(data.health);
        RestoreEquippedItem(data.equippedSlotIndex);
        RestoreWorldItems(data.worldItems);
    }

    private List<SavedWorldItem> CreateWorldItemsData()
    {
        List<SavedWorldItem> savedItems = new List<SavedWorldItem>();
        WorldItem[] worldItems = FindObjectsByType<WorldItem>(
            FindObjectsInactive.Exclude,
            FindObjectsSortMode.None);

        foreach (WorldItem worldItem in worldItems)
        {
            savedItems.Add(new SavedWorldItem
            {
                itemId = worldItem.Item.Id,
                quantity = worldItem.Quantity,
                loadedAmmo = worldItem.LoadedAmmo,
                position = worldItem.transform.position,
                rotation = worldItem.transform.rotation
            });
        }

        return savedItems;
    }

    private void RestoreWorldItems(List<SavedWorldItem> savedItems)
    {
        WorldItem[] sceneItems = FindObjectsByType<WorldItem>(
            FindObjectsInactive.Exclude,
            FindObjectsSortMode.None);

        foreach (WorldItem sceneItem in sceneItems)
            sceneItem.Remove();

        foreach (SavedWorldItem savedItem in savedItems)
        {
            ItemData data = itemDatabase.GetById(savedItem.itemId);

            if (data == null || data.WorldPrefab == null)
                continue;

            WorldItem worldItem = Instantiate(
                data.WorldPrefab,
                savedItem.position,
                savedItem.rotation);

            worldItem.Initialize(data, savedItem.quantity, savedItem.loadedAmmo);
        }
    }

    private int GetEquippedSlotIndex()
    {
        if (equipment.EquippedItem == null)
            return -1;

        return inventory.FindSlotIndex(equipment.EquippedItem);
    }

    private void RestoreEquippedItem(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= inventory.SlotCount)
            return;

        InventorySlot slot = inventory.GetSlot(slotIndex);

        if (!slot.IsEmpty && slot.Item.Data is WeaponData weapon)
            equipment.Equip(slot.Item, weapon);
    }

    [ContextMenu("Delete Save")]
    private void DeleteSave()
    {
        SaveSystem.Delete();
    }
}