using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private InputReader input;
    [SerializeField] private InventoryController inventoryController;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private LayerMask raycastLayers;
    [SerializeField, Min(0.1f)] private float interactionDistance = 3f;
    [SerializeField, Min(0.1f)] private float maxRayDistance = 15f;
    [SerializeField, Min(0.01f)] private float rayRadius = 0.15f;

    public Inventory Inventory => inventoryController.Inventory;
    public bool HasTarget { get; private set; }

    private void Update()
    {
        IInteractable target = FindLookedAtInteractable();
        HasTarget = target != null;

        if (HasTarget && input.InteractPressed)
            target.Interact(this);
    }

    private IInteractable FindLookedAtInteractable()
    {
        bool hitSomething = Physics.SphereCast(
            cameraTransform.position,
            rayRadius,
            cameraTransform.forward,
            out RaycastHit hit,
            maxRayDistance,
            raycastLayers,
            QueryTriggerInteraction.Collide);

        if (!hitSomething)
            return null;

        float playerDistance = Vector3.Distance(transform.position, hit.point);

        if (playerDistance > interactionDistance)
            return null;

        return hit.collider.GetComponentInParent<IInteractable>();
    }
}