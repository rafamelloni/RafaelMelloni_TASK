using UnityEngine;
using UnityEngine.UI;

public class HealthBarView : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Image fillImage;

    private void Start()
    {
        playerHealth.OnChanged += Refresh;
        Refresh();
    }

    private void OnDestroy()
    {
        playerHealth.OnChanged -= Refresh;
    }

    private void Refresh()
    {
        fillImage.fillAmount = (float)playerHealth.CurrentHealth / playerHealth.MaxHealth;
    }
}