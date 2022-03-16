using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Upgrade Pool")]
public class PlayerUpgradePool : ScriptableObject
{
    [SerializeField] private List<PlayerUpgrade> _upgrades = new List<PlayerUpgrade>();

    public List<PlayerUpgrade> Upgrades { get => _upgrades; }
}