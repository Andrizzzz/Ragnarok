using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lance
{
    public class EB1_MoveState : MoveState
    {
        private EnemyBoss1 enemy;

        public EB1_MoveState(Entity entity, FiniteStateMachine stateMachine, string animBool, D_MoveState stateData, EnemyBoss1 enemy)
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

             if (isDetectingWall || !isDetectingLedge)
            {
                Debug.Log("Gumalaw na sya");
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
