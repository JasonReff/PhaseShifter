using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private MapParameters _mapParameters;

    public void StartGame()
    {
        _scoreCounter.CurrentScore = 0;
        _mapParameters.Level = 1;
        SceneTransition.Instance.LoadScene("Game");
    }
}