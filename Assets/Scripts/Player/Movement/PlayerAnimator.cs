using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private static readonly int SpeedHash = Animator.StringToHash("Speed");

    [SerializeField] private PlayerMovement movement;
    [SerializeField] private Animator animator;
    [SerializeField] private float speedDampTime = 0.1f;

    private void Update()
    {
        animator.SetFloat(
            SpeedHash,
            movement.CurrentSpeed,
            speedDampTime,
            Time.deltaTime);
    }
}