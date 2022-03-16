using UnityEngine;

public abstract class PlayerAttack : CharacterAttack
{ 
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }
    public override Vector2 AttackDirection()
    {
        Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 fireDirection = mousePosition - (Vector2)transform.position;
        return fireDirection.normalized;
    }
}
