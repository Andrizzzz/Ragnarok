using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lance
{
    public class EB1_IdleState : IdleState
    {
        EnemyBoss1 enemy;
        public EB1_IdleState(Entity entity, FiniteStateMachine stateMachine, string animBool, D_IdleState stateData, EnemyBoss1 enemy) 
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

              if (isIdleTimeOver)
            {
                Debug.Log("Idle na sya");

                stateMachine.ChangeState(enemy.moveState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}
