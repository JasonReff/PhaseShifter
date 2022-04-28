using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Trishot")]
public class PlayerUpgradeTrishot : PlayerUpgrade
{
    [SerializeField] private CharacterShoot _characterShoot;
    public override void AddUpgrade()
    {
        base.AddUpgrade();
        _characterShoot = CharacterManager.Instance.Player.GetComponent<CharacterShoot>();
        CharacterShoot.OnPlayerShoot += AdditionalShots;
    }

    public override void RemoveUpgrade()
    {
        base.RemoveUpgrade();
        CharacterShoot.OnPlayerShoot -= AdditionalShots;
    }

    private void AdditionalShots(Vector2 attackDirection)
    {
        _characterShoot.ShootInDirection(Quaternion.Euler(0, 0, 10) * attackDirection, _characterShoot.Projectile, _characterShoot.BulletSpread);
        _characterShoot.ShootInDirection(Quaternion.Euler(0, 0, -10) * attackDirection, _characterShoot.Projectile, _characterShoot.BulletSpread);
    }
}
