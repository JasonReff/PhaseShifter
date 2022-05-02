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
        PlayerInputController.OnMovementInput += MoveCharacter;
        PlayerInputController.OnMovementStop += StopMoving;
        CharacterMelee.CanMove += SetMovement;
        GetComponent<PlayerHealthController>().OnDeath += OnDeath;
    }

    private void OnDisable()
    {
        PlayerInputController.OnMovementInput -= MoveCharacter;
        PlayerInputController.OnMovementStop -= StopMoving;
        CharacterMelee.CanMove -= SetMovement;
        GetComponent<PlayerHealthController>().OnDeath -= OnDeath;
    }

    private void FixedUpdate()
    {
        if (!_canMove)
            return;
        _rb.AddForce(_input * _moveSpeed * Time.deltaTime);
    }

    private void Update()
    {
        if (!_canMove)
        {
            _input = Vector2.zero;
            return;
        }
    }



    private void MoveCharacter(Vector2 input)
    {
        _input = input;
        MovementAnimation(true);
    }

    private void StopMoving()
    {
        _input = Vector2.zero;
        MovementAnimation(false);
    }

    void MovementAnimation(bool isMoving)
    {
        _animator.SetBool("Walking", isMoving);
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
