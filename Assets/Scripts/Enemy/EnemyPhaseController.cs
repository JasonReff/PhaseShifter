using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPhaseController : CharacterPhaseController
{
    [SerializeField] private List<Phase> _vulnerablePhases = new List<Phase>();

    public List<Phase> VulnerablePhases { get => _vulnerablePhases; set => _vulnerablePhases = value; }

    public event Action OnVulnerabilityEnded;

    protected override void ChangePhase(Phase phase)
    {
        base.ChangePhase(phase);
        if (_vulnerablePhases.Contains(phase))
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
