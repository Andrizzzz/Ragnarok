using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_DodgeState : DodgeState
{

    Enemy2 enemy;
    public E2_DodgeState(Entity entity, FiniteStateMachine stateMachine, string animBool, D_DodgeState stateData, Enemy2 enemy) : base(entity, stateMachine, animBool, stateData)
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

        if (isDodgeOver)
        {
            if (isPlayerInMaxAgroRange && performCloseRangeAction)
            {
                stateMachine.ChangeState(enemy.meleeAttackState);
            }

            else if (isPlayerInMaxAgroRange && !performCloseRangeAction)
            {
                stateMachine.ChangeState(enemy.lookForPlayerState);
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
