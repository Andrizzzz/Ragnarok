using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lance
{
    public class E4_MoveState : MoveState
    {
        Enemy4 enemy;
        public E4_MoveState(Entity entity, FiniteStateMachine stateMachine, string animBool, D_MoveState stateData, Enemy4 enemy)
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
                stateMachine.ChangeState(enemy.playerDetectedState);
            }

            else if (isDetectingWall || !isDetectingLedge)
            {
                enemy.idleState.SetFlipAfterIdle(true);
                stateMachine.ChangeState(enemy.idleState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}
