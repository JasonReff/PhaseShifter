using System.Collections;
using UnityEngine;

public class EnemyMelee : EnemyAttack
{
    [SerializeField] private float _attackDuration = 0.25f, _attackSpeed, _knockback = 2;

    public override void Attack(Vector2 attackDirection)
    {
        base.Attack(attackDirection);
        GetComponent<Rigidbody2D>().AddForce(attackDirection * _attackSpeed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerHealthController playerHealth))
        {
            if (playerHealth.Vulnerable)
            {
                playerHealth.TakeDamage(1);
                playerHealth.GetComponent<Rigidbody2D>().AddForce(AttackDirection() * _knockback, ForceMode2D.Impulse);
            }
        }
    }
}