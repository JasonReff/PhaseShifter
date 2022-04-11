using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats/CharacterStats")]
public class CharacterStats : ScriptableObject
{
    public Stats Stats = new Stats();
    [SerializeField] private Stats _defaultStats = new Stats();
    public List<PlayerUpgrade> Upgrades = new List<PlayerUpgrade>();

    private void OnEnable()
    {
        PlayerHealthController.OnHealthChanged += ChangeHealth;
    }

    private void OnDisable()
    {
        PlayerHealthController.OnHealthChanged -= ChangeHealth;
    }

    public void ClearUpgrades()
    {
        for (int i = Upgrades.Count - 1; i >= 0; i--)
            Upgrades[i].RemoveUpgrade();
    }

    public void ResetStats()
    {
        Stats = _defaultStats;
    }

    private void ChangeHealth(int health)
    {
        Stats.CurrentHealth = health;
    }
}

[System.Serializable]
public struct Stats
{
    public float MeleeSpeedMultiplier, ShotSpeedMultiplier, InvulnerabilityMultiplier, KnockbackMultiplier, MovementSpeedMultiplier;
    public int MaxHealth, CurrentHealth;
}
