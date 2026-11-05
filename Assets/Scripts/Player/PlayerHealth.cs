using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField, Min(1)] private int maxHealth = 100;

    public int CurrentHealth { get; private set; }
    public bool IsFull => CurrentHealth >= maxHealth;

    public event Action OnChanged;

    private void Awake()
    {
        CurrentHealth = maxHealth;
    }

    public void Heal(int amount)
    {
        CurrentHealth = Mathf.Min(CurrentHealth + amount, maxHealth);
        OnChanged?.Invoke();
    }

    public void TakeDamage(int amount)
    {
        CurrentHealth = Mathf.Max(CurrentHealth - amount, 0);
        OnChanged?.Invoke();
    }
}
