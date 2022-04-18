using UnityEngine;
using UnityEngine.Tilemaps;

public class MinimapTilemapManager : MonoBehaviour
{
    [SerializeField] private Tilemap _minimapTilemap;
    [SerializeField] private Transform _iconParent;
    [SerializeField] private GameObject _bossIcon, _portalIcon, _chestIcon;
    [SerializeField] private StageData _stage;


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
            PlaceIconInRoom(doorway.NextRoom);
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
                    _minimapTilemap.SetTile(tileCoords, _stage.Floor);
                else if (room.Tiles[x, y] == TileType.Wall)
                    _minimapTilemap.SetTile(tileCoords, _stage.Wall);
            }
        }
    }

    private void PlaceIconInRoom(Room room)
    {
        if (room == null)
            return;
        GameObject icon = null;
        switch (room.Type)
        {
            case RoomType.BossRoom:
                icon = _bossIcon;
                break;
            case RoomType.ItemRoom:
                icon = _chestIcon;
                break;
            case RoomType.PortalRoom:
                icon = _portalIcon;
                break;
            default:
                break;
        }
        if (icon == null)
            return;
        var position = _minimapTilemap.CellToWorld((Vector3Int)room.GetCenterOfRoom());
        Instantiate(icon, position, Quaternion.identity, _iconParent);
    }
}