using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UltimatePickupSpawner : MonoBehaviour
{
    [SerializeField] private float _spawnDelay;
    [SerializeField] private UltimatePickup _pickup;
    [SerializeField] private Tilemap _floorTilemap;
    private bool _spawning;
    private Room _room;

    private void OnEnable()
    {
        EnemySpawner.OnSpawnerTriggered += StartSpawning;
        EnemySpawner.OnCombatEnd += StopSpawning;
    }

    private void OnDisable()
    {
        EnemySpawner.OnSpawnerTriggered -= StartSpawning;
        EnemySpawner.OnCombatEnd -= StopSpawning;
    }

    private void StartSpawning(EnemySpawner spawner)
    {
        _room = spawner.Room;
        _spawning = true;
        StartCoroutine(SpawnCoroutine());
    }

    private void StopSpawning()
    {
        _spawning = false;
    }

    private IEnumerator SpawnCoroutine()
    {
        while (_spawning && _room != null)
        {
            var tilePosition = (Vector3Int)_room.RandomRoomTilemapPosition(3);
            var position = _floorTilemap.GetCellCenterWorld(tilePosition);
            Instantiate(_pickup, position, Quaternion.identity);
            yield return new WaitForSeconds(_spawnDelay);
        }
    }
}