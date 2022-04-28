using UnityEngine;

public class PlayerShootMeleeWeapon : PlayerAttack
{
    [SerializeField] private float _blowback, _shotSpread;
    [SerializeField] private Projectile _projectile;
    [SerializeField] private PlayerHealthController _healthController;
    [SerializeField] private PunchStats _meleeStats;
    public override void Attack(Vector2 attackDirection)
    {
        base.Attack(attackDirection);
        ShootAttack(attackDirection, _blowback);
        MeleeAttack(attackDirection, _healthController, _meleeStats);
    }

    protected override void ShootAttack(Vector2 attackDirection, float blowback)
    {
        ShootInDirection(attackDirection, _projectile, _shotSpread);
        base.ShootAttack(attackDirection, blowback);
    }

}