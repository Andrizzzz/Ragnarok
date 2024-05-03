using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_LookForPlayer : LookForPlayerState
{

    Enemy3 enemy;
    public E3_LookForPlayer(Entity entity, FiniteStateMachine stateMachine, string animBool, D_LookForPlayerState stateData, Enemy3 enemy) : base(entity, stateMachine, animBool, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(enemy.playerDetectedState);
        }
        else if (isAllTurnsTimeDone)
        {
            stateMachine.ChangeState(enemy.movestate);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
