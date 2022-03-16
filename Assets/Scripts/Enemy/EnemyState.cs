using UnityEngine;

public class EnemyState
{
    protected EnemyStateMachine _enemyStateMachine;
    protected float _distanceToPlayer;
    protected Vector2 _directionToPlayer = new Vector2();
    protected GameObject _player;

    public EnemyState(EnemyStateMachine stateMachine, GameObject player)
    {
        _enemyStateMachine = stateMachine;
        _player = player;
    }

    public virtual void PrepareState()
    {

    }

    public virtual void UpdateState()
    {
        GetRelativePlayerPosition();
    }

    public virtual void EndState()
    {

    }

    private void GetRelativePlayerPosition()
    {
        _directionToPlayer = (_player.transform.position - _enemyStateMachine.transform.position).normalized;
        _distanceToPlayer = (_player.transform.position - _enemyStateMachine.transform.position).magnitude;
    }
}