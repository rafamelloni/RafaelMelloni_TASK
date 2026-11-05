using UnityEngine;

public class InputReader : MonoBehaviour
{
    public Vector2 Move => new Vector2(
        Input.GetAxisRaw("Horizontal"),
        Input.GetAxisRaw("Vertical"));

    public bool IsSprinting => Input.GetKey(KeyCode.LeftShift);
}
