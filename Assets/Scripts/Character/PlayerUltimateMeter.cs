using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUltimateMeter : MonoBehaviour
{
    [SerializeField] private int _currentCharge, _maxCharge;

    public static event Action OnUltimateCharged;
    public static event Action OnUltimateEmptied;
    public static event Action<float> OnUltimatePercentChanged;

    private void OnEnable()
    {
        UltimatePickup.OnPickedUp += ChargeUltimate;
    }

    private void OnDisable()
    {
        UltimatePickup.OnPickedUp -= ChargeUltimate;
    }

    private void ChargeUltimate(int chargeAmount)
    {
        _currentCharge += chargeAmount;
        CheckIfMeterFilled();
        OnUltimatePercentChanged?.Invoke((float)_currentCharge / _maxCharge);
    }

    public void SubtractUltimate(int loss)
    {
        _currentCharge -= loss;
        CheckIfMeterEmpty();
        OnUltimatePercentChanged?.Invoke((float)_currentCharge / _maxCharge);
    }

    private void CheckIfMeterFilled()
    {
        if (_currentCharge >= _maxCharge)
        {
            _currentCharge = _maxCharge;
            OnUltimateCharged?.Invoke();
        }
    }

    private void CheckIfMeterEmpty()
    {
        if (_currentCharge <= 0)
        {
            _currentCharge = 0;
            OnUltimateEmptied?.Invoke();
        }
    }
}
