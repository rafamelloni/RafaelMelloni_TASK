using UnityEngine;

public class InputReader : MonoBehaviour
{
    private bool isGameplayEnabled = true;

    public bool IsBlocked { get; set; }

    public bool IsGameplayEnabled
    {
        get => isGameplayEnabled && !IsBlocked;
        set => isGameplayEnabled = value;
    }

    public Vector2 Move => IsGameplayEnabled
        ? new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"))
        : Vector2.zero;

    public bool IsSprinting => IsGameplayEnabled && Input.GetKey(KeyCode.LeftShift);
    public bool IsAiming => IsGameplayEnabled && Input.GetMouseButton(1);
    public bool FirePressed => IsGameplayEnabled && Input.GetMouseButtonDown(0);
    public bool ReloadPressed => IsGameplayEnabled && Input.GetKeyDown(KeyCode.R);
    public bool InteractPressed => IsGameplayEnabled && Input.GetKeyDown(KeyCode.E);
    public bool InventoryTogglePressed => !IsBlocked && Input.GetKeyDown(KeyCode.Tab);

    public bool PausePressed => Input.GetKeyDown(KeyCode.Escape);
}