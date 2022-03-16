using System;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private Phase _phase;
    [SerializeField] private float _lifetime, _knockback;
    [SerializeField] protected float _currentLifetime = 0;
    [SerializeField] protected AudioClip _soundEffect;
    public Vector2 AttackDirection;

    public float Knockback { get => _knockback; set => _knockback = value; }

    public static event Action<Vector3> OnDamageDealt;

    protected virtual void Update()
    {
        _currentLifetime += Time.deltaTime;
        if (_currentLifetime >= _lifetime)
        {
            Disappear();
        }
    }

    protected virtual void Disappear()
    {
        Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (transform.IsChildOf(other.transform))
        {
            return;
        }
        if (other.TryGetComponent(out CharacterHealthController healthController))
        {
            if (healthController.Vulnerable)
            {
                DealDamage(healthController);
                KnockbackCharacter(healthController.GetComponent<Rigidbody2D>());
            }
        }
    }

    private void KnockbackCharacter(Rigidbody2D rb)
    {
        var force = AttackDirection * Knockback;
        rb.AddForce(force, ForceMode2D.Impulse);
    }

    public virtual void DealDamage(CharacterHealthController healthController)
    {
        healthController.TakeDamage(_damage);
        AudioManager.PlaySoundEffect(_soundEffect);
        OnDamageDealt?.Invoke(AttackDirection);
    }
}