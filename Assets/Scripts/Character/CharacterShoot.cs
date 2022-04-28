using System;
using System.Collections;
using UnityEngine;

public class CharacterShoot : PlayerAttack
{
    [SerializeField] protected Projectile _projectile;
    [SerializeField] protected float _bulletSpreadInDegrees, _blowback;

    public Projectile Projectile { get => _projectile; }
    public float BulletSpread { get => _bulletSpreadInDegrees; }

    public override void Attack(Vector2 attackDirection)
    {
        base.Attack(attackDirection);
        ShootAttack(attackDirection, _blowback);
    }

    protected override void ShootAttack(Vector2 attackDirection, float blowback)
    {
        ShootInDirection(attackDirection, _projectile, blowback);
        base.ShootAttack(attackDirection, blowback);
    }

    public IEnumerator BurstCoroutine(Vector2 attackDirection, float shotDelay)
    {
        yield return new WaitForSeconds(shotDelay);
        ShootInDirection(attackDirection, _projectile, _blowback);
        yield return new WaitForSeconds(shotDelay);
        ShootInDirection(attackDirection, _projectile, _blowback);
    }
}
