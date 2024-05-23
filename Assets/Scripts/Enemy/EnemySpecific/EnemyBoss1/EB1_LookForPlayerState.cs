using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lance
{
    public class EB1_LookForPlayerState : LookForPlayerState
    {

        EnemyBoss1 enemy;

        public EB1_LookForPlayerState(Entity entity, FiniteStateMachine stateMachine, string animBool, D_LookForPlayerState stateData, EnemyBoss1 enemy)
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
            else if (isAllTurnsTimeDone)
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
