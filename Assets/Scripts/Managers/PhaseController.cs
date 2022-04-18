using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class PhaseController : MonoBehaviour
{
    
    [SerializeField] private bool _changePhaseIfOnlyOneEnemyType;
    public static Phase Phase;
    private List<EnemyPhaseController> _enemies = new List<EnemyPhaseController>();
    public static event Action<Phase> OnPhaseChanged;


    private void OnEnable()
    {
        PlayerUltimateMeter.OnUltimateCharged += ChangePhaseToPurple;
        PlayerUltimateMeter.OnUltimateEmptied += RandomPhase;
    }

    private void OnDisable()
    {
        PlayerUltimateMeter.OnUltimateCharged -= ChangePhaseToPurple;
        PlayerUltimateMeter.OnUltimateEmptied -= RandomPhase;
    }

    protected virtual void ChangePhase()
    {
        if (StayOneColor() != null)
        {
            Phase = (Phase)StayOneColor();
            OnPhaseChanged?.Invoke(Phase);
            return;
        }
        switch (Phase)
        {
            case Phase.Blue:
                Phase = Phase.Red;
                break;
            case Phase.Red:
                Phase = Phase.Blue;
                break;
            default:
                break;
        }
        OnPhaseChanged?.Invoke(Phase);
    }

    private Phase? StayOneColor()
    {
        if (_changePhaseIfOnlyOneEnemyType == false)
            return null;
        if (EnemyManager.Enemies.Count == 0) return null;
        if (EnemyManager.Enemies.TrueForAll(t => t.GetComponent<EnemyPhaseController>().VulnerablePhases.Contains(Phase.Red)))
            return Phase.Red;
        else if (EnemyManager.Enemies.TrueForAll(t => t.GetComponent<EnemyPhaseController>().VulnerablePhases.Contains(Phase.Blue)))
            return Phase.Blue;
        else return null;
    }

    private void ChangePhaseToPurple()
    {
        Phase = Phase.Purple;
        OnPhaseChanged?.Invoke(Phase);
    }

    private void RandomPhase()
    {
        Phase = (Phase)UnityEngine.Random.Range(0, 2);
        OnPhaseChanged?.Invoke(Phase);
    }
}

[System.Serializable]
public enum Phase
{
    Red = 0,
    Blue = 1,
    Purple = 2
}
