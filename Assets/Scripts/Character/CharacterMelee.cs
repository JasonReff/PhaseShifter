using System;
using System.Collections;
using UnityEngine;

public class CharacterMelee : PlayerAttack
{
    private bool _dealContactDamage = false;
    [SerializeField] private float _attackDuration = 1f, _attackSpeed = 1f, _knockback = 2, _chargeTimeMultiplier = 1, _maxChargeTime;
    [SerializeField] private CharacterStats _stats;

    public static Action<bool> CanMove;
    public static event Action OnPlayerMelee;

    public override void Attack(Vector2 attackDirection)
    {
        base.Attack(attackDirection);
        StopCoroutine(ContactDamage());
        StartCoroutine(ContactDamage());
        CalculateChargeMultiplier();
        GetComponent<Rigidbody2D>().AddForce(attackDirection * _attackSpeed * _stats.Stats.MeleeSpeedMultiplier * _chargeTimeMultiplier, ForceMode2D.Impulse);
        OnPlayerMelee?.Invoke();
    }

    private IEnumerator ContactDamage()
    {
        _dealContactDamage = true;
        GetComponent<PlayerHealthController>().MakeInvulnerable();
        CanMove?.Invoke(false);
        yield return new WaitForSeconds(_attackDuration);
        _dealContactDamage = false;
        GetComponent<PlayerHealthController>().MakeVulnerable();
        CanMove?.Invoke(true);
    }

    private void CalculateChargeMultiplier()
    {
        if (Chargeable)
        {
            if (ChargeTime > _maxChargeTime)
                ChargeTime = _maxChargeTime;
            _chargeTimeMultiplier = 1 + ChargeTime;
        }
        else _chargeTimeMultiplier = 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_dealContactDamage)
            return;
        if (collision.gameObject.TryGetComponent(out EnemyHealth enemyHealth))
        {
            if (enemyHealth.Vulnerable)
            {
                enemyHealth.TakeDamage(1);
                enemyHealth.GetComponent<Rigidbody2D>().AddForce(AttackDirection() * _knockback * _stats.Stats.KnockbackMultiplier, ForceMode2D.Impulse);
            }
        }
    }
}
