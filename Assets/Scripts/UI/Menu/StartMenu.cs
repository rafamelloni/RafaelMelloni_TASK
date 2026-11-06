using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(100)]
public class StartMenu : MonoBehaviour
{
    [SerializeField] private GameSaveController saveController;
    [SerializeField] private InputReader input;
    [SerializeField] private CinemachineInputAxisController cameraInput;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button newGameButton;

    private void Awake()
    {
        input.IsBlocked = true;

        continueButton.onClick.AddListener(ContinueGame);
        newGameButton.onClick.AddListener(StartNewGame);
    }

    private void Start()
    {

        CursorState.Show(this);
        continueButton.gameObject.SetActive(SaveSystem.HasSave);
        cameraInput.enabled = false;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void ContinueGame()
    {
        saveController.ContinueGame();
        Close();
    }

    private void StartNewGame()
    {
        saveController.StartNewGame();
        Close();
    }

    private void Close()
    {
        CursorState.Hide(this);
        input.IsBlocked = false;
        cameraInput.enabled = true;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        gameObject.SetActive(false);
    }
}