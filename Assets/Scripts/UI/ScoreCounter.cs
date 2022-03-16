using UnityEngine;

[CreateAssetMenu(menuName = "ScoreCounter")]
public class ScoreCounter : ScriptableObject
{
    public int CurrentScore = 0, HighScore = 0; 
}