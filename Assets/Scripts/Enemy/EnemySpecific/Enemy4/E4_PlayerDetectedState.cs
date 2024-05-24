using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lance
{
    public class E4_PlayerDetectedState : PlayerDetectedState
    {
        Enemy4 enemy;
        public E4_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBool, D_PlayerDetected stateData, Enemy4 enemy)
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

            if (performCloseRangeAction)
            {
                stateMachine.ChangeState(enemy.meleeAttackState);
            }

            else if (performLongRangeAction)
            {
                stateMachine.ChangeState(enemy.chargeState);
            }

            else if (!isPlayerInMaxAgroRange)
            {
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }

            else if (!isDetectingLedge)
            {
                Movement?.Flip();
                stateMachine.ChangeState(enemy.moveState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}
