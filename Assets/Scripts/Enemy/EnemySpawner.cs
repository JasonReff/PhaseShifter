using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : RoomTrigger
{
    [SerializeField] private int _minimumEntryDistance = 3, _minimumSpawnDistance = 3, _enemiesDestroyed = 0;
    private Tilemap _floorTilemap, _wallTilemap;
    [SerializeField] private Tile _wallTile;
    private List<Vector2Int> _wallPositions;
    private List<EnemyDifficulty> _enemies = new List<EnemyDifficulty>();

    public List<EnemyDifficulty> Enemies { get => _enemies; }

    public static event Action OnCombatStart;
    public static event Action OnCombatEnd;
    public static event Action OnWallsSpawned;
    public static event Action OnWallsDestroyed;
    public static event Action<EnemySpawner> OnSpawnerTriggered;

    public void SetSpawner(List<EnemyDifficulty> enemies, Tilemap floorTilemap, Tilemap wallTilemap)
    {
        foreach (var enemy in enemies)
            _enemies.Add(enemy);
        _floorTilemap = floorTilemap;
        _wallTilemap = wallTilemap;
    }

    private void SpawnEnemies()
    {
        List<Vector2Int> roomPositions = _room.RandomRoomTilemapPositions(_enemies.Count, _minimumEntryDistance, _minimumSpawnDistance);
        var enemies = new List<EnemyHealth>();
        for (int i = 0; i < _enemies.Count; i++)
        {
            var spawnPosition = _floorTilemap.GetCellCenterWorld((Vector3Int)roomPositions[i]);
            var enemy = Instantiate(_enemies[i], spawnPosition, Quaternion.identity);
            enemy.GetComponent<EnemyHealth>().OnDeath += OnEnemyDeath;
            enemies.Add(enemy.GetComponent<EnemyHealth>());
        }
        EnemyManager.Enemies = enemies;
        OnCombatStart?.Invoke();
    }

    private void SpawnWalls()
    {
        _wallPositions = new List<Vector2Int>();
        _wallPositions.Add(_room.GetGlobalPositionOfTile(_room.Entry.Position));
        foreach (var exit in _room.Exits)
            if (exit.NextRoom != null)
                _wallPositions.Add(_room.GetGlobalPositionOfTile(exit.Position));
        foreach (var tile in _wallPositions)
            _wallTilemap.SetTile((Vector3Int)tile, _wallTile);
        OnWallsSpawned?.Invoke();
    }

    private void OnEnemyDeath()
    {
        _enemiesDestroyed++;
        if (_enemiesDestroyed >= _enemies.Count)
        {
            DestroyWalls();
        }
    }

    private void DestroyWalls()
    {
        foreach (var tile in _wallPositions)
            _wallTilemap.SetTile((Vector3Int)tile, null);
        OnWallsDestroyed?.Invoke();
        OnCombatEnd?.Invoke();
    }

    protected override void RoomEntered()
    {
        base.RoomEntered();
        SpawnEnemies();
        SpawnWalls();
    }
}
