using UnityEngine;

public class BossLimbHealthController : EnemyHealth
{
    [SerializeField] private BossHealthController bossHealth;

    public override void TakeDamage(int damage)
    {
        bossHealth.TakeDamage(damage);
        base.TakeDamage(damage);
    }

    protected override void Death()
    {
        base.Death();
        bossHealth.Limbs.Remove(this);
    }
}