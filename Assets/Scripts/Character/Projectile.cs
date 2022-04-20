using System.Collections;
using UnityEngine;

public class Projectile : Attack
{
    [SerializeField] private float _shotSpeed;
    [SerializeField] private float _range;
    [SerializeField] private Rigidbody2D _rb;

    public float ShotSpeed { get => _shotSpeed; }
    public Rigidbody2D Rb { get => _rb; }

    protected override IEnumerator DespawnCoroutine()
    {
        yield return new WaitForSeconds(_shotSpeed * _range);
        Disappear();
    }

    public override void DealDamage(CharacterHealthController healthController)
    {
        base.DealDamage(healthController);
        Disappear();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        Disappear();
    }
}