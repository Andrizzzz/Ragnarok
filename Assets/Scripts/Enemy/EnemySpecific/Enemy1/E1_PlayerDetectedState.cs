using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_PlayerDetectedState : PlayerDetectedState
{
    protected Enemy1 enemy;

    public E1_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBool, D_PlayerDetected stateData, Enemy1 enemy) : base(entity, stateMachine, animBool, stateData)
    {
        this.enemy = enemy;
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

        if (performLongRangeAction)
        {
            stateMachine.ChangeState(enemy.chargeState);
        }

        else if (!isPlayerInMaxAgroRange)
        {
            stateMachine.ChangeState(enemy.lookForPlayerState);
        }

        //TODO: Transition to Attack state

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
