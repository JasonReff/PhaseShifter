using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private MapParameters _mapParameters;
    [SerializeField] private int _scorePerEnemy = 100, _scorePerLevel = 200;

    private void OnEnable()
    {
        EnemyHealth.EnemyKilled += OnEnemyKilled;
        PortalTrigger.OnPortalEntered += OnLevelCompleted;
    }

    private void OnDisable()
    {
        EnemyHealth.EnemyKilled -= OnEnemyKilled;
        PortalTrigger.OnPortalEntered -= OnLevelCompleted;
    }

    private void AddScore(int score)
    {
        _scoreCounter.CurrentScore += score;
        if (_scoreCounter.CurrentScore > _scoreCounter.HighScore)
            _scoreCounter.HighScore = _scoreCounter.CurrentScore;
    }

    private void OnEnemyKilled()
    {
        AddScore(_scorePerEnemy);
    }

    private void OnLevelCompleted()
    {
        AddScore(_scorePerLevel * _mapParameters.Level);
    }
}
