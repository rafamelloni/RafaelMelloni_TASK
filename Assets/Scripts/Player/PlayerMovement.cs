using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private InputReader input;
    [SerializeField] private Transform cameraTransform;

    [Header("Movement")]
    [SerializeField] private float walkSpeed = 2.5f;
    [SerializeField] private float runSpeed = 5.5f;
    [SerializeField] private float acceleration = 15f;
    [SerializeField] private float rotationSpeed = 720f;

    [Header("Gravity")]
    [SerializeField] private float gravity = -20f;
    [SerializeField] private float groundedVerticalVelocity = -2f;

    private CharacterController controller;
    private Vector3 horizontalVelocity;
    private float verticalVelocity;

    public float CurrentSpeed => horizontalVelocity.magnitude;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector3 moveDirection = GetCameraRelativeDirection();

        UpdateHorizontalVelocity(moveDirection);
        RotateTowards(moveDirection);
        UpdateVerticalVelocity();

        Vector3 velocity = horizontalVelocity + Vector3.up * verticalVelocity;
        controller.Move(velocity * Time.deltaTime);
    }

    private Vector3 GetCameraRelativeDirection()
    {
        Vector3 forward = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up).normalized;
        Vector3 right = Vector3.ProjectOnPlane(cameraTransform.right, Vector3.up).normalized;
        Vector2 moveInput = input.Move;

        return Vector3.ClampMagnitude(forward * moveInput.y + right * moveInput.x, 1f);
    }

    private void UpdateHorizontalVelocity(Vector3 moveDirection)
    {
        float targetSpeed = input.IsSprinting ? runSpeed : walkSpeed;
        Vector3 targetVelocity = moveDirection * targetSpeed;

        horizontalVelocity = Vector3.MoveTowards(
            horizontalVelocity,
            targetVelocity,
            acceleration * Time.deltaTime);
    }

    private void RotateTowards(Vector3 moveDirection)
    {
        if (moveDirection.sqrMagnitude < 0.001f)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime);
    }

    private void UpdateVerticalVelocity()
    {
        if (controller.isGrounded && verticalVelocity < 0f)
            verticalVelocity = groundedVerticalVelocity;

        verticalVelocity += gravity * Time.deltaTime;
    }
}
