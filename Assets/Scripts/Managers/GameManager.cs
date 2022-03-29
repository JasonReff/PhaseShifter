using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private MapGenerator _mapGenerator; 
    [SerializeField] private CharacterStats _characterStats;
    [SerializeField] private MapParameters _mapParameters;
    [SerializeField] private VictoryScreen _victoryScreen;
    [SerializeField] private GameObject _tutorial, _gameOverScreen;
    [SerializeField] private SpawnerManager _spawnerManager;
    

    private void OnEnable()
    {
        PortalTrigger.OnPortalEntered += ShowVictoryScreen;
        PlayerHealthController.OnPlayerDeath += ShowGameOverScreen;
    }

    private void OnDisable()
    {
        PortalTrigger.OnPortalEntered -= ShowVictoryScreen;
        PlayerHealthController.OnPlayerDeath -= ShowGameOverScreen;
    }

    private void Start()
    {
        _mapParameters.UpdateParameters();
        _mapGenerator.GenerateMapUntilSuccessful();
        _spawnerManager.FillOutMap();
        GetComponent<CameraManager>().FollowPlayer();
        OnGameReset();
    }

    private void ShowVictoryScreen()
    {
        _victoryScreen.gameObject.SetActive(true);
    }

    private void ShowGameOverScreen()
    {
        StartCoroutine(GameOverCoroutine());
    }

    private IEnumerator GameOverCoroutine()
    {
        yield return new WaitForSeconds(2);
        _gameOverScreen.SetActive(true);
    }

    private void OnGameReset()
    {
        if (_mapParameters.Level == 1)
        {
            SetTutorialText();
            _characterStats.ResetStats();
        }
        else _tutorial.gameObject.SetActive(false);
    }

    private void SetTutorialText()
    {
        _tutorial.gameObject.SetActive(true);
        _tutorial.transform.position = CharacterManager.Instance.Player.transform.position;
    }
}
