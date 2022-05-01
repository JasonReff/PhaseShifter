using UnityEngine;

public class EnemyStateMachine : StateMachine<EnemyState>
{
    [SerializeField] private EnemyMovement _movement;
    [SerializeField] protected float _maxAttackRange, _minFollowDistance;
    [SerializeField] protected EnemyAttack _currentAttack;

    public EnemyMovement Movement { get => _movement; }
    public float MaxAttackRange { get => _maxAttackRange; }
    public float MinFollowDistance { get => _minFollowDistance; }
    public EnemyAttack CurrentAttack { get => _currentAttack; }

    protected virtual void OnEnable()
    {
        PlayerHealthController.OnPlayerDeath += OnPlayerDeath;
        GetComponent<EnemyHealth>().OnDeath += OnDeath;
    }

    protected virtual void OnDisable()
    {
        PlayerHealthController.OnPlayerDeath -= OnPlayerDeath;
        GetComponent<EnemyHealth>().OnDeath -= OnDeath;
    }

    protected override void Start()
    {
        _initialState = new EnemyFollowPlayerState(this, CharacterManager.Instance.Player);
        base.Start();
    }

    private void OnPlayerDeath()
    {
        _currentState = null;
    }

    private void OnDeath()
    {
        _currentState = null;
    }
}