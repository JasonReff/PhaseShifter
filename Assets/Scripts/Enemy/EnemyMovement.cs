using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    public void MoveInDirection(Vector2 direction)
    {
        _rb.AddForce(direction * _moveSpeed * Time.deltaTime);
    }
}