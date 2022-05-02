using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private InputActionReference _movement, _attack, _stickAiming, _mouseAiming;
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
        _stickAiming.action.Enable();
        _mouseAiming.action.Enable();

        _movement.action.performed += MovementInput;
        _movement.action.canceled += StopMovement;
        _attack.action.started += AttackStart;
        _attack.action.canceled += AttackStop;
        _stickAiming.action.performed += StickAim;
        _mouseAiming.action.performed += MouseAim;
    }

    private void OnDisable()
    {
        _movement.action.performed -= MovementInput;
        _movement.action.canceled -= StopMovement;
        _attack.action.started -= AttackStart;
        _attack.action.canceled -= AttackStop;
        _stickAiming.action.performed -= StickAim;
        _mouseAiming.action.performed -= MouseAim;
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

    private void MouseAim(InputAction.CallbackContext context)
    {

        var aiming = context.ReadValue<Vector2>();
        OnMouseMovement?.Invoke(_camera.ScreenToWorldPoint(aiming));
    }

    private void StickAim(InputAction.CallbackContext context)
    {
        var aiming = context.ReadValue<Vector2>();
        OnAimInput?.Invoke(aiming);
    }
}