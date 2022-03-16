using UnityEngine;

public class EnemyFollowPlayerState : EnemyState
{

    public EnemyFollowPlayerState(EnemyStateMachine stateMachine, GameObject player) : base(stateMachine, player)
    {
        
    }


    public override void UpdateState()
    {
        base.UpdateState();
        
        if (_distanceToPlayer < _enemyStateMachine.MaxAttackRange)
        {
            _enemyStateMachine.ChangeState(new EnemyAttackPlayerState(_enemyStateMachine, _player));
            return;
        }
        _enemyStateMachine.Movement.MoveInDirection(_directionToPlayer);
    }

    
}
