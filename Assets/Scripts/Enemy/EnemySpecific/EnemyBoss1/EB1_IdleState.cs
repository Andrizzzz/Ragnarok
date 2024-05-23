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


            if (isPlayerInMinAgroRange)
            {
                Debug.Log("Nadetect nya");
                stateMachine.ChangeState(enemy.playerDetectedState); // Use enemy.playerDetectedState
            }
            else if (isIdleTimeOver)
            {
                Debug.Log("Eh Nag Estedi");
                stateMachine.ChangeState(enemy.moveState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}
