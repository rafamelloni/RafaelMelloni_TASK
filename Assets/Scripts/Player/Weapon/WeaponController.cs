using Unity.Cinemachine;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private static readonly int IsAimingHash = Animator.StringToHash("IsAiming");

    [SerializeField] private InputReader input;
    [SerializeField] private EquipmentController equipment;
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private Animator animator;
    [SerializeField] private CinemachineCamera playerCamera;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private LayerMask aimLayers;
    [SerializeField, Min(1f)] private float aimDistance = 100f;
    [SerializeField] private float normalFieldOfView = 45f;
    [SerializeField] private float aimFieldOfView = 35f;
    [SerializeField, Min(0f)] private float zoomSpeed = 60f;
    [SerializeField] private InventoryController inventoryController;
    [SerializeField] private WeaponEffects effects;

    private float nextFireTime;

    public bool IsAiming { get; private set; }
    public float LastDryFireTime { get; private set; } = -100f;
    public Vector3 AimPoint { get; private set; }

    private void Update()
    {
        IsAiming = input.IsAiming && equipment.EquippedWeapon != null;
        AimPoint = FindAimPoint(out _);

        movement.IsAiming = IsAiming;
        movement.AimPoint = AimPoint;
        animator.SetBool(IsAimingHash, IsAiming);

        UpdateZoom();

        if (IsAiming && input.FirePressed)
            TryFire();
        if (input.ReloadPressed)
            TryReload();
    }

    private Vector3 FindAimPoint(out RaycastHit hit)
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);

        return Physics.Raycast(ray, out hit, aimDistance, aimLayers, QueryTriggerInteraction.Ignore)
            ? hit.point
            : ray.GetPoint(aimDistance);
    }

    private void TryFire()
    {
        if (Time.time < nextFireTime)
            return;

        ItemInstance weaponItem = equipment.EquippedItem;

        if (weaponItem.LoadedAmmo <= 0)
        {
            LastDryFireTime = Time.time;
            return;
        }

        WeaponData weapon = equipment.EquippedWeapon;

        weaponItem.SetLoadedAmmo(weaponItem.LoadedAmmo - 1);
        nextFireTime = Time.time + weapon.FireInterval;

        Vector3 endPoint = FindAimPoint(out RaycastHit hit);

        effects.PlayShot(endPoint);

        if (hit.collider == null)
            return;

        effects.PlayImpact(hit.point, hit.normal);

        IDamageable damageable = hit.collider.GetComponentInParent<IDamageable>();

        if (damageable != null)
            damageable.TakeDamage(weapon.Damage);
    }

    private void UpdateZoom()
    {
        float targetFieldOfView = IsAiming ? aimFieldOfView : normalFieldOfView;

        playerCamera.Lens.FieldOfView = Mathf.MoveTowards(
            playerCamera.Lens.FieldOfView,
            targetFieldOfView,
            zoomSpeed * Time.deltaTime);
    }

    private void TryReload()
    {
        WeaponData weapon = equipment.EquippedWeapon;

        if (weapon == null)
            return;

        ItemInstance weaponItem = equipment.EquippedItem;
        int missingAmmo = weapon.MagazineSize - weaponItem.LoadedAmmo;

        if (missingAmmo <= 0)
            return;

        int loadedFromInventory = inventoryController.Inventory.RemoveItem(weapon.AmmoItem, missingAmmo);

        weaponItem.SetLoadedAmmo(weaponItem.LoadedAmmo + loadedFromInventory);
    }
}