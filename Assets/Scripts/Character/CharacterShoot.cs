using System;
using System.Collections;
using UnityEngine;

public class CharacterShoot : PlayerAttack
{
    [SerializeField] private Projectile _projectile;
    [SerializeField] private CharacterStats _stats;
    [SerializeField] private float _bulletSpreadInDegrees;
    public static event Action<Vector2> OnPlayerShoot;
    public override void Attack(Vector2 attackDirection)
    {
        base.Attack(attackDirection);
        ShootInDirection(attackDirection);
        OnPlayerShoot?.Invoke(attackDirection);
    }

    public virtual void ShootInDirection(Vector2 attackDirection)
    {
        attackDirection = DirectionAfterBulletSpread(attackDirection);
        Projectile projectile = Instantiate(_projectile, (Vector2)transform.position + attackDirection, Quaternion.identity);
        projectile.transform.Rotate(0, 0, Vector2.SignedAngle(Vector2.right, attackDirection));
        projectile.AttackDirection = attackDirection;
        projectile.Knockback *= _stats.Stats.KnockbackMultiplier;
        projectile.Rb.AddForce(attackDirection * projectile.ShotSpeed * _stats.Stats.ShotSpeedMultiplier, ForceMode2D.Impulse);
    }

    public Vector2 DirectionAfterBulletSpread(Vector2 attackDirection)
    {
        float rotation = UnityEngine.Random.Range(-_bulletSpreadInDegrees/2, _bulletSpreadInDegrees/2);
        Vector2 newDirection = Quaternion.Euler(0, 0, rotation) * attackDirection;
        return newDirection;
    }

    public IEnumerator BurstCoroutine(Vector2 attackDirection, float shotDelay)
    {
        yield return new WaitForSeconds(shotDelay);
        ShootInDirection(attackDirection);
        yield return new WaitForSeconds(shotDelay);
        ShootInDirection(attackDirection);
    }
}
