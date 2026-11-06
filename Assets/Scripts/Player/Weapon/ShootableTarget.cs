using UnityEngine;

public class ShootableTarget : MonoBehaviour, IDamageable
{
    [SerializeField, Min(1)] private int health = 50;

    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
            Destroy(gameObject);
    }
}