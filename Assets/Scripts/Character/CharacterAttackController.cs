using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackController : MonoBehaviour
{
    [SerializeField] private PlayerAttack _blueAttack, _redAttack, _currentAttack;

    private void OnEnable()
    {
        PhaseController.OnPhaseChanged += ChangeAttack;
    }

    private void OnDisable()
    {
        PhaseController.OnPhaseChanged -= ChangeAttack;
    }

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            if (_currentAttack.Chargeable)
            {
                _currentAttack.ChargeAttack();
            }
            else
                _currentAttack.TryAttack();
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            if (_currentAttack.Chargeable)
            {
                _currentAttack.TryAttack();
            }
        }
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
        }
    }

}
