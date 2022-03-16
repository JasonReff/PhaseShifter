using UnityEngine;

public class EnemyThreewayShoot : EnemyShoot
{
    protected override void OnShoot(Vector2 attackDirection)
    {
        ShootInDirection(attackDirection);
        ShootInDirection(Quaternion.Euler(0, 0, 30) * attackDirection);
        ShootInDirection(Quaternion.Euler(0, 0, -30) * attackDirection);
    }
}