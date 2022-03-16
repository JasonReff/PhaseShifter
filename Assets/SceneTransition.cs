using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition Instance;
    [SerializeField] private Animator _animator;
    private bool GameStarted = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneStart;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneStart;
    }
    
    public IEnumerator LoadSceneCoroutine(string sceneName)
    {
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene(sceneName);
    }

    public void LoadScene(string sceneName)
    {
        _animator.SetTrigger("SceneEnd");
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    private void SceneStart(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (GameStarted)
            _animator.SetTrigger("SceneStart");
        else GameStarted = true;
    }
}
