using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyPools")]
public class EnemyPools : ScriptableObject
{
    [SerializeField] private List<EnemyDifficulty> _allEnemies = new List<EnemyDifficulty>();
    [SerializeField] private List<EnemyDifficulty> _bossList = new List<EnemyDifficulty>();
    public List<EnemyDifficulty> BossList { get => _bossList; }

    public List<EnemyDifficulty> GenerateEnemies(int minimumDifficulty, int maximumDifficulty)
    {
        var encounterDifficulty = Random.Range(minimumDifficulty, maximumDifficulty + 1);
        int currentDifficulty = 0;
        var enemies = new List<EnemyDifficulty>();
        while (currentDifficulty < encounterDifficulty)
        {
            var randomEnemy = _allEnemies.Where(t => t.Level <= encounterDifficulty - currentDifficulty).ToList().Rand();
            enemies.Add(randomEnemy);
            currentDifficulty += randomEnemy.Level;
        }
        return enemies;
    }
}

[System.Serializable]
public class EnemyList
{
    public List<GameObject> Enemies = new List<GameObject>();
}