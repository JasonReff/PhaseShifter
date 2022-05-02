using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackController : MonoBehaviour
{
    [SerializeField] private PlayerAttack _blueAttack;
    [SerializeField] private PlayerAttack _redAttack;
    [SerializeField] private PlayerAttack _ultimateAttack;
    [SerializeField] private PlayerAttack _currentAttack;

    private void OnEnable()
    {
        PlayerInputController.OnAttackStart += AttackStart;
        PlayerInputController.OnAttackStop += AttackStop;
        PhaseController.OnPhaseChanged += ChangeAttack;
    }

    private void OnDisable()
    {
        PlayerInputController.OnAttackStart -= AttackStart;
        PlayerInputController.OnAttackStop -= AttackStop;
        PhaseController.OnPhaseChanged -= ChangeAttack;
    }

    private void AttackStart()
    {
        if (_currentAttack.Chargeable)
        {
            _currentAttack.ChargeAttack();
        }
        else
            _currentAttack.TryAttack();
    }

    private void AttackStop()
    {
        if (_currentAttack.Chargeable)
            _currentAttack.TryAttack();
    }

    private void ChangeAttack(Phase phase)
    {
        switch (phase)
        {
            case Phase.Red:
                _currentAttack = _redAttack;
                break;
            case Phase.Blue:
                _currentAttack = _blueAttack;
                break;
            case Phase.Purple:
                _currentAttack = _ultimateAttack;
                break;
        }
    }

}
