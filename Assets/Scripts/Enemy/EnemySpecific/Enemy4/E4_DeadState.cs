using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lance
{
    public class E4_DeadState : DeadState
    {
        Enemy4 enemy;
        public E4_DeadState(Entity entity, FiniteStateMachine stateMachine, string animBool, D_DeadState stateData, Enemy4 enemy)
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
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}
