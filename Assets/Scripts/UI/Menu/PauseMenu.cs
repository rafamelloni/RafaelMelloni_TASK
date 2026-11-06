using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private InputReader input;
    [SerializeField] private InventoryPanelController inventoryPanel;
    [SerializeField] private GameSaveController saveController;
    [SerializeField] private CinemachineInputAxisController cameraInput;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button quitButton;

    private bool isPaused;

    private void Awake()
    {
        panel.SetActive(false);

        resumeButton.onClick.AddListener(Resume);
        mainMenuButton.onClick.AddListener(GoToMainMenu);
        quitButton.onClick.AddListener(Quit);
    }

    private void Update()
    {
        if (!input.PausePressed)
            return;

        if (isPaused)
        {
            Resume();
            return;
        }

        if (input.IsBlocked)
            return;

        if (inventoryPanel.IsOpen)
            inventoryPanel.Close();
        else
            Pause();
    }

    private void Pause()
    {
        SetPaused(true);
    }

    private void Resume()
    {
        SetPaused(false);
    }

    private void SetPaused(bool paused)
    {
        if (paused)
            CursorState.Show(this);
        else
            CursorState.Hide(this);

        isPaused = paused;
        panel.SetActive(paused);

        Time.timeScale = paused ? 0f : 1f;
        input.IsBlocked = paused;
        cameraInput.enabled = !paused;

        
    }

    private void GoToMainMenu()
    {
        saveController.SaveNow();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Quit()
    {
        Application.Quit();
    }
}