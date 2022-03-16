using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource Music, Sfx;
    [SerializeField] private VolumeSettings _volumeSettings;
    [SerializeField] private float _minPitch = 0.8f, _maxPitch = 1.2f;
    [SerializeField] private AudioClip _titleMusic, _gameMusic;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        if (Instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += ChangeMusicOnSceneChange;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= ChangeMusicOnSceneChange;
    }

    public static void PlayMusic(AudioClip audioClip)
    {
        Instance.Music.clip = audioClip;
        Instance.Music.Play();
    }

    public static void PlaySoundEffect(AudioClip audioClip)
    {
        Instance.Sfx.pitch = UnityEngine.Random.Range(Instance._minPitch, Instance._maxPitch);
        Instance.Sfx.PlayOneShot(audioClip);
    }

    private void ChangeMusicOnSceneChange(Scene scene, LoadSceneMode loadSceneMode)
    {
        AudioClip music = _gameMusic;
        if (scene.name == "Game")
        {
            music = _gameMusic;
        }
        else if (scene.name == "Title")
        {
            music = _titleMusic;
        }
        if (music != Music.clip)
        {
            PlayMusic(music);
        }
    }

}
