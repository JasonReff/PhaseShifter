using UnityEngine;

public class PlayerAimController : MonoBehaviour
{
    private Camera _camera;

    private void OnEnable()
    {
        PlayerInputController.OnAimInput += SetDirection;
        PlayerInputController.OnMouseMovement += GetMouseDirection;
        _camera = Camera.main;
    }

    private void OnDisable()
    {
        PlayerInputController.OnAimInput -= SetDirection;
        PlayerInputController.OnMouseMovement -= GetMouseDirection;
    }

    public void SetDirection(Vector2 fireDirection)
    {
        var angle = Vector2.SignedAngle(Vector2.right, fireDirection);
        var rotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = rotation;
    }

    private void GetMouseDirection(Vector2 mousePosition)
    {
        var direction = mousePosition - (Vector2)transform.position;
        SetDirection(direction.normalized);
    }
}