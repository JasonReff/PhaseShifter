using UnityEngine;

public abstract class CharacterPhaseController : MonoBehaviour
{
    [SerializeField] private Phase _phase;

    public Phase Phase { get => _phase; }

    protected virtual void ChangePhase(Phase phase)
    {
        _phase = phase;
    }

    protected virtual void OnDisable()
    {
        PhaseController.OnPhaseChanged -= ChangePhase;
    }

    protected virtual void OnEnable()
    {
        PhaseController.OnPhaseChanged += ChangePhase;
    }
}
