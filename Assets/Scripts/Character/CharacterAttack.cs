using UnityEngine;

public abstract class CharacterAttack : MonoBehaviour
{
    [SerializeField] protected float _attackTimer;
    public bool Chargeable;
    public float ChargeTime = 0f;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioClip _attackSound;
    [SerializeField] protected WeaponStats _weaponStats;
    protected Vector2 _attackDirection;

    public virtual void Attack(Vector2 attackDirection)
    {
        _animator.SetTrigger("Attack");
        _animator.SetFloat("AttackHorizontalVelocity", attackDirection.CardinalDirection().x);
        _animator.SetFloat("AttackVerticalVelocity", attackDirection.CardinalDirection().y);
        if (_attackSound != null)
            AudioManager.PlaySoundEffect(_attackSound);
    }

    public void TryAttack()
    {
        if (_attackTimer <= 0)
        {
            Attack(transform.forward);
            _attackTimer = _weaponStats.AttackCooldown;
            ChargeTime = 0;
        }
    }

    public void ChargeAttack()
    {
        ChargeTime += Time.deltaTime;
    }

    private void Update()
    {
        if (_attackTimer > 0)
        {
            _attackTimer -= Time.deltaTime;
        }
    }
}
