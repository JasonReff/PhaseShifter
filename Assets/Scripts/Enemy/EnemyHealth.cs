using System;
using System.Collections;
using UnityEngine;

public class EnemyHealth : CharacterHealthController
{
    [SerializeField] private EnemyPhaseController _phaseController;
    public static event Action EnemyKilled;

    protected override void OnEnable()
    {
        base.OnEnable();
        _phaseController.OnVulnerabilityEnded += MakeInvulnerable;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _phaseController.OnVulnerabilityEnded -= MakeInvulnerable;
    }

    protected override void Death()
    {
        base.Death();
        EnemyManager.Enemies.Remove(this);
        EnemyKilled?.Invoke();
    }

    public override void MakeVulnerable()
    {
        if (_phaseController.VulnerablePhases.Contains(_phaseController.Phase))
        {
            base.MakeVulnerable();
            gameObject.layer = 6;
        }
    }

    public override void MakeInvulnerable()
    {
        base.MakeInvulnerable();
        gameObject.layer = 7;
    }

    public override IEnumerator FallingDeath()
    {
        EnemyManager.Enemies.Remove(this);
        EnemyKilled?.Invoke();
        return base.FallingDeath();
    }
}