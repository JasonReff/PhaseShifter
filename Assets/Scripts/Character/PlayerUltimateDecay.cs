using System;
using System.Collections;
using UnityEngine;

public class PlayerUltimateDecay : MonoBehaviour
{
    [SerializeField] private PlayerUltimateMeter _ultimate;
    [SerializeField] private float _decayRate;
    [SerializeField] private int _chargeLoss;
    private bool _decayCoroutine;


    private void OnEnable()
    {
        PlayerUltimateMeter.OnUltimateCharged += StartDecay;
        PlayerUltimateMeter.OnUltimateEmptied += StopDecay;
    }

    private void OnDisable()
    {
        PlayerUltimateMeter.OnUltimateCharged -= StartDecay;
        PlayerUltimateMeter.OnUltimateEmptied -= StopDecay;
    }

    private void Awake()
    {
        StartCoroutine(DecayUltimateCoroutine());
    }

    private void StartDecay()
    {
        if (_decayCoroutine == false)
            _decayCoroutine = true;
    }

    private void StopDecay()
    {
        if (_decayCoroutine)
            _decayCoroutine = false;
    }

    private IEnumerator DecayUltimateCoroutine()
    {
        while (true)
        {
            if (_decayCoroutine)
                _ultimate.SubtractUltimate(_chargeLoss);
            yield return new WaitForSeconds(_decayRate);
        }
    }
}
