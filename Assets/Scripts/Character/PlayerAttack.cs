using System;
using UnityEngine;

public abstract class PlayerAttack : CharacterAttack
{ 
    [SerializeField] private Camera _camera;

    public static event Action OnPlayerAttack;

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
}
