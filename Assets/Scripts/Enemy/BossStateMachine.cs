using System;
using UnityEngine;

public class BossStateMachine : EnemyStateMachine
{
    public static Action OnBossSpawned;

    private void Awake()
    {
        OnBossSpawned?.Invoke();
    }
}

public class SummonerBossStateMachine : BossStateMachine
{

}

public class SummonerBossSummonEnemyState : SummonerBossState
{
    public SummonerBossSummonEnemyState(EnemyStateMachine stateMachine, GameObject player) : base(stateMachine, player)
    {

    }


}