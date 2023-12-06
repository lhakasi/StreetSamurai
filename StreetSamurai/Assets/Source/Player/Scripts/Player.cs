using States;
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private int _currentHealth;
    [SerializeField] private int _damage;
    [SerializeField] private int _healing;

    private int _maxHealth = 100;

    public event Action<int> MaxHealthEstablished;
    public event Action<int> HealthChanged;

    public PlayerStates State { get; private set; }

    private void Awake()
    {
        _currentHealth = _maxHealth;

        MaxHealthEstablished?.Invoke(_maxHealth);
    }

    public void SetState(PlayerStates state) =>
        State = state;

    public void TakeDamage()
    {
        _currentHealth = Mathf.Clamp(_currentHealth -= _damage, 0, _maxHealth);

        HealthChanged?.Invoke(_currentHealth);
    }

    public void TakeHealing()
    {
        _currentHealth = Mathf.Clamp(_currentHealth += _healing, 0, _maxHealth);

        HealthChanged?.Invoke(_currentHealth);
    }
}