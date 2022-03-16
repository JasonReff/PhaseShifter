using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room
{
    public int DistanceFromStart;
    public Vector2Int GlobalPosition;
    public Tile[,] Tiles;
    public Doorway Entry;
    public List<Doorway> Exits;

    public Room(Vector2Int position, Tile[,] tiles, Doorway entry, List<Doorway> exits, int distance)
    {
        GlobalPosition = position;
        Tiles = tiles;
        Entry = entry;
        Exits = exits;
        DistanceFromStart = distance;
    }

    public Vector2Int GetGlobalPositionOfTile(Vector2Int tilePosition)
    {
        return new Vector2Int(GlobalPosition.x + tilePosition.x, GlobalPosition.y + tilePosition.y);
    }

    public Vector2Int EntryTileWorldPosition()
    {
        var entryPosition = Entry.Position;
        switch (Entry.Wall)
        {
            case Wall.Top:
                entryPosition += new Vector2Int(0, -1);
                break;
            case Wall.Bottom:
                entryPosition += new Vector2Int(0, 1);
                break;
            case Wall.Left:
                entryPosition += new Vector2Int(1, 0);
                break;
            case Wall.Right:
                entryPosition += new Vector2Int(-1, 0);
                break;
        }
        var worldPosition = GetGlobalPositionOfTile(entryPosition);
        return worldPosition;
    }

    public Vector2Int RandomRoomTilemapPosition(int minimumDistanceFromEntry)
    {
        var tilePosition = new Vector2Int(UnityEngine.Random.Range(1, Tiles.GetLength(0) - 1), UnityEngine.Random.Range(1, Tiles.GetLength(1) - 1));
        while (Mathf.Abs(tilePosition.x - Entry.Position.x) + Mathf.Abs(tilePosition.y - Entry.Position.y) < minimumDistanceFromEntry || TooCloseToExits(tilePosition, minimumDistanceFromEntry))
        {
            tilePosition = new Vector2Int(UnityEngine.Random.Range(1, Tiles.GetLength(0) - 1), UnityEngine.Random.Range(1, Tiles.GetLength(1) - 1));
        }
        var positionInTilemap = GetGlobalPositionOfTile(tilePosition);
        return positionInTilemap;
    }

    public List<Vector2Int> RandomRoomTilemapPositions(int numberOfTiles, int minimumDistanceFromEntry, int minimumSpaceBetweenPositions)
    {
        List<Vector2Int> positions = new List<Vector2Int>();
        for (int i = 0; i < numberOfTiles; i++)
        {
            var position = RandomRoomTilemapPosition(minimumDistanceFromEntry);
            int numberOfLoops = 0;
            int maxLoops = 3;
            while (AreTilesTooClose(position, positions, minimumSpaceBetweenPositions) || TooCloseToExits(position, minimumSpaceBetweenPositions))
            {
                position = RandomRoomTilemapPosition(minimumDistanceFromEntry);
                numberOfLoops++;
                if (numberOfLoops > maxLoops)
                {
                    minimumSpaceBetweenPositions--;
                }
            }
            positions.Add(position);
        }
        return positions;
    }

    private bool AreTilesTooClose(Vector2Int tile, List<Vector2Int> otherTiles, int distance)
    {

        foreach (var otherTile in otherTiles)
        {
            if (Mathf.Abs(tile.x - otherTile.x) + Mathf.Abs(tile.y - otherTile.y) < distance)
            {
                return true;
            }
        }
        return false;
    }

    private bool TooCloseToExits(Vector2Int tilePosition, int distance)
    {
        List<Vector2Int> exitPositions = new List<Vector2Int>();
        foreach (var exit in Exits)
        {
            exitPositions.Add(exit.Position);
        }
        if (AreTilesTooClose(tilePosition, exitPositions, distance))
            return true;
        else return false;
    }

    public Vector2Int GetCenterOfRoom()
    {
        var center = new Vector2Int(Tiles.GetLength(0) / 2, Tiles.GetLength(1) / 2);
        var mapPosition = GetGlobalPositionOfTile(center);
        return mapPosition;
    }
}
