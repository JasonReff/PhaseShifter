using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private InputActionReference _movement, _attack, _aiming;
    private Camera _camera;
    public static event Action<Vector2> OnMovementInput, OnAimInput, OnMouseMovement;
    public static event Action OnAttackStart, OnAttackStop, OnMovementStop;

    private void Awake()
    {
        _camera = Camera.main;
    }
    private void OnEnable()
    {

        _movement.action.Enable();
        _attack.action.Enable();
        _aiming.action.Enable();

        _movement.action.performed += MovementInput;
        _movement.action.canceled += StopMovement;
        _attack.action.started += AttackStart;
        _attack.action.canceled += AttackStop;
        _aiming.action.performed += AimInput;
    }

    private void OnDisable()
    {
        _movement.action.performed -= MovementInput;
        _movement.action.canceled -= StopMovement;
        _attack.action.started -= AttackStart;
        _attack.action.canceled -= AttackStop;
        _aiming.action.performed -= AimInput;
    }

    private void MovementInput(InputAction.CallbackContext context)
    {
        var movement = context.ReadValue<Vector2>();
        OnMovementInput?.Invoke(movement);
    }

    private void StopMovement(InputAction.CallbackContext context)
    {
        OnMovementStop?.Invoke();
    }

    private void AttackStart(InputAction.CallbackContext context)
    {
        OnAttackStart?.Invoke();
    }

    private void AttackStop(InputAction.CallbackContext context)
    {
        OnAttackStop?.Invoke();
    }

    private void AimInput(InputAction.CallbackContext context)
    {
        var aiming = context.ReadValue<Vector2>();
        //if using controller
        if (false)
            OnAimInput?.Invoke(aiming);
        //if using mouse
        else if (true)
            OnMouseMovement?.Invoke(_camera.ScreenToWorldPoint(aiming));
    }

    
}