using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private InputReader input;
    [SerializeField] private InventoryPanelController inventoryPanel;
    [SerializeField] private GameSaveController saveController;
    [SerializeField] private CinemachineInputAxisController cameraInput;
    [SerializeField] private Volume deathVolume;
    [SerializeField] private GameObject panel;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button quitButton;
    [SerializeField, Min(0.01f)] private float fadeDuration = 1.5f;

    private bool isGameOver;

    private void Awake()
    {
        panel.SetActive(false);
        deathVolume.weight = 0f;

        mainMenuButton.onClick.AddListener(GoToMainMenu);
        quitButton.onClick.AddListener(Quit);
    }

    private void Start()
    {
        playerHealth.OnChanged += CheckDeath;
    }

    private void OnDestroy()
    {
        playerHealth.OnChanged -= CheckDeath;
    }

    private void Update()
    {
        if (!isGameOver)
            return;

        deathVolume.weight = Mathf.MoveTowards(
            deathVolume.weight,
            1f,
            Time.unscaledDeltaTime / fadeDuration);
    }

    private void CheckDeath()
    {
        if (isGameOver || playerHealth.CurrentHealth > 0)
            return;

        TriggerGameOver();
    }

    private void TriggerGameOver()
    {
        isGameOver = true;
        CursorState.Show(this);

        if (inventoryPanel.IsOpen)
            inventoryPanel.Close();

        saveController.ClearSave();

        input.IsBlocked = true;
        cameraInput.enabled = false;
        Time.timeScale = 0f;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        panel.SetActive(true);
    }

    private void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Quit()
    {
        Application.Quit();
    }
}