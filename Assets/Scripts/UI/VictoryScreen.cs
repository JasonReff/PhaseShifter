using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : GameEndScreen
{
    [SerializeField] private TextMeshProUGUI _levelCompleteText;

    private void Update()
    {
        UpdateText();
    }

    public override void UpdateText()
    {
        base.UpdateText();
        _levelCompleteText.text = $"Level {_mapParameters.Level} Complete!";
    }

    public void GoToNextLevel()
    {
        _mapParameters.Level++;
        SceneTransition.Instance.LoadScene("Game");
    }
}
