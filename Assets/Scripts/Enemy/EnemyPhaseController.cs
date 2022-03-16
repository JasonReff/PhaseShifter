using System;
using UnityEngine;

public class EnemyPhaseController : CharacterPhaseController
{
    [SerializeField] private Phase _vulnerablePhase;

    public Phase VulnerablePhase { get => _vulnerablePhase; set => _vulnerablePhase = value; }

    public event Action OnVulnerabilityEnded;

    protected override void ChangePhase(Phase phase)
    {
        base.ChangePhase(phase);
        if (phase == _vulnerablePhase)
        {
            MakeVulnerable();
        }
        else 
        { 
            MakeInvulnerable();
            OnVulnerabilityEnded?.Invoke();
        }

    }

    protected override void OnEnable()
    {
        base.OnEnable();
        ChangePhase(PhaseController.Phase);
    }

    protected virtual void MakeVulnerable()
    {
        GetComponent<CharacterHealthController>().MakeVulnerable();
    }

    protected virtual void MakeInvulnerable()
    {
        GetComponent<CharacterHealthController>().MakeInvulnerable();
    }
}
