using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private Tilemap _floorTilemap;
    [SerializeField] private GameObject _player, _playerPrefab;

    public void SpawnPlayer(Room startingRoom)
    {
        var worldPosition = _floorTilemap.GetCellCenterWorld((Vector3Int)startingRoom.GetCenterOfRoom());
        _player = Instantiate(_playerPrefab, worldPosition, Quaternion.identity);
        CharacterManager.Instance.Player = _player;
    }
}