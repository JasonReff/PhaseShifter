using TMPro;
using UnityEngine;

public class GameEndScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _highScoreText;
    [SerializeField] protected ScoreCounter _scoreCounter;
    [SerializeField] protected MapParameters _mapParameters;

    public virtual void UpdateText()
    {
        _scoreText.text = $"Score: {_scoreCounter.CurrentScore}";
        _highScoreText.text = $"High Score: {_scoreCounter.HighScore}";
    }

    private void Awake()
    {
        UpdateText();
    }
}