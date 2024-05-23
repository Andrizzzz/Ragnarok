using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lance
{
    public class EnemyBoss1 : Entity
    {

        public EB1_IdleState idleState { get; private set; }
        public EB1_MoveState moveState { get; private set; }


        [SerializeField]
        private D_IdleState idleStateData;
        [SerializeField]
        private D_MoveState moveStateData;



        public override void Awake()
        {
            base.Awake();


            moveState = new EB1_MoveState(this, stateMachine, "move", moveStateData, this);
            idleState = new EB1_IdleState(this, stateMachine, "idle", idleStateData, this);


        }

        private void Start()
        {
            stateMachine.Initialize(moveState);

        }

        public override void Damage(AttackDetails attackDetails)
        {
            base.Damage(attackDetails);
        }

        public override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
        }
    }
}
