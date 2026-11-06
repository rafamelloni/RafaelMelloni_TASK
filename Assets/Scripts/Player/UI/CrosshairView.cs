using UnityEngine;
using UnityEngine.UI;

public class CrosshairView : MonoBehaviour
{
    [SerializeField] private PlayerInteractor interactor;
    [SerializeField] private InputReader input;
    [SerializeField] private Image image;
    [SerializeField, Min(0f)] private float defaultScale = 1f;
    [SerializeField, Min(0f)] private float targetScale = 2f;
    [SerializeField, Min(0f)] private float scaleSpeed = 12f;

    private void Update()
    {
        image.enabled = input.IsGameplayEnabled;

        float desiredScale = interactor.HasTarget ? targetScale : defaultScale;
        Vector3 desiredVector = Vector3.one * desiredScale;

        image.rectTransform.localScale = Vector3.MoveTowards(
            image.rectTransform.localScale,
            desiredVector,
            scaleSpeed * Time.deltaTime);
    }
}