using UnityEngine;

public class InputReader : MonoBehaviour
{
    public bool IsGameplayEnabled { get; set; } = true;
    public Vector2 Move => IsGameplayEnabled
        ? new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"))
        : Vector2.zero;

    public bool IsSprinting => Input.GetKey(KeyCode.LeftShift);
    public bool InventoryTogglePressed => Input.GetKeyDown(KeyCode.Tab);
    public bool InteractPressed => IsGameplayEnabled && Input.GetKeyDown(KeyCode.E);
    public bool IsAiming => IsGameplayEnabled && Input.GetMouseButton(1);
    public bool FirePressed => IsGameplayEnabled && Input.GetMouseButtonDown(0);
    public bool ReloadPressed => IsGameplayEnabled && Input.GetKeyDown(KeyCode.R);

}
