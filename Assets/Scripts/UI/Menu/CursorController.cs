using UnityEngine;

public class CursorController : MonoBehaviour
{
    private void Update()
    {
        CursorState.Apply();
    }
}