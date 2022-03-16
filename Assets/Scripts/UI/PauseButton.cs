using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    [SerializeField] private Sprite _redButton, _blueButton;
    [SerializeField] private Image _pauseButton;
    [SerializeField] private TextMeshProUGUI _scoreText, _highScoreText;
    [SerializeField] private ScoreCounter _scoreCounter;

    private void OnEnable()
    {
        PhaseController.OnPhaseChanged += OnPhaseChange;
        OnPhaseChange(PhaseController.Phase);
    }

    private void OnDisable()
    {
        PhaseController.OnPhaseChanged -= OnPhaseChange;
    }

    private void OnPhaseChange(Phase phase)
    {
        switch (phase)
        {
            case Phase.Red:
                _pauseButton.sprite = _redButton;
                break;
            case Phase.Blue:
                _pauseButton.sprite = _blueButton;
                break;
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        UpdateScores();
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
    }

    private void UpdateScores()
    {
        _scoreText.text = $"Score: {_scoreCounter.CurrentScore}";
        _highScoreText.text = $"High Score: {_scoreCounter.HighScore}";
    }
}
