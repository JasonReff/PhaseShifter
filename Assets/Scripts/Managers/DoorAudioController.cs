using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAudioController : MonoBehaviour
{
    [SerializeField] private AudioClip _doorsOpen, _doorsClose;

    private void OnEnable()
    {
        EnemySpawner.OnWallsSpawned += OnWallsSpawned;
        EnemySpawner.OnWallsDestroyed += OnWallsDestroyed;
    }

    private void OnDisable()
    {
        EnemySpawner.OnWallsSpawned -= OnWallsSpawned;
        EnemySpawner.OnWallsDestroyed -= OnWallsDestroyed;
    }

    private void OnWallsSpawned()
    {
        AudioManager.PlaySoundEffect(_doorsClose);
    }

    private void OnWallsDestroyed()
    {
        AudioManager.PlaySoundEffect(_doorsOpen);
    }
}
