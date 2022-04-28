using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : RoomTrigger
{
    [SerializeField] private int _minimumEntryDistance = 3, _minimumSpawnDistance = 3, _enemiesDestroyed = 0;
    private Tilemap _floorTilemap, _wallTilemap;
    [SerializeField] private Tile _wallTile;
    private List<Vector2Int> _wallPositions;
    private List<List<EnemyDifficulty>> _enemyWaves = new List<List<EnemyDifficulty>>();
    private int _enemiesRemaining;

    public static event Action OnCombatStart;
    public static event Action OnCombatEnd;
    public static event Action OnWallsSpawned;
    public static event Action OnWallsDestroyed;
    public static event Action<EnemySpawner> OnSpawnerTriggered;

    public void SetSpawner(List<List<EnemyDifficulty>> enemyWaves, Tilemap floorTilemap, Tilemap wallTilemap)
    {
        foreach (var wave in enemyWaves)
        {
            var enemyWave = new List<EnemyDifficulty>();
            _enemyWaves.Add(enemyWave);
            foreach (var enemy in wave)
                enemyWave.Add(enemy);
        }
        _floorTilemap = floorTilemap;
        _wallTilemap = wallTilemap;
    }

    public void SetBossSpawner(EnemyDifficulty boss, Tilemap floorTilemap, Tilemap wallTilemap)
    {
        var bossWave = new List<EnemyDifficulty>() { boss };
        _enemyWaves.Add(bossWave);
        _floorTilemap = floorTilemap;
        _wallTilemap = wallTilemap;
    }

    private void SpawnEnemies()
    {
        SpawnWave();
        OnCombatStart?.Invoke();
    }

    private void SpawnWave()
    {
        var enemies = _enemyWaves.First();
        var enemyHealthList = new List<EnemyHealth>();
        List<Vector2Int> roomPositions = _room.RandomRoomTilemapPositions(enemies.Count, _minimumEntryDistance, _minimumSpawnDistance);
        for (int i = 0; i < enemies.Count; i++)
        {
            var spawnPosition = _floorTilemap.GetCellCenterWorld((Vector3Int)roomPositions[i]);
            var enemy = Instantiate(enemies[i], spawnPosition, Quaternion.identity);
            enemy.GetComponent<EnemyHealth>().OnDeath += OnEnemyDeath;
            enemyHealthList.Add(enemy.GetComponent<EnemyHealth>());
        }
        EnemyManager.Enemies = enemyHealthList;
        _enemiesRemaining = enemies.Count;
        _enemyWaves.RemoveAt(0);
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
        _enemiesRemaining--;
        if (_enemiesRemaining <= 0)
        {
            SpawnNextWave();
        }
    }

    private void SpawnNextWave()
    {
        if (_enemyWaves.Count > 0)
            SpawnWave();
        else DestroyWalls();
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
        MovePlayer();
        OnSpawnerTriggered?.Invoke(this);
    }

    private void MovePlayer()
    {
        CharacterManager.Instance.Player.transform.position = transform.position;
    }
}
