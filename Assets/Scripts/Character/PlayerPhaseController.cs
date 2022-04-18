using UnityEngine;

public class PlayerPhaseController : CharacterPhaseController
{
    [SerializeField] private Animator _animator;
    [SerializeField] private RuntimeAnimatorController _red, _blue, _purple;

    protected override void ChangePhase(Phase phase)
    {
        base.ChangePhase(phase);
        ChangeAnimator(phase);
    }

    private void ChangeAnimator(Phase phase)
    {
        switch (phase)
        {
            case Phase.Red:
                _animator.runtimeAnimatorController = _red;
                break;
            case Phase.Blue:
                _animator.runtimeAnimatorController = _blue;
                break;
            case Phase.Purple:
                _animator.runtimeAnimatorController = _purple;
                break;
        }
    }
}