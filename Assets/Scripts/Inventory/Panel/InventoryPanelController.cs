using Unity.Cinemachine;
using UnityEngine;

public class InventoryPanelController : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private InputReader input;
    [SerializeField] private CinemachineInputAxisController cameraInput;
    [SerializeField] private InventoryDragController dragController;
    [SerializeField] private ItemContextMenu contextMenu;
    private bool isOpen;

    private void Start()
    {
        SetOpen(false);
    }

    private void Update()
    {
        if (input.InventoryTogglePressed && !dragController.IsDragging)
            SetOpen(!isOpen);
    }

    private void SetOpen(bool open)
    {
        contextMenu.Close();
        isOpen = open;
        panel.SetActive(open);

        input.IsGameplayEnabled = !open;
        cameraInput.enabled = !open;

        Cursor.visible = open;
        Cursor.lockState = open ? CursorLockMode.None : CursorLockMode.Locked;
    }
}