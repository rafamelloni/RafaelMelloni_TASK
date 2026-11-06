using UnityEngine;

public class PenaltyWall : MonoBehaviour, IDamageable
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField, Min(1)] private int penaltyDamage = 10;
    [SerializeField] private SoundEffect hitSound = new SoundEffect();

    public void TakeDamage(int amount)
    {
        hitSound.Play();
        playerHealth.TakeDamage(penaltyDamage);
    }
}