using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PhaseController : MonoBehaviour
{
    [SerializeField] private float _minTimer, _maxTimer, _timer = 5;
    [SerializeField] private AudioClip _timerClip;
    public static Phase Phase;
    private int _numberOfClicks = 0;
    private List<EnemyPhaseController> _enemies = new List<EnemyPhaseController>();

    public static event Action<Phase> OnPhaseChanged;

    private void Update()
    {
        PlayTimerSound();
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
        else
        {
            ChangePhase();
            SetTimer();
        }
    }

    private void PlayTimerSound()
    {
        if (_timer < 1.5 && _numberOfClicks == 3)
        {
            AudioManager.PlaySoundEffect(_timerClip);
            _numberOfClicks--;
        }
        else if (_timer < 1 && _numberOfClicks == 2)
        {
            AudioManager.PlaySoundEffect(_timerClip);
            _numberOfClicks--;
        }
        else if (_timer < 0.5 && _numberOfClicks == 1)
        {
            AudioManager.PlaySoundEffect(_timerClip);
            _numberOfClicks--;
        }
    }

    private void SetTimer()
    {
        _timer = UnityEngine.Random.Range(_minTimer, _maxTimer);
        _numberOfClicks = 3;
    }

    private void ChangePhase()
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
                var random = UnityEngine.Random.Range(0, 2);
                Phase = (Phase)random;
                break;
        }
        OnPhaseChanged?.Invoke(Phase);
    }

    private Phase? StayOneColor()
    {
        if (EnemyManager.Enemies.Count == 0) return null;
        if (EnemyManager.Enemies.TrueForAll(t => t.GetComponent<EnemyPhaseController>().VulnerablePhase == Phase.Red))
            return Phase.Red;
        else if (EnemyManager.Enemies.TrueForAll(t => t.GetComponent<EnemyPhaseController>().VulnerablePhase == Phase.Blue))
            return Phase.Blue;
        else return null;
    }
}

public enum Phase
{
    Red,
    Blue
}
