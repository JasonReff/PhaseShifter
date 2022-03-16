using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    public void MoveInDirection(Vector2 direction)
    {
        transform.Translate(direction * _moveSpeed * Time.deltaTime);
    }
}