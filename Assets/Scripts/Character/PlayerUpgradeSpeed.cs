using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Speed")]
public class PlayerUpgradeSpeed : PlayerUpgrade
{
    public override void AddUpgrade()
    {
        base.AddUpgrade();
        _stats.Stats.MovementSpeedMultiplier *= 1.1f;
    }

    public override void RemoveUpgrade()
    {
        base.RemoveUpgrade();
        _stats.Stats.MovementSpeedMultiplier /= 1.1f;
    }
}