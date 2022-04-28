using System;
using System.Collections;
using UnityEngine;

public abstract class PlayerAttack : CharacterAttack
{ 
    [SerializeField] private Camera _camera;
    [SerializeField] protected CharacterStats _stats;
    [SerializeField] protected Rigidbody2D _rb;

    public static Action<bool> CanMove;
    public static event Action OnPlayerAttack;
    public static event Action<Vector2> OnPlayerShoot;
    public static event Action OnPlayerMelee;

    private void Awake()
    {
        _rb = GetComponentInParent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _camera = Camera.main;
    }

    public override void Attack(Vector2 attackDirection)
    {
        base.Attack(attackDirection);
        OnPlayerAttack?.Invoke();
    }

    public override Vector2 AttackDirection()
    {
        Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 fireDirection = mousePosition - (Vector2)transform.position;
        return fireDirection.normalized;
    }

    protected virtual void MeleeAttack(Vector2 attackDirection, PlayerHealthController healthController, PunchStats stats)
    {
        StartCoroutine(InvulnerabilityCoroutine());
        StartCoroutine(MoveCoroutine(attackDirection));
        CalculateChargeMultiplier();
        OnPlayerMelee?.Invoke();

        IEnumerator InvulnerabilityCoroutine()
        {
            healthController.MakeInvulnerable();
            yield return new WaitForSeconds(stats.AttackDuration);
            healthController.MakeVulnerable();
        }

        IEnumerator MoveCoroutine(Vector2 attackDirection)
        {
            CanMove?.Invoke(false);
            _rb.AddForce(attackDirection * stats.AttackSpeed, ForceMode2D.Impulse);
            yield return new WaitForSeconds(stats.AttackDuration);
            CanMove?.Invoke(true);
        }

        void CalculateChargeMultiplier()
        {
            if (Chargeable)
            {
                if (ChargeTime > stats.MaxChargeTime)
                    ChargeTime = stats.MaxChargeTime;
                stats.ChargeTimeMultiplier = 1 + ChargeTime;
            }
            else stats.ChargeTimeMultiplier = 1;
        }
    }

    protected virtual void ShootAttack(Vector2 attackDirection, float blowback)
    {
        BlowbackCharacter(attackDirection);
        OnPlayerShoot?.Invoke(attackDirection);

        void BlowbackCharacter(Vector2 attackDirection)
        {
            _rb.AddForce(-attackDirection * blowback, ForceMode2D.Impulse);
        }
    }

    public virtual void ShootInDirection(Vector2 attackDirection, Projectile projectilePrefab, float bulletSpreadAngle)
    {
        attackDirection = DirectionAfterBulletSpread(attackDirection);
        Projectile projectile = Instantiate(projectilePrefab, (Vector2)transform.position + attackDirection, Quaternion.identity);
        projectile.transform.Rotate(0, 0, Vector2.SignedAngle(Vector2.right, attackDirection));
        projectile.AttackDirection = attackDirection;
        projectile.Knockback *= _stats.Stats.KnockbackMultiplier;
        projectile.Rb.AddForce(attackDirection * projectile.ShotSpeed * _stats.Stats.ShotSpeedMultiplier, ForceMode2D.Impulse);

        Vector2 DirectionAfterBulletSpread(Vector2 attackDirection)
        {
            float rotation = UnityEngine.Random.Range(-bulletSpreadAngle / 2, bulletSpreadAngle / 2);
            Vector2 newDirection = Quaternion.Euler(0, 0, rotation) * attackDirection;
            return newDirection;
        }
    }
}
