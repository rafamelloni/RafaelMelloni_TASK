using UnityEngine;

public class WorldItem : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemData item;
    [SerializeField, Min(1)] private int quantity = 1;
    [SerializeField, Min(0)] private int loadedAmmo;

    public string PromptText => $"E: Levantar {item.DisplayName}";

    public void Initialize(ItemData newItem, int newQuantity, int newLoadedAmmo)
    {
        item = newItem;
        quantity = newQuantity;
        loadedAmmo = newLoadedAmmo;
    }

    public void Interact(PlayerInteractor interactor)
    {
        int remaining = interactor.Inventory.AddItem(item, quantity, loadedAmmo);

        if (remaining == 0)
            Destroy(gameObject);
        else
            quantity = remaining;
    }
}