using UnityEngine;

public abstract class PlayerUpgrade : ScriptableObject
{
    [SerializeField] private string _upgradeName, _upgradeDescription;
    [SerializeField] protected CharacterStats _stats;

    public string UpgradeName { get => _upgradeName; set => _upgradeName = value; }
    public string UpgradeDescription { get => _upgradeDescription; set => _upgradeDescription = value; }

    public virtual void AddUpgrade()
    {
        _stats.Upgrades.Add(this);
    }

    public virtual void RemoveUpgrade()
    {
        _stats.Upgrades.Remove(this);
    }
}
