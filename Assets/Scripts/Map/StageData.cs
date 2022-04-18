using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "StageData")]
public class StageData : ScriptableObject
{
    [SerializeField] private Tile _floor, _wall;

    public Tile Floor { get => _floor; }
    public Tile Wall { get => _wall; }
}
