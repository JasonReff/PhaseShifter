using System;
using System.Collections;
using UnityEngine;

public class CharacterShoot : PlayerAttack
{
    [SerializeField] private Projectile _projectile;
    [SerializeField] private CharacterStats _stats;
    public static event Action<Vector2> OnPlayerShoot;
    public override void Attack(Vector2 attackDirection)
    {
        base.Attack(attackDirection);
        ShootInDirection(attackDirection);
        OnPlayerShoot?.Invoke(attackDirection);
    }

    public void ShootInDirection(Vector2 attackDirection)
    {
        Projectile projectile = Instantiate(_projectile, (Vector2)transform.position + attackDirection, Quaternion.identity);
        projectile.transform.Rotate(0, 0, Vector2.SignedAngle(Vector2.right, attackDirection));
        projectile.AttackDirection = attackDirection;
        projectile.Knockback *= _stats.Stats.KnockbackMultiplier;
        projectile.Rb.AddForce(attackDirection * projectile.ShotSpeed * _stats.Stats.ShotSpeedMultiplier, ForceMode2D.Impulse);
    }

    public IEnumerator BurstCoroutine(Vector2 attackDirection, float shotDelay)
    {
        yield return new WaitForSeconds(shotDelay);
        ShootInDirection(attackDirection);
        yield return new WaitForSeconds(shotDelay);
        ShootInDirection(attackDirection);
    }
}
