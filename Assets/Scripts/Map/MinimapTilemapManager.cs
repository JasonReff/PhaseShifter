using UnityEngine;
using UnityEngine.Tilemaps;

public class MinimapTilemapManager : MonoBehaviour
{
    [SerializeField] private Tilemap _minimapTilemap;
    [SerializeField] private Tile _floor, _wall;


    private void OnEnable()
    {
        MinimapTrigger.OnRoomEntered += CopyRoomToTilemap;
        MapGenerator.StartingRoomSpawned += CopyRoomToTilemap;
    }

    private void OnDisable()
    {
        MinimapTrigger.OnRoomEntered -= CopyRoomToTilemap;
        MapGenerator.StartingRoomSpawned -= CopyRoomToTilemap;
    }

    private void CopyRoomToTilemap(Room room)
    {
        for (int x = 0; x < room.Tiles.GetLength(0); x++)
        {
            for (int y = 0; y < room.Tiles.GetLength(1); y++)
            {
                Vector3Int tileCoords = new Vector3Int(x + room.GlobalPosition.x, y + room.GlobalPosition.y, 0);
                if (room.Tiles[x, y] == _floor)
                    _minimapTilemap.SetTile(tileCoords, _floor);
                else if (room.Tiles[x, y] == _wall)
                    _minimapTilemap.SetTile(tileCoords, _wall);
            }
        }
    }
}