using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GalleryTarget : MonoBehaviour, IDamageable
{
    private ShootingGallery gallery;
    private Rigidbody targetRigidbody;
    private Collider targetCollider;
    private Transform originalParent;
    private Vector3 originalLocalPosition;
    private Quaternion originalLocalRotation;

    public bool IsDown { get; private set; }

    private void Awake()
    {
        gallery = GetComponentInParent<ShootingGallery>();
        targetRigidbody = GetComponent<Rigidbody>();
        targetCollider = GetComponent<Collider>();

        originalParent = transform.parent;
        originalLocalPosition = transform.localPosition;
        originalLocalRotation = transform.localRotation;

        targetRigidbody.isKinematic = true;
    }

    public void TakeDamage(int amount)
    {
        if (IsDown || !gallery.CanShootTargets)
            return;

        IsDown = true;
        targetCollider.enabled = false;
        transform.SetParent(null);
        targetRigidbody.isKinematic = false;

        gallery.RegisterTargetDown();
    }

    public void ResetTarget()
    {
        if (!IsDown)
            return;

        targetRigidbody.linearVelocity = Vector3.zero;
        targetRigidbody.angularVelocity = Vector3.zero;
        targetRigidbody.isKinematic = true;

        transform.SetParent(originalParent);
        transform.localPosition = originalLocalPosition;
        transform.localRotation = originalLocalRotation;

        targetCollider.enabled = true;
        IsDown = false;
    }
}