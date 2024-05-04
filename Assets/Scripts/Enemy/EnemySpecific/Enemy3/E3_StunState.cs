using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_StunState : StunState
{
    Enemy3 enemy;
   
    public E3_StunState(Entity entity, FiniteStateMachine stateMachine, string animBool, D_StunState stateData, Enemy3 enemy ) : base(entity, stateMachine, animBool, stateData)
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

        if (isStunTimeOver)
        {
            if (isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(enemy.playerDetectedState);
            }
            else
            {
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

