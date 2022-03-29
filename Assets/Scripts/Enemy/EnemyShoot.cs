using UnityEngine;

public class EnemyShoot : EnemyAttack
{
    [SerializeField] private Projectile _projectile;
    [SerializeField] private EnemyShootStats _shotStats;
    public override void Attack(Vector2 attackDirection)
    {
        base.Attack(attackDirection);
        OnShoot(attackDirection);
    }

    protected virtual void OnShoot(Vector2 attackDirection)
    {
        ShootInDirection(attackDirection);
    }

    protected void ShootInDirection(Vector2 attackDirection)
    {
        Projectile projectile = Instantiate(_projectile, (Vector2)transform.position + attackDirection, Quaternion.identity);
        projectile.AttackDirection = attackDirection;
        projectile.transform.Rotate(0, 0, Vector2.SignedAngle(Vector2.right, attackDirection));
        projectile.Rb.AddForce(attackDirection * projectile.ShotSpeed);
    }
}
