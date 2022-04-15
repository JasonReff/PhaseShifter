using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private MapGenerator _mapGenerator;
    [SerializeField] private MapParameters _mapParameters;
    [SerializeField] private PlayerSpawner _playerSpawner;
    [SerializeField] private Transform _spawnerParent;
    [SerializeField] private EnemySpawner _enemySpawnerPrefab;
    [SerializeField] private Tilemap _floorTilemap, _wallTilemap;
    [SerializeField] private EnemyPools _enemyPools;
    [SerializeField] private List<GameObject> _hazardPrefabs = new List<GameObject>();
    [SerializeField] private PortalTrigger _portalTriggerPrefab;
    [SerializeField] private GameObject _upgradeChest;


    public void FillOutMap()
    {
        _playerSpawner.SpawnPlayer(_mapGenerator.Rooms.First(t => t.Type == RoomType.StartingRoom));
        SetEnemySpawners();
        SetBossSpawner();
        SpawnHazards();
        SpawnPortal();
        SpawnPickups();
    }


    private void SetEnemySpawners()
    {
        List<Room> rooms = _mapGenerator.Rooms.Where(t => t.Type == RoomType.EnemyRoom).ToList();
        foreach (var room in rooms)
        {
            var worldPosition = _floorTilemap.GetCellCenterWorld((Vector3Int)room.EntryTileWorldPosition());
            var enemySpawner = Instantiate(_enemySpawnerPrefab, worldPosition, Quaternion.identity, _spawnerParent);
            enemySpawner.SetSpawner(_enemyPools.GenerateEnemies(_mapParameters.MinimumEncounterDifficulty, _mapParameters.MaximumEncounterDifficulty), _floorTilemap, _wallTilemap);
            enemySpawner.Room = room;
            enemySpawner.GetComponent<MinimapTrigger>().Room = room;
        }
    }

    private void SetBossSpawner()
    {
        var room = _mapGenerator.Rooms.First(t => t.Type == RoomType.BossRoom);
        var worldPosition = _floorTilemap.GetCellCenterWorld((Vector3Int)room.EntryTileWorldPosition());
        var enemySpawner = Instantiate(_enemySpawnerPrefab, worldPosition, Quaternion.identity, _spawnerParent);
        enemySpawner.SetSpawner(new List<EnemyDifficulty>() { _enemyPools.BossList.Rand()}, _floorTilemap, _wallTilemap);
        enemySpawner.Room = room;
        enemySpawner.GetComponent<MinimapTrigger>().Room = room;
    }

    private void SpawnHazards()
    {
        List<Room> rooms = _mapGenerator.Rooms.Where(t => t.Type == RoomType.EnemyRoom).ToList();
        foreach (var room in rooms)
        {
            var numberOfHazards = UnityEngine.Random.Range(0, 5);
            var hazardPositions = room.RandomRoomTilemapPositions(numberOfHazards, 3, 2);
            foreach (var position in hazardPositions)
            {
                var worldPosition = _floorTilemap.GetCellCenterWorld((Vector3Int)position);
                var hazard = Instantiate(_hazardPrefabs.Rand(), worldPosition, Quaternion.identity);
            }
        }
    }

    private void SpawnPortal()
    {
        var room = _mapGenerator.Rooms.First(t => t.Type == RoomType.PortalRoom);
        var worldPosition = _floorTilemap.GetCellCenterWorld((Vector3Int)room.GetCenterOfRoom());
        var portal = Instantiate(_portalTriggerPrefab, worldPosition, Quaternion.identity);
    }

    private void SpawnPickups()
    {
        foreach (var room in _mapGenerator.Rooms.Where(t => t.Type == RoomType.ItemRoom).ToList())
        {
            var worldPosition = _floorTilemap.GetCellCenterWorld((Vector3Int)room.GetCenterOfRoom());
            var item = Instantiate(_upgradeChest, worldPosition, Quaternion.identity);
        }
    }
}