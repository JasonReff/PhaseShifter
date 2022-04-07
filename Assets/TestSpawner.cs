using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    [SerializeField] private EnemyHealth _enemy;
    [SerializeField] private GameObject _chest;
    [SerializeField] private Vector3 _spawnLocation;

    public void SpawnEnemy()
    {
        Instantiate(_enemy, _spawnLocation, Quaternion.identity);
        EnemyManager.Enemies.Add(_enemy);
    }

    public void SpawnChest()
    {
        Instantiate(_chest, _spawnLocation, Quaternion.identity);
    }
}
