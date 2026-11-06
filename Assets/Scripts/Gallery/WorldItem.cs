using UnityEngine;

public class WorldItem : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemData item;
    [SerializeField, Min(1)] private int quantity = 1;
    [SerializeField, Min(0)] private int loadedAmmo;
    [SerializeField] private SoundEffect pickUpSound = new SoundEffect();

    public ItemData Item => item;
    public int Quantity => quantity;
    public int LoadedAmmo => loadedAmmo;

    public void Initialize(ItemData newItem, int newQuantity, int newLoadedAmmo)
    {
        item = newItem;
        quantity = newQuantity;
        loadedAmmo = newLoadedAmmo;
    }

    public void Interact(PlayerInteractor interactor)
    {
        int remaining = interactor.Inventory.AddItem(item, quantity, loadedAmmo);

        if (remaining < quantity)
            pickUpSound.Play();

        if (remaining == 0)
            Remove();
        else
            quantity = remaining;
    }

    public void Remove()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}