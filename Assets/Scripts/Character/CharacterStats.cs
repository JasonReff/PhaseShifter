using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats/CharacterStats")]
public class CharacterStats : ScriptableObject
{
    public Stats Stats = new Stats();
    public List<PlayerUpgrade> Upgrades = new List<PlayerUpgrade>();


    public void ResetStats()
    {
        Stats.MeleeSpeedMultiplier = 1;
        Stats.ShotSpeedMultiplier = 1;
        Stats.InvulnerabilityMultiplier = 1;
        Stats.KnockbackMultiplier = 1;
        Stats.MovementSpeedMultiplier = 1;
    }
}

[System.Serializable]
public struct Stats
{
    public float MeleeSpeedMultiplier, ShotSpeedMultiplier, InvulnerabilityMultiplier, KnockbackMultiplier, MovementSpeedMultiplier;

}
