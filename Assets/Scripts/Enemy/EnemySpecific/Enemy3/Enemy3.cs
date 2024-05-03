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
    private Transform rangedAttackPosition;

    public override void Start()
    {
        base.Start();

        movestate = new E3_MoveState(this, stateMachine, "move", moveStateData, this);
        idlestate = new E3_IdleState(this, stateMachine, "idle", idleStateData, this);
        lookForPlayerState = new E3_LookForPlayer(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        playerDetectedState = new E3_PlayerDetectedState(this, stateMachine,"playerDetectedState", playerDetectedStateData, this);
        rangedAttackState = new E3_RangedAttackState(this, stateMachine, "rangedAttack", rangedAttackPosition, rangedAttackStateData, this);

        stateMachine.Initialize(movestate);

    }

    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);

         if (CheckPlayerInMinAgroRange())
        {
            stateMachine.ChangeState(rangedAttackState);
        }

        else if (!CheckPlayerInMinAgroRange())
        {
            lookForPlayerState.SetTurnImmediately(true);
            stateMachine.ChangeState(lookForPlayerState);
        }
    }
}
