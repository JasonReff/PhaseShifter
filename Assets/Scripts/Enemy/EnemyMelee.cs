using System.Collections;
using UnityEngine;

public class EnemyMelee : EnemyAttack
{
    [SerializeField] private EnemyMeleeStats _meleeStats;

    public override void Attack(Vector2 attackDirection)
    {
        base.Attack(attackDirection);
        GetComponent<Rigidbody2D>().AddForce(attackDirection * _meleeStats.AttackSpeed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerHealthController playerHealth))
        {
            if (playerHealth.Vulnerable)
            {
                playerHealth.TakeDamage(1);
                playerHealth.GetComponent<Rigidbody2D>().AddForce(_attackDirection * _meleeStats.Knockback, ForceMode2D.Impulse);
            }
        }
    }
}
