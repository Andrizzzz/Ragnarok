using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lance
{
    public class EnemyBoss1 : Entity
    {

        public EB1_IdleState idleState { get; private set; }
        public EB1_MoveState moveState { get; private set; }
        public EB1_PlayerDetectedState playerDetectedState { get; private set; }
        public EB1_LookForPlayerState lookForPlayerState { get; private set; }

        public EB1_MeleeAttackState meleeAttackState { get; private set; }
        public EB1_StunState stunState { get; private set; }
        public EB1_DeadState deadState { get; private set; }
        public EB1_ChargeState chargeState { get; private set; }


        [SerializeField]
        private D_MeleeAttack meleeAttackStateData;
        [SerializeField]
        private Transform meleeAttackPosition;
        [SerializeField]
        private D_StunState stunStateData;
        [SerializeField]
        private D_DeadState deadStateData;
        [SerializeField]
        private D_ChargeState chargeStateData;
        [SerializeField]
        private D_IdleState idleStateData;
        [SerializeField]
        private D_MoveState moveStateData;
        [SerializeField]
        private D_PlayerDetected playerDetectedData;
        [SerializeField]
        private D_LookForPlayerState lookForPlayerStateData;
        [SerializeField]
      




        public override void Awake()
        {
            base.Awake();


            moveState = new EB1_MoveState(this, stateMachine, "move", moveStateData, this);
            idleState = new EB1_IdleState(this, stateMachine, "idle", idleStateData, this);
            playerDetectedState = new EB1_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedData, this);
            lookForPlayerState = new EB1_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
            meleeAttackState = new EB1_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
            stunState = new EB1_StunState(this, stateMachine, "stun", stunStateData, this);
            deadState = new EB1_DeadState(this, stateMachine, "dead", deadStateData, this);
            chargeState = new EB1_ChargeState(this, stateMachine, "charge", chargeStateData, this);





        }

        private void Start()
        {
            stateMachine.Initialize(moveState);

        }

        public override void Damage(AttackDetails attackDetails)
        {
            base.Damage(attackDetails);

            if (isDead)
            {
                stateMachine.ChangeState(deadState);
            }

            else if (isStunned && stateMachine.currentState != stunState)
            {
                stateMachine.ChangeState(stunState);
            }
        }

        public override void OnDrawGizmos()
        {
            base.OnDrawGizmos();

            Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);

        }
    }
}
