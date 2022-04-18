using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapSpawner : MonoBehaviour 
{
    [SerializeField] private MapGenerator _generator;
    [SerializeField] private Tilemap _floorTilemap, _wallTilemap;
    [SerializeField] private StageData _stage;

    public static event Action<Room> StartingRoomSpawned;

    public void SpawnMap()
    {
        if (_generator.Rooms.Count == 0)
            _generator.GenerateMapUntilSuccessful();
        StartCoroutine(SpawnRoomCoroutine());
    }
    private IEnumerator SpawnRoomCoroutine()
    {
        foreach (var room in _generator.Rooms)
        {
            SpawnRoom(room, room.GlobalPosition);
        }
        yield return null;
    }

    private void SpawnRoom(Room room, Vector2Int lowerLeft)
    {
        for (int x = 0; x < room.Tiles.GetLength(0); x++)
        {
            for (int y = 0; y < room.Tiles.GetLength(1); y++)
            {
                Vector3Int tileCoords = new Vector3Int(x + lowerLeft.x, y + lowerLeft.y, 0);
                if (room.Tiles[x, y] == TileType.Floor)
                    _floorTilemap.SetTile(tileCoords, _stage.Floor);
                else if (room.Tiles[x, y] == TileType.Wall)
                    _wallTilemap.SetTile(tileCoords, _stage.Wall);
            }
        }
        if (room.Type == RoomType.StartingRoom)
            StartingRoomSpawned?.Invoke(room);
    }
}