using UnityEngine;

public class EnemyAttackPlayerState : EnemyState
{
    private float _followDistance;
    private EnemyAttack _attack;
    public EnemyAttackPlayerState(EnemyStateMachine stateMachine, GameObject player) : base(stateMachine, player)
    {
        
    }

    public override void PrepareState()
    {
        base.PrepareState();
        _attack = _enemyStateMachine.CurrentAttack;
        _followDistance = _enemyStateMachine.MinFollowDistance;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (_distanceToPlayer > _followDistance)
        {
            _enemyStateMachine.ChangeState(new EnemyFollowPlayerState(_enemyStateMachine, _player));
            return;
        }
        else
        {
            _attack.DirectionToPlayer = _directionToPlayer;
            _attack.TryAttack();
        }
    }
}