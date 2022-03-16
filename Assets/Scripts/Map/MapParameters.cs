using UnityEngine;

[CreateAssetMenu(menuName = "MapParameters")]
public class MapParameters : ScriptableObject
{
    [SerializeField] public Vector2Int StartingLocation, MinGridSize, MaxGridSize, BossRoomSize;
    [SerializeField] public int MaxDistanceFromStart, MaxNumberOfRooms, Level = 1, MinimumEncounterDifficulty = 1, MaximumEncounterDifficulty = 3;

    public void UpdateParameters()
    {
        MaxDistanceFromStart = 2 + Level;
        MaxNumberOfRooms = 5 * (Level + 1);
        MinGridSize = new Vector2Int(Level + 7, Level + 7);
        MaxGridSize = new Vector2Int(Level + 9, Level + 9);
        MinimumEncounterDifficulty = Level;
        MaximumEncounterDifficulty = Level + 2;
    }

    
}

