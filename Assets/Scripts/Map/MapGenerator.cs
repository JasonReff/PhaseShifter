using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    
    [SerializeField] private MapParameters _mapParameters;
    [SerializeField] private int _numberOfGenerations = 0;
    private List<Room> _plannedRooms = new List<Room>(), _enemyRooms = new List<Room>();
    private List<Vector2Int> _tilePositions = new List<Vector2Int>();
    private Room _bossRoom, _startingRoom;

    public List<Room> Rooms { get => _plannedRooms; }
    public List<Vector2Int> TilePositions { get => _tilePositions; }
 


    public void GenerateMapUntilSuccessful()
    {
        bool mapGenerated = GenerateMap();
        while (mapGenerated == false)
        {
            mapGenerated = GenerateMap();
            _numberOfGenerations++;
        }
        Debug.Log($"Map generated in {_numberOfGenerations} loops");
    }

    public bool GenerateMap()
    {
        ClearRooms();
        CreateStartingRoom();
        for (int i = 0; i < _mapParameters.MaxDistanceFromStart; i++)
        {
            CreateRoomSetWithHallways();
            if (_plannedRooms.Where(t => t.Type != RoomType.Hallway).ToList().Count >= _mapParameters.MaxNumberOfRooms)
                break;
        }
        SetEnemyRooms();
        CreateBossRoom();
        bool endingRoomCreated = CreateEndingRoom();
        ChooseItemRooms();
        EraseEmptyExits();
        return endingRoomCreated;
    }

    private void ClearRooms()
    {
        _plannedRooms.Clear();
        _enemyRooms.Clear();
        _tilePositions.Clear();
    }

    public void CreateStartingRoom()
    {
        var roomSize = new Vector2Int(UnityEngine.Random.Range(_mapParameters.MinGridSize.x, _mapParameters.MaxGridSize.x), UnityEngine.Random.Range(_mapParameters.MinGridSize.y, _mapParameters.MaxGridSize.y));
        TileType[,] tiles = new TileType[roomSize.x, roomSize.y];
        for (int x = 0; x < roomSize.x; x++)
        {
            for (int y = 0; y < roomSize.y; y++)
            {
                Vector3Int tileCoordinates = new Vector3Int(x, y, 0);
                if (x == 0 || y == 0 || x == roomSize.x - 1 || y == roomSize.y - 1)
                {
                    tiles[x, y] = TileType.Wall;
                }
                else tiles[x, y] = TileType.Floor;
            }
        }
        List<Doorway> exits = StartingRoomExits(roomSize);
        _startingRoom = new Room(_mapParameters.StartingLocation, tiles, null, exits, 0);
        AddRoomToSpawner(_startingRoom);
        _startingRoom.Type = RoomType.StartingRoom;
        foreach (var exit in exits)
        {
            _startingRoom.Tiles[exit.Position.x, exit.Position.y] = TileType.Floor;
        }
    }

    private void SetEnemyRooms()
    {
        var rooms = _plannedRooms.Where(t => t != _startingRoom && t.Type != RoomType.Hallway).ToList();
        foreach (var room in rooms)
        {
            _enemyRooms.Add(room);
            room.Type = RoomType.EnemyRoom;
        }
    }

    private void CreateBossRoom()
    {
        var rooms = _enemyRooms.OrderBy(t => t.Exits.Count(e => e.NextRoom != null)).ThenByDescending(t => t.DistanceFromStart).ToList();
        var replacedRoom = rooms.First();
        RemoveRoom(replacedRoom);
        _bossRoom = CreateRoom(replacedRoom.Entry.NextRoom, replacedRoom.Entry.NextRoom.Exits.First(t => t.NextRoom == replacedRoom), replacedRoom.Entry.NextRoom.DistanceFromStart, 1, false, true);
        while (_bossRoom.Exits.Count < 1)
        {
            _bossRoom = CreateRoom(replacedRoom.Entry.NextRoom, replacedRoom.Entry.NextRoom.Exits.First(t => t.NextRoom == replacedRoom), replacedRoom.Entry.NextRoom.DistanceFromStart, 1, false, true);
        }
        _bossRoom.Type = RoomType.BossRoom;
        _plannedRooms.Add(_bossRoom);
    }

    private bool CreateEndingRoom()
    {
        var endingRoom = CreateRoom(_bossRoom, _bossRoom.Exits.First(), _bossRoom.DistanceFromStart + 1, 0);
        if (endingRoom == null)
        {
            return false;
        }
        endingRoom.Type = RoomType.PortalRoom;
        _plannedRooms.Add(endingRoom);
        return true;
    }

    private void ChooseItemRooms()
    {
        var numberOfItemRooms = _enemyRooms.Count / 3;
        for (int i = 0; i < numberOfItemRooms; i++)
        {
            var rooms = _enemyRooms.OrderBy(t => t.Exits.Count(e => e.NextRoom != null)).ToList();
            var itemRoom = rooms.First();
            itemRoom.Type = RoomType.ItemRoom;
            _enemyRooms.Remove(itemRoom);
        }
    }

    private void CreateRoomSet()
    {
        IEnumerable<Room> rooms = _plannedRooms;
        foreach (var room in rooms.ToList())
        {
            if (_plannedRooms.Count >= _mapParameters.MaxNumberOfRooms)
                break;
            IEnumerable<Doorway> exits = room.Exits;
            foreach (var exit in exits.ToList())
            {
                if (_plannedRooms.Count >= _mapParameters.MaxNumberOfRooms)
                    break;
                if (exit.NextRoom != null)
                    continue;
                var newRoom = CreateRoom(room, exit, room.DistanceFromStart);
                if (newRoom != null)
                {
                    AddRoomToSpawner(newRoom);
                }
            }
        }
    }

    private void CreateRoomSetWithHallways()
    {
        IEnumerable<Room> rooms = _plannedRooms;
        foreach (var room in rooms.ToList())
        {
            if (_plannedRooms.Count >= _mapParameters.MaxNumberOfRooms)
                break;
            IEnumerable<Doorway> exits = room.Exits;
            foreach (var exit in exits.ToList())
            {
                if (_plannedRooms.Count >= _mapParameters.MaxNumberOfRooms)
                    break;
                if (exit.NextRoom != null)
                    continue;
                var roomAndHallway = CreateRoomWithHallway(room, exit, room.DistanceFromStart);
                if (roomAndHallway == null)
                    continue;
                foreach (var newRoom in roomAndHallway)
                {
                    if (newRoom != null)
                        AddRoomToSpawner(newRoom);
                }
            }
        }
    }

    private void EraseEmptyExits()
    {
        foreach (var room in _plannedRooms.ToList())
        {
            if (room == null)
            {
                _plannedRooms.Remove(room);
                continue;
            }
            foreach (var exit in room.Exits)
            {
                if (exit.NextRoom == null)
                {
                    room.Tiles[exit.Position.x, exit.Position.y] = TileType.Wall;
                }
            }
        }
    }

    public Room CreateRoom(Room previousRoom, Doorway previousExit, int distance, int numberOfExits = 3, bool canShrink = true, bool overrideMaxDistance = false)
    {
        distance++;
        var roomSize = new Vector2Int(UnityEngine.Random.Range(_mapParameters.MinGridSize.x, _mapParameters.MaxGridSize.x), UnityEngine.Random.Range(_mapParameters.MinGridSize.y, _mapParameters.MaxGridSize.y));
        TileType[,] tiles = SetTiles(roomSize);
        Vector2Int shift = RoomShift(previousExit.Wall, roomSize);
        Vector2Int offset = OffsetRoomByOne(previousExit.Wall);
        Doorway entry = CreateRoomEntry(previousExit, shift);
        entry.NextRoom = previousRoom;
        List<Doorway> exits = new List<Doorway>();
        if (distance < _mapParameters.MaxDistanceFromStart || overrideMaxDistance)
               exits = CreateRoomExits(entry, roomSize, numberOfExits);
        Room currentRoom = new Room(previousRoom.GetGlobalPositionOfTile(previousExit.Position) - shift - offset, tiles, entry, exits, distance);
        if (AttemptRoomRecreation(ref currentRoom, roomSize, previousRoom, previousExit, canShrink) == false)
            return null;
        SetEntranceAndExits(currentRoom, previousExit);
        return currentRoom;
    }

    public Room CreateHallway(Room previousRoom, Doorway previousExit)
    {
        var hallwayWidth = _mapParameters.HallwaySize.x;
        var hallwayLength = _mapParameters.HallwaySize.y;
        Vector2Int roomSize = new Vector2Int(hallwayWidth, hallwayLength);
        switch (previousExit.Wall)
        {
            case Wall.Left:
            case Wall.Right:
                roomSize = new Vector2Int(hallwayLength, hallwayWidth);
                break;
        }
        TileType[,] tiles = SetTiles(roomSize);
        Vector2Int shift = RoomShift(previousExit.Wall, roomSize);
        Vector2Int offset = OffsetRoomByOne(previousExit.Wall);
        Doorway entry = CreateRoomEntry(previousExit, shift);
        entry.NextRoom = previousRoom;
        Doorway exit = CreateHallwayExit(entry.Wall, roomSize);
        Room currentRoom = new Room(previousRoom.GetGlobalPositionOfTile(previousExit.Position) - shift - offset, tiles, entry, new List<Doorway> { exit }, 0);
        if (IsRoomObstructed(currentRoom))
            return null;
        SetEntranceAndExits(currentRoom, previousExit);
        currentRoom.Type = RoomType.Hallway;
        return currentRoom;
    }

    public Doorway CreateHallwayExit(Wall entryWall, Vector2Int roomSize)
    {
        var exitWall = Room.GetOppositeWall(entryWall);
        var exit = CreateDoorwayAlongWall(exitWall, roomSize);
        return exit;
    }

    public List<Room> CreateRoomWithHallway(Room previousRoom, Doorway previousExit, int distance, int numberOfExits = 3, bool canShrink = true, bool overrideMaxDistance = false)
    {
        var hallway = CreateHallway(previousRoom, previousExit);
        if (hallway == null)
            return null;
        var exit = hallway.Exits.First();
        var nextRoom = CreateRoom(hallway, exit, distance, numberOfExits, canShrink, overrideMaxDistance);
        if (nextRoom == null)
            return null;
        exit.NextRoom = nextRoom;
        List<Room> rooms = new List<Room> { hallway, nextRoom };
        return rooms;
    }

    private TileType[,] SetTiles(Vector2Int roomSize)
    {
        TileType[,] tiles = new TileType[roomSize.x, roomSize.y];
        for (int x = 0; x < roomSize.x; x++)
        {
            for (int y = 0; y < roomSize.y; y++)
            {
                Vector3Int tileCoordinates = new Vector3Int(x, y, 0);
                if (x == 0 || y == 0 || x == roomSize.x - 1 || y == roomSize.y - 1)
                {
                    tiles[x, y] = TileType.Wall;
                }
                else tiles[x, y] = TileType.Floor;
            }
        }
        return tiles;
    }

    private void SetEntranceAndExits(Room currentRoom, Doorway previousExit)
    {
        currentRoom.Tiles[currentRoom.Entry.Position.x, currentRoom.Entry.Position.y] = TileType.Floor;
        previousExit.NextRoom = currentRoom;
        foreach (var exit in currentRoom.Exits)
        {
            currentRoom.Tiles[exit.Position.x, exit.Position.y] = TileType.Floor;
        }
    }
    private void RemoveRoom(Room room)
    {
        _plannedRooms.Remove(room);
        _enemyRooms.Remove(room);
        for (int x = 0; x < room.Tiles.GetLength(0); x++)
        {
            for (int y = 0; y < room.Tiles.GetLength(1); y++)
            {
                var globalTilePosition = room.GetGlobalPositionOfTile(new Vector2Int(x, y));
                _tilePositions.Remove(globalTilePosition);
            }
        }
    }

    private Room RecreateRoom(Room previousRoom, Doorway previousExit, Vector2Int roomSize)
    {
        TileType[,] tiles = SetTiles(roomSize);
        Vector2Int shift = RoomShift(previousExit.Wall, roomSize);
        Vector2Int offset = OffsetRoomByOne(previousExit.Wall);
        Doorway entry = CreateRoomEntry(previousExit, shift);
        entry.NextRoom = previousRoom;
        List<Doorway> exits = CreateRoomExits(entry, roomSize);
        int distance = previousRoom.DistanceFromStart++;
        Room newRoom = new Room(previousRoom.GetGlobalPositionOfTile(previousExit.Position) - shift - offset, tiles, entry, exits, distance);
        newRoom.Tiles[entry.Position.x, entry.Position.y] = TileType.Floor;
        return newRoom;
    }

    private bool AttemptRoomRecreation(ref Room currentRoom, Vector2Int roomSize, Room previousRoom, Doorway previousExit, bool canShrink)
    {
        int i = 0;
        while (IsRoomObstructed(currentRoom))
        {
            if (canShrink)
            {
                i++;
            }
            Vector2Int smallerGrid = new Vector2Int(roomSize.x - i, roomSize.y - i);
            if (smallerGrid.x < _mapParameters.MinGridSize.x || smallerGrid.y < _mapParameters.MinGridSize.y)
            {
                RemoveExit(previousRoom, previousExit);
                return false;
            }
            currentRoom = RecreateRoom(previousRoom, previousExit, smallerGrid);
        }
        return true;
    }

    private void AddRoomToSpawner(Room room)
    {
        _plannedRooms.Add(room);
        for (int x = 0; x < room.Tiles.GetLength(0); x++)
        {
            for (int y = 0; y < room.Tiles.GetLength(1); y++)
            {
                var position = room.GetGlobalPositionOfTile(new Vector2Int(x, y));
                _tilePositions.Add(position);
            }
        }
    }

    private bool IsRoomObstructed(Room room)
    {
        for (int x = 0; x < room.Tiles.GetLength(0); x++)
        {
            for (int y = 0; y < room.Tiles.GetLength(1); y++)
            {
                var position = room.GetGlobalPositionOfTile(new Vector2Int(x, y));
                if (_tilePositions.Contains(position))
                    return true;
            }
        }
        return false;
    }

    private Doorway CreateRoomEntry(Doorway previousRoomExit, Vector2Int roomShift)
    {
        Wall wall = Wall.Top;
        switch (previousRoomExit.Wall)
        {
            case Wall.Top:
                wall = Wall.Bottom;
                break;
            case Wall.Bottom:
                break;
            case Wall.Left:
                wall = Wall.Right;
                break;
            case Wall.Right:
                wall = Wall.Left;
                break;
        }
        Doorway entry = new Doorway();
        entry.Wall = wall;
        entry.Position = roomShift;
        return entry;
    }

    private Vector2Int RoomShift(Wall wall, Vector2Int roomSize)
    {
        Vector2Int shift = new Vector2Int();
        switch (wall)
        {
            case Wall.Top:
                shift = new Vector2Int(UnityEngine.Random.Range(1, roomSize.x - 1), 0);
                break;
            case Wall.Bottom:
                shift = new Vector2Int(UnityEngine.Random.Range(1, roomSize.x - 1), roomSize.y - 1);
                break;
            case Wall.Left:
                shift = new Vector2Int(roomSize.x - 1, UnityEngine.Random.Range(1, roomSize.y - 1));
                break;
            case Wall.Right:
                shift = new Vector2Int(0, UnityEngine.Random.Range(1, roomSize.y - 1));
                break;
        }
        return shift;
    }

    private Vector2Int OffsetRoomByOne(Wall wall)
    {
        Vector2Int offset = new Vector2Int(0, 0);
        switch (wall)
        {
            case Wall.Top:
                offset.y = -1;
                break;
            case Wall.Bottom:
                offset.y = 1;
                break;
            case Wall.Left:
                offset.x = 1;
                break;
            case Wall.Right:
                offset.x = -1;
                break;
        }
        return offset;
    }

    private List<Doorway> CreateRoomExits(Doorway? entry, Vector2Int gridSize, int exitNumber = 3)
    {
        List<Doorway> exits = new List<Doorway>();
        var numberOfExits = exitNumber;
        List<Doorway> allDoors = new List<Doorway>();
        if (entry != null)
            allDoors.Add(entry);
        foreach (var exit in exits)
            allDoors.Add(exit);
        while (exits.Count < numberOfExits)
        {
            Wall wall = (Wall)UnityEngine.Random.Range(0, 4);
            while (allDoors.Any(t => t.Wall == wall))
            {
                wall = (Wall)UnityEngine.Random.Range(0, 4);
            }
            Doorway exit = CreateDoorwayAlongWall(wall, gridSize);
            exits.Add(exit);
            allDoors.Add(exit);
        }
        return exits;
    }

    private List<Doorway> StartingRoomExits(Vector2Int gridSize)
    {
        List<Doorway> exits = new List<Doorway>();
        var numberOfExits = 4;
        List<Doorway> allDoors = new List<Doorway>();
        foreach (var exit in exits)
            allDoors.Add(exit);
        while (exits.Count < numberOfExits)
        {
            Wall wall = (Wall)UnityEngine.Random.Range(0, 4);
            while (allDoors.Any(t => t.Wall == wall))
            {
                wall = (Wall)UnityEngine.Random.Range(0, 4);
            }
            Doorway exit = CreateDoorwayAlongWall(wall, gridSize);
            exits.Add(exit);
            allDoors.Add(exit);
        }
        return exits;
    }

    private void RemoveExit(Room room, Doorway exit)
    {
        room.Tiles[exit.Position.x, exit.Position.y] = TileType.Wall;
        room.Exits.Remove(exit);
    }
    private Doorway CreateDoorwayAlongWall(Wall wall, Vector2Int gridSize)
    {
        var door = new Doorway();
        switch (wall)
        {
            case Wall.Bottom:
                door.Position = new Vector2Int(UnityEngine.Random.Range(1, gridSize.x - 1), 0);
                break;
            case Wall.Left:
                door.Position = new Vector2Int(0, UnityEngine.Random.Range(1, gridSize.y - 1));
                break;
            case Wall.Top:
                door.Position = new Vector2Int(UnityEngine.Random.Range(1, gridSize.x - 1), gridSize.y - 1);
                break;
            case Wall.Right:
                door.Position = new Vector2Int(gridSize.x - 1, UnityEngine.Random.Range(1, gridSize.y - 1));
                break;
        }
        door.Wall = wall;
        return door;
    }



}
