using UnityEngine;

public class PinwheelStateMachine : EnemyStateMachine
{
    [SerializeField] private EnemyAttack _redAttack, _blueAttack;
    [SerializeField] private EnemyPhaseController _phaseController;
    [SerializeField] private Animator _animator;
    [SerializeField] private RuntimeAnimatorController _redController, _blueController;
    [SerializeField] private int _transformationHealthThreshold;
    [SerializeField] private float _blueRange, _blueFollowDistance;

    protected override void OnEnable()
    {
        base.OnEnable();
        GetComponent<EnemyHealth>().OnHealthChanged += OnHealthChanged;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        GetComponent<EnemyHealth>().OnHealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(int health)
    {
        if (health == _transformationHealthThreshold)
        {
            _currentAttack = _blueAttack;
            _animator.runtimeAnimatorController = _blueController;
            _animator.SetTrigger("Transform");
            _phaseController.VulnerablePhase = Phase.Blue;
            _maxAttackRange = _blueRange;
            _minFollowDistance = _blueFollowDistance;
        }
    }

}
