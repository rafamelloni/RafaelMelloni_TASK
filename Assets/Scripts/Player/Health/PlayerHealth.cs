using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField, Min(1)] private int maxHealth = 100;

    public int MaxHealth => maxHealth;
    public int CurrentHealth { get; private set; }
    public bool IsFull => CurrentHealth >= maxHealth;
    public float LastDamageTime { get; private set; } = -100f;

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
        LastDamageTime = Time.time;
        OnChanged?.Invoke();
    }

    public void SetHealth(int value)
    {
        CurrentHealth = Mathf.Clamp(value, 0, maxHealth);
        OnChanged?.Invoke();
    }
}