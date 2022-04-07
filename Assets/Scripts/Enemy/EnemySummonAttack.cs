using UnityEngine;

public class EnemySummonAttack : EnemyAttack
{
    [SerializeField] private EnemyHealth _summonedEnemy;

    public override void Attack(Vector2 attackDirection)
    {
        base.Attack(attackDirection);
        SummonEnemy(attackDirection);
    }

    private void SummonEnemy(Vector2 attackDirection)
    {
        Instantiate(_summonedEnemy, transform.position + (Vector3)attackDirection, Quaternion.identity);
        EnemyManager.Enemies.Add(_summonedEnemy);
    }
}