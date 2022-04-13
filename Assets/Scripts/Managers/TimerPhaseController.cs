using UnityEngine;

public class TimerPhaseController : PhaseController
{
    [SerializeField] private float _minTimer, _maxTimer, _timer = 5;
    private int _numberOfClicks = 0;
    [SerializeField] private AudioClip _timerClip;

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
}
