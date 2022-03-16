using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Burst")]
public class PlayerUpgradeBurstShot : PlayerUpgrade
{
    [SerializeField] private CharacterShoot _characterShoot;
    private float _shotDelay = 0.2f;

    public override void AddUpgrade()
    {
        base.AddUpgrade();
        _characterShoot = CharacterManager.Instance.Player.GetComponent<CharacterShoot>();
        CharacterShoot.OnPlayerShoot += BurstRounds;
    }

    public override void RemoveUpgrade()
    {
        base.RemoveUpgrade();
        CharacterShoot.OnPlayerShoot -= BurstRounds;
    }

    private void BurstRounds(Vector2 attackDirection)
    {
        _characterShoot.StartCoroutine(_characterShoot.BurstCoroutine(attackDirection, _shotDelay));   
    }
}