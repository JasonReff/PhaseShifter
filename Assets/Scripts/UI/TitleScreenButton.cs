using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenButton : MonoBehaviour
{
    public void BackToTitleScreen()
    {
        Time.timeScale = 1;
        SceneTransition.Instance.LoadScene("Title");
    }
}
