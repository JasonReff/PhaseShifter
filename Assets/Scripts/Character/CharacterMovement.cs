using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Animator _animator;
    
    private bool _canMove = true;
    private Vector2 _input = new Vector2();

    private void OnEnable()
    {
        CharacterMelee.CanMove += SetMovement;
        GetComponent<PlayerHealthController>().OnDeath += OnDeath;
    }

    private void OnDisable()
    {
        CharacterMelee.CanMove -= SetMovement;
        GetComponent<PlayerHealthController>().OnDeath -= OnDeath;
    }

    private void FixedUpdate()
    {
        if (!_canMove)
            return;
        _rb.MovePosition((Vector2)transform.position + _input * _moveSpeed * Time.deltaTime);
    }

    private void Update()
    {
        if (!_canMove)
        {
            _input = Vector2.zero;
            return;
        }
        _input.x = Input.GetAxisRaw("Horizontal");
        _input.y = Input.GetAxisRaw("Vertical");

        MovementAnimation();
    }

    private void MovementAnimation()
    {
        _animator.SetFloat("HorizontalVelocity", _input.x);
        _animator.SetFloat("VerticalVelocity", _input.y);
    }

    private void SetMovement(bool canMove)
    {
        _canMove = canMove;
    }

    private void OnDeath()
    {
        _canMove = false;
    }
}
