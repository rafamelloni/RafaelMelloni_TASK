using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AimIK : MonoBehaviour
{
    [SerializeField] private WeaponController weapon;
    [SerializeField, Range(0f, 1f)] private float bodyWeight = 0.6f;
    [SerializeField, Range(0f, 1f)] private float headWeight = 1f;
    [SerializeField, Min(0f)] private float blendSpeed = 8f;

    private Animator animator;
    private float currentWeight;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        float targetWeight = weapon.IsAiming ? 1f : 0f;

        currentWeight = Mathf.MoveTowards(currentWeight, targetWeight, blendSpeed * Time.deltaTime);

        animator.SetLookAtWeight(currentWeight, bodyWeight, headWeight);
        animator.SetLookAtPosition(weapon.AimPoint);
    }
}