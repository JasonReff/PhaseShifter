using UnityEngine;

public class EnemyFourwayShoot : EnemyShoot
{

    protected override void OnShoot(Vector2 attackDirection)
    {
        ShootInDirection(Vector2.up);
        ShootInDirection(Vector2.right);
        ShootInDirection(Vector2.left);
        ShootInDirection(Vector2.down);
    }
}