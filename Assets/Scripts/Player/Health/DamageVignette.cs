using UnityEngine;
using UnityEngine.Rendering;

public class DamageVignette : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;

    [Header("Low health")]
    [SerializeField] private Volume lowHealthVolume;
    [SerializeField] private AnimationCurve lowHealthCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
    [SerializeField, Min(0f)] private float lowHealthSpeed = 2f;

    [Header("Damage pulse")]
    [SerializeField] private Volume damagePulseVolume;
    [SerializeField]
    private AnimationCurve pulseCurve = new AnimationCurve(
        new Keyframe(0f, 0f),
        new Keyframe(0.08f, 1f),
        new Keyframe(0.5f, 0f));

    private void Awake()
    {
        lowHealthVolume.weight = 0f;
        damagePulseVolume.weight = 0f;
    }

    private void Update()
    {
        UpdateLowHealth();
        UpdatePulse();
    }

    private void UpdateLowHealth()
    {
        float missingHealth = 1f - (float)playerHealth.CurrentHealth / playerHealth.MaxHealth;
        float targetWeight = lowHealthCurve.Evaluate(missingHealth);

        lowHealthVolume.weight = Mathf.MoveTowards(
            lowHealthVolume.weight,
            targetWeight,
            lowHealthSpeed * Time.unscaledDeltaTime);
    }

    private void UpdatePulse()
    {
        float elapsed = Time.time - playerHealth.LastDamageTime;
        damagePulseVolume.weight = pulseCurve.Evaluate(elapsed);
    }
}