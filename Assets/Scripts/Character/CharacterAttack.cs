using UnityEngine;

public abstract class CharacterAttack : MonoBehaviour
{
    [SerializeField] protected float _attackCooldown;
    [SerializeField] protected float _attackTimer;
    public bool Chargeable;
    public float ChargeTime = 0f;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioClip _attackSound;

    public virtual void Attack(Vector2 attackDirection)
    {
        _animator.SetTrigger("Attack");
        _animator.SetFloat("AttackHorizontalVelocity", attackDirection.x);
        if (_attackSound != null)
            AudioManager.PlaySoundEffect(_attackSound);
    }

    public abstract Vector2 AttackDirection();

    public void TryAttack()
    {
        if (_attackTimer <= 0)
        {
            Attack(AttackDirection());
            _attackTimer = _attackCooldown;
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
