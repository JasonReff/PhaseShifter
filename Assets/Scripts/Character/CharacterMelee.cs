using System;
using System.Collections;
using UnityEngine;

public class CharacterMelee : PlayerAttack
{
    [SerializeField] private PlayerHealthController _healthController;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private CharacterStats _stats;
    [SerializeField] private PunchStats _punchStats;

    public static Action<bool> CanMove;
    public static event Action OnPlayerMelee;

    public override void Attack(Vector2 attackDirection)
    {
        base.Attack(attackDirection);
        StartCoroutine(InvulnerabilityCoroutine());
        StartCoroutine(MoveCoroutine(attackDirection));
        CalculateChargeMultiplier();
        OnPlayerMelee?.Invoke();
    }

    private IEnumerator InvulnerabilityCoroutine()
    {
        _healthController.MakeInvulnerable();
        yield return new WaitForSeconds(_punchStats.AttackDuration);
        _healthController.MakeVulnerable();
    }

    private IEnumerator MoveCoroutine(Vector2 attackDirection)
    {
        CanMove?.Invoke(false);
        _rb.AddForce(attackDirection * _punchStats.AttackSpeed, ForceMode2D.Impulse);
        yield return new WaitForSeconds(_punchStats.AttackDuration);
        CanMove?.Invoke(true);
    }

    private void CalculateChargeMultiplier()
    {
        if (Chargeable)
        {
            if (ChargeTime > _punchStats.MaxChargeTime)
                ChargeTime = _punchStats.MaxChargeTime;
            _punchStats.ChargeTimeMultiplier = 1 + ChargeTime;
        }
        else _punchStats.ChargeTimeMultiplier = 1;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out EnemyHealth enemyHealth))
        {
            if (enemyHealth.Vulnerable)
            {
                enemyHealth.TakeDamage(1);
                enemyHealth.GetComponent<Rigidbody2D>().AddForce(base.AttackDirection() * _punchStats.Knockback * _stats.Stats.KnockbackMultiplier, ForceMode2D.Impulse);
            }
        }
    }
}
