using UnityEngine;

public class PenaltyWall : MonoBehaviour, IDamageable
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField, Min(1)] private int penaltyDamage = 10;

    public void TakeDamage(int amount)
    {
        playerHealth.TakeDamage(penaltyDamage);
    }
}