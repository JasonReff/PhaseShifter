using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Melee Charge")]
public class PlayerUpgradeChargeMelee : PlayerUpgrade
{
    [SerializeField] CharacterMelee _characterMelee;

    public override void AddUpgrade()
    {
        base.AddUpgrade();
        _characterMelee = CharacterManager.Instance.Player.GetComponentInChildren<CharacterMelee>();
        _characterMelee.Chargeable = true;
    }

    public override void RemoveUpgrade()
    {
        base.RemoveUpgrade();
        _characterMelee.Chargeable = false;
    }
}