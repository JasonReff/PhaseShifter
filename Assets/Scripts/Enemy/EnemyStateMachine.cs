using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private EnemyMovement _movement;
    [SerializeField] protected float _maxAttackRange, _minFollowDistance;
    private EnemyState _initialState;
    private EnemyState _enemyState;
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

    protected virtual void Start()
    {
        _initialState = new EnemyFollowPlayerState(this, CharacterManager.Instance.Player);
        ChangeState(_initialState);
    }

    private void Update()
    {
        if (_enemyState == null)
            return;
        _enemyState.UpdateState();
    }

    public void ChangeState(EnemyState newState)
    {
        if (_enemyState == newState)
            return;
        if (_enemyState != null)
        {
            _enemyState.EndState();
        }
        _enemyState = newState;
        _enemyState.PrepareState();
    }

    private void OnPlayerDeath()
    {
        _enemyState = null;
    }

    private void OnDeath()
    {
        _enemyState = null;
    }
}
