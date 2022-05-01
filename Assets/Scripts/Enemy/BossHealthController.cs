using System;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthController : EnemyHealth
{
    [SerializeField] public readonly List<BossLimbHealthController> Limbs = new List<BossLimbHealthController>();
    [SerializeField] private int _maxHealth;
    public static event Action<float> OnBossHealthChanged;
    public static event Action OnBossDeath;

    protected override void OnEnable()
    {
        base.OnEnable();
        OnHealthChanged += GetBossHealthPercent;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        OnHealthChanged -= GetBossHealthPercent;
    }

    private void Awake()
    {
        foreach (var limbHealth in GetComponentsInChildren<BossLimbHealthController>())
            Limbs.Add(limbHealth);
    }

    private void GetBossHealthPercent(int health)
    {
        float percent = (float)health / _maxHealth;
        OnBossHealthChanged?.Invoke(percent);
    }

    protected override void Death()
    {
        base.Death();
        OnBossDeath?.Invoke();
    }
}
