using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lance
{
    public class E4_StunState : StunState
    {
        Enemy4 enemy;
        public E4_StunState(Entity entity, FiniteStateMachine stateMachine, string animBool, D_StunState stateData, Enemy4 enemy)
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

            if (isStunTimeOver)
            {
                if (performCloseRangeAction)
                {
                    stateMachine.ChangeState(enemy.meleeAttackState);
                }
                else if (isPlayerInMinAgroRange)
                {
                    stateMachine.ChangeState(enemy.chargeState);
                }
                else
                {
                    enemy.lookForPlayerState.SetTurnImmediately(true);
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
