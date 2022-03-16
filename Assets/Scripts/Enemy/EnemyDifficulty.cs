using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDifficulty : MonoBehaviour
{
    [SerializeField] private int _level, _score;

    public int Level { get => _level; }
    public int Score { get => _score; }
}
