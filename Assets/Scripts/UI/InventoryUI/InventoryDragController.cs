using UnityEngine;
using UnityEngine.UI;

public class InventoryDragController : MonoBehaviour
{
    private const int NoSlot = -1;

    [SerializeField] private InventoryController inventoryController;
    [SerializeField] private Image dragIcon;

    [Header("Inertia")]
    [SerializeField, Min(0.001f)] private float followTime = 0.08f;
    [SerializeField, Min(0f)] private float tiltAmount = 0.01f;
    [SerializeField, Min(0f)] private float maxTilt = 20f;
    [SerializeField, Min(0f)] private float tiltSpeed = 12f;

    [Header("Sounds")]
    [SerializeField] private SoundEffect pickUpSound = new SoundEffect();
    [SerializeField] private SoundEffect dropSound = new SoundEffect();

    private int draggedSlotIndex = NoSlot;
    private Vector3 targetPosition;
    private Vector3 velocity;
    private float currentTilt;

    public bool IsDragging => draggedSlotIndex != NoSlot;

    private void Awake()
    {
        dragIcon.enabled = false;
    }

    private void Update()
    {
        if (!IsDragging)
            return;

        Transform icon = dragIcon.transform;

        icon.position = Vector3.SmoothDamp(
            icon.position,
            targetPosition,
            ref velocity,
            followTime,
            Mathf.Infinity,
            Time.unscaledDeltaTime);

        float targetTilt = Mathf.Clamp(-velocity.x * tiltAmount, -maxTilt, maxTilt);
        currentTilt = Mathf.Lerp(currentTilt, targetTilt, tiltSpeed * Time.unscaledDeltaTime);

        icon.rotation = Quaternion.Euler(0f, 0f, currentTilt);
    }

    public void BeginDrag(int slotIndex, Sprite icon, Vector2 position)
    {
        draggedSlotIndex = slotIndex;
        targetPosition = position;
        velocity = Vector3.zero;
        currentTilt = 0f;

        dragIcon.sprite = icon;
        dragIcon.enabled = true;
        dragIcon.transform.SetPositionAndRotation(position, Quaternion.identity);

        pickUpSound.Play();
    }

    public void UpdateDrag(Vector2 position)
    {
        if (IsDragging)
            targetPosition = position;
    }

    public void DropOn(int targetSlotIndex)
    {
        if (!IsDragging)
            return;

        inventoryController.Inventory.MoveItem(draggedSlotIndex, targetSlotIndex);
    }

    public void EndDrag()
    {
        if (IsDragging)
            dropSound.Play();

        draggedSlotIndex = NoSlot;
        dragIcon.enabled = false;
    }
}