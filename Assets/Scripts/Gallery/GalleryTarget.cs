using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GalleryTarget : MonoBehaviour, IDamageable
{
    [SerializeField, Min(0f)] private float respawnDelay = 5f;

    private Rigidbody targetRigidbody;
    private Collider targetCollider;
    private Transform originalParent;
    private Vector3 originalLocalPosition;
    private Quaternion originalLocalRotation;
    private bool isDown;

    private void Awake()
    {
        targetRigidbody = GetComponent<Rigidbody>();
        targetCollider = GetComponent<Collider>();

        originalParent = transform.parent;
        originalLocalPosition = transform.localPosition;
        originalLocalRotation = transform.localRotation;

        targetRigidbody.isKinematic = true;
    }

    public void TakeDamage(int amount)
    {
        if (isDown)
            return;

        isDown = true;
        targetCollider.enabled = false;
        transform.SetParent(null);
        targetRigidbody.isKinematic = false;

        StartCoroutine(RespawnAfterDelay());
    }

    private IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(respawnDelay);

        targetRigidbody.linearVelocity = Vector3.zero;
        targetRigidbody.angularVelocity = Vector3.zero;
        targetRigidbody.isKinematic = true;

        transform.SetParent(originalParent);
        transform.localPosition = originalLocalPosition;
        transform.localRotation = originalLocalRotation;

        targetCollider.enabled = true;
        isDown = false;
    }
}