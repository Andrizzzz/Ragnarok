using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lance
{
    public class E4_IdleState : IdleState
    {
        Enemy4 enemy;
        public E4_IdleState(Entity entity, FiniteStateMachine stateMachine, string animBool, D_IdleState stateData, Enemy4 enemy) 
            : base(entity, stateMachine, animBool, stateData)
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
                stateMachine.ChangeState(enemy.playerDetectedState); // Use enemy.playerDetectedState
            }
            else if (isIdleTimeOver)
            {
                stateMachine.ChangeState(enemy.moveState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}
