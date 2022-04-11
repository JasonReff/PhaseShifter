using UnityEngine;

public abstract class PlayerUpgrade : ScriptableObject
{
    [SerializeField] private string _upgradeName, _upgradeDescription;
    [SerializeField] protected CharacterStats _stats;
    [SerializeField] private bool _reusable;

    public string UpgradeName { get => _upgradeName; set => _upgradeName = value; }
    public string UpgradeDescription { get => _upgradeDescription; set => _upgradeDescription = value; }
    public bool Reusable { get => _reusable; }

    public virtual void AddUpgrade()
    {
        _stats.Upgrades.Add(this);
    }

    public virtual void RemoveUpgrade()
    {
        _stats.Upgrades.Remove(this);
    }
}
