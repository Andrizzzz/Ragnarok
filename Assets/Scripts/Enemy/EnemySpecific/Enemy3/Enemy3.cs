using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : Entity
{
    public E3_IdleState idlestate {  get; private set; }
    public E3_MoveState movestate {  get; private set; }
    public E3_LookForPlayer lookForPlayerState { get; private set; }
    public E3_PlayerDetectedState playerDetectedState { get; private set; }
    public E3_RangedAttackState rangedAttackState { get; private set; }
    public E3_StunState stunState { get; private set; }
    public E3_MeleeAttackState meleeAttackState { get; private set; }
    public E3_DeadState deadState { get; private set; }


    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_LookForPlayerState lookForPlayerStateData;
    [SerializeField]
    private D_PlayerDetected playerDetectedStateData;
    [SerializeField]
    private D_RangedAttackState rangedAttackStateData;
    [SerializeField]
    private D_StunState stunStateData;
    [SerializeField]
    private D_MeleeAttack meleeAttackStateData;
    [SerializeField]
    private D_DeadState deadStateData;

    [SerializeField]
    private Transform meleeAttackPosition;  
    [SerializeField]
    private Transform rangedAttackPosition;

    public override void Start()
    {
        base.Start();

        movestate = new E3_MoveState(this, stateMachine, "move", moveStateData, this);
        idlestate = new E3_IdleState(this, stateMachine, "idle", idleStateData, this);
        lookForPlayerState = new E3_LookForPlayer(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        playerDetectedState = new E3_PlayerDetectedState(this, stateMachine,"playerDetectedState", playerDetectedStateData, this);
        rangedAttackState = new E3_RangedAttackState(this, stateMachine, "rangedAttack", rangedAttackPosition, rangedAttackStateData, this);
        meleeAttackState = new E3_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        stunState = new E3_StunState(this, stateMachine, "stun", stunStateData, this);
        deadState = new E3_DeadState(this, stateMachine, "dead", deadStateData, this);

        stateMachine.Initialize(movestate);

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
        else if (CheckPlayerInMinAgroRange())
        {
            stateMachine.ChangeState(rangedAttackState);
        }
        else if (!CheckPlayerInMinAgroRange())
        {
            lookForPlayerState.SetTurnImmediately(true);
            stateMachine.ChangeState(lookForPlayerState);
        }
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }
}
