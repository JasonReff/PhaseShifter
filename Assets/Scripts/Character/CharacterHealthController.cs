using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class CharacterHealthController : MonoBehaviour
{
    [SerializeField] protected int _health;
    [SerializeField] private bool _vulnerable = true;
    [SerializeField] protected float _invincibilityTime = 0.5f, _fadeTime = 1;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private ParticleSystem _bloodParticleSystem;
    [SerializeField] protected AudioClip _deathSound;
    public bool Vulnerable { get => _vulnerable; set => _vulnerable = value; }
    public event Action OnDamage;
    public event Action OnDeath;
    public event Action<int> OnHealthChanged;

    protected virtual void OnEnable()
    {
        OnDamage += OnDamaged;
    }

    protected virtual void OnDisable()
    {
        OnDamage -= OnDamaged;
    }

    protected virtual void Death()
    {
        OnDeath?.Invoke();
        AudioManager.PlaySoundEffect(_deathSound);
        StartCoroutine(FadeOutAndDestroy());
    }

    private IEnumerator FadeOutAndDestroy()
    {
        _spriteRenderer.DOFade(0, _fadeTime);
        yield return new WaitForSeconds(_fadeTime);
        Destroy(gameObject);
    }

    public virtual IEnumerator FallingDeath()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        OnDeath?.Invoke();
        AudioManager.PlaySoundEffect(_deathSound);
        transform.DOScale(0, _fadeTime);
        yield return new WaitForSeconds(_fadeTime);
        Destroy(gameObject);
    }

    protected virtual void OnDamaged()
    {
        _bloodParticleSystem.Play();
    }

    public virtual void TakeDamage(int damage)
    {
        SetHealth(_health - damage);
        OnDamage?.Invoke();
        if (_health <= 0)
        {
            Death();
        }
        StartCoroutine(InvincibilityTimer());
    }

    protected virtual void SetHealth(int health)
    {
        _health = health;
        OnHealthChanged?.Invoke(health);
    }

    protected virtual IEnumerator InvincibilityTimer()
    {
        MakeInvulnerable();
        yield return new WaitForSeconds(_invincibilityTime);
        MakeVulnerable();
    }

    public virtual void MakeVulnerable()
    {
        Vulnerable = true;
        _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, 1);
    }

    public virtual void MakeInvulnerable()
    {
        Vulnerable = false;
        _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, 0.5f);

    }
}
