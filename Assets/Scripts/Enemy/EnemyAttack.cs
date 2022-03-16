using UnityEngine;

public class EnemyAttack : CharacterAttack
{
    private Vector2 _directionToPlayer = new Vector2();

    public Vector2 DirectionToPlayer { get => _directionToPlayer; set => _directionToPlayer = value; }

    public override Vector2 AttackDirection()
    {
        return _directionToPlayer;
    }
}
