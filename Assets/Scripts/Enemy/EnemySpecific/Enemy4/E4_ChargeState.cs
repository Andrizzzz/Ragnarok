using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lance
{
    public class E4_ChargeState : ChargeState
    {
        Enemy4 enemy;
        public E4_ChargeState(Entity entity, FiniteStateMachine stateMachine, string animBool, D_ChargeState stateData, Enemy4 enemy) : base(entity, stateMachine, animBool, stateData)
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

            else if (!isDetectingLedge || isDetectingWall)
            {
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }

            else if (isChargeTimeOver)
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
}