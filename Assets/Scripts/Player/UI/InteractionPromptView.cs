using TMPro;
using UnityEngine;

public class InteractionPromptView : MonoBehaviour
{
    [SerializeField] private PlayerInteractor interactor;
    [SerializeField] private InputReader input;
    [SerializeField] private TMP_Text promptText;

    private void Update()
    {
        promptText.enabled = interactor.HasTarget && input.IsGameplayEnabled;
    }
}