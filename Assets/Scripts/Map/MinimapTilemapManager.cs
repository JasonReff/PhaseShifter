using UnityEngine;
using UnityEngine.Tilemaps;

public class MinimapTilemapManager : MonoBehaviour
{
    [SerializeField] private Tilemap _minimapTilemap;
    [SerializeField] private Tile _floor, _wall;


    private void OnEnable()
    {
        MinimapTrigger.OnRoomEntered += CopyAdjacentRooms;
        MapSpawner.StartingRoomSpawned += CopyAdjacentRooms;
        MapSpawner.StartingRoomSpawned += CopyRoomToTilemap;
    }

    private void OnDisable()
    {
        MinimapTrigger.OnRoomEntered -= CopyAdjacentRooms;
        MapSpawner.StartingRoomSpawned -= CopyAdjacentRooms;
        MapSpawner.StartingRoomSpawned -= CopyRoomToTilemap;
    }

    private void CopyAdjacentRooms(Room room)
    {
        foreach (var doorway in room.Exits)
        {
            CopyRoomToTilemap(doorway.NextRoom);
        }
    }

    private void CopyRoomToTilemap(Room room)
    {
        if (room == null)
            return;
        for (int x = 0; x < room.Tiles.GetLength(0); x++)
        {
            for (int y = 0; y < room.Tiles.GetLength(1); y++)
            {
                Vector3Int tileCoords = new Vector3Int(x + room.GlobalPosition.x, y + room.GlobalPosition.y, 0);
                if (room.Tiles[x, y] == TileType.Floor)
                    _minimapTilemap.SetTile(tileCoords, _floor);
                else if (room.Tiles[x, y] == TileType.Wall)
                    _minimapTilemap.SetTile(tileCoords, _wall);
            }
        }
    }
}