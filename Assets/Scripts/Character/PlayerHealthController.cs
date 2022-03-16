using System;
using System.Collections;
using UnityEngine;

public class PlayerHealthController : CharacterHealthController
{
    [SerializeField] private int _maxHealth = 3;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioClip _damagedSound;
    public static event Action OnPlayerDeath;
    public static event Action<int> OnHealthChanged;
    [SerializeField] private CharacterStats _stats;

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

    protected override void SetHealth(int health)
    {
        base.SetHealth(health);
        OnHealthChanged?.Invoke(_health);
    }

    private void OnHealthPickup()
    {
        if (_health == _maxHealth)
            return;
        var health = _health + 1;
        SetHealth(health);
    }

    protected override IEnumerator InvincibilityTimer()
    {
        MakeInvulnerable();
        yield return new WaitForSeconds(_invincibilityTime * _stats.Stats.InvulnerabilityMultiplier);
        MakeVulnerable();
    }
}