using System;
using System.Collections;
using UnityEngine;

public class PlayerHealthController : CharacterHealthController
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioClip _damagedSound;
    public static event Action OnPlayerDeath;
    public static new event Action<int> OnHealthChanged;
    public static event Action<int> OnMaxHealthChanged;
    [SerializeField] private CharacterStats _stats;
    public override int Health { get => _stats.Stats.CurrentHealth; }

    protected override void OnEnable()
    {
        base.OnEnable();
        HealthPickup.OnHealthPickup += OnHealthPickup;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        HealthPickup.OnHealthPickup -= OnHealthPickup;
    }

    protected override void OnDamaged()
    {
        base.OnDamaged();
        _animator.SetTrigger("Damaged");
        AudioManager.PlaySoundEffect(_damagedSound);
    }

    protected override void Death()
    {
        OnPlayerDeath?.Invoke();
        base.Death();
    }

    public override IEnumerator FallingDeath()
    {
        OnPlayerDeath?.Invoke();
        return base.FallingDeath();
    }

    public void IncreaseMaxHealth(int maxHealthGain)
    {
        _stats.Stats.MaxHealth += maxHealthGain;
        OnMaxHealthChanged?.Invoke(_stats.Stats.MaxHealth);
        var health = Health + maxHealthGain;
        SetHealth(health);
    }

    public void DecreaseMaxHealth(int maxHealthLoss)
    {
        _stats.Stats.MaxHealth -= maxHealthLoss;
        OnMaxHealthChanged?.Invoke(_stats.Stats.MaxHealth);
        if (Health > _stats.Stats.MaxHealth)
            SetHealth(_stats.Stats.MaxHealth);
    }

    protected override void SetHealth(int health)
    {
        base.SetHealth(health);
        OnHealthChanged?.Invoke(health);
    }

    private void OnHealthPickup()
    {
        if (Health == _stats.Stats.MaxHealth)
            return;
        var health = _stats.Stats.CurrentHealth + 1;
        SetHealth(health);
    }

    protected override IEnumerator InvincibilityTimer()
    {
        MakeInvulnerable();
        yield return new WaitForSeconds(_invincibilityTime * _stats.Stats.InvulnerabilityMultiplier);
        MakeVulnerable();
    }
}