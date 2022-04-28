using System;
using System.Collections;
using UnityEngine;

public class CharacterMelee : PlayerAttack
{
    [SerializeField] private PlayerHealthController _healthController;
    [SerializeField] private PunchStats _punchStats;
    [SerializeField] private TestSettings _testSettings;
    [SerializeField] private SpriteRenderer _renderer;

    protected override void Awake()
    {
        base.Awake();
        _healthController = GetComponentInParent<PlayerHealthController>();
        BoxColliderSettings();


        void BoxColliderSettings()
        {
            if (_testSettings.BoxCollidersVisible)
                _renderer.sprite = _testSettings.BoxColliderSprite;
            else
                _renderer.sprite = null;
        }
    }

    public override void Attack(Vector2 attackDirection)
    {
        base.Attack(attackDirection);
        MeleeAttack(attackDirection, _healthController, _punchStats);
    }

    public void OnTriggerEnter2D(Collider2D collider)
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
