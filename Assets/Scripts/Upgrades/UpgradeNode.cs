using UnityEngine;

public class UpgradeNode : BaseNode
{
    [Input] public int PrerequisiteUpgrade;
    [Output] public int FollowingUpgrades;
    public PlayerUpgrade Upgrade;

    public override PlayerUpgrade GetUpgrade()
    {
        return Upgrade;
    }
}
