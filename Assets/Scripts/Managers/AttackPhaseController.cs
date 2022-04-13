using System.Collections;
using UnityEngine;

public class AttackPhaseController : PhaseController
{
    [SerializeField] private float _phaseChangeDelay = 1;
    private void OnEnable()
    {
        PlayerAttack.OnPlayerAttack += BeginPhaseChange;
    }

    private void OnDisable()
    {
        PlayerAttack.OnPlayerAttack -= BeginPhaseChange;
    }

    private void BeginPhaseChange()
    {
        StartCoroutine(PhaseCountdown());
    }

    private IEnumerator PhaseCountdown()
    {
        yield return new WaitForSeconds(_phaseChangeDelay);
        ChangePhase();
    }
}