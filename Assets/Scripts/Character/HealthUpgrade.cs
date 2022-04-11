using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/MaxHealthUpgrade")]
public class HealthUpgrade : PlayerUpgrade
{
    [SerializeField] private PlayerHealthController _healthController;
    public override void AddUpgrade()
    {
        base.AddUpgrade();
        _healthController = CharacterManager.Instance.Player.GetComponent<PlayerHealthController>();
        _healthController.IncreaseMaxHealth(1);
    }

    public override void RemoveUpgrade()
    {
        base.RemoveUpgrade();
        _healthController.DecreaseMaxHealth(1);
    }
}