using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lance
{
    public class EB1_PlayerDetectedState : PlayerDetectedState
    {
        EnemyBoss1 enemy;
        public EB1_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBool, D_PlayerDetected stateData, EnemyBoss1 enemy) 
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
            entity.ResetStunResistance();

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
