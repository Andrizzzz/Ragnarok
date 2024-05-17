using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeState : State
{
    protected D_ChargeState stateData;
    protected bool isPlayerInMinAgroRange;
    protected bool isDetectingLedge;
    protected bool isDetectingWall;
    protected bool isChargeTimeOver;
    protected bool performCloseRangeAction;

    public ChargeState(Entity entity, FiniteStateMachine stateMachine, string animBool, D_ChargeState stateData)
        : base(entity, stateMachine, animBool)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        isChargeTimeOver = false;
        core.Movement.SetVelocityX(stateData.chargeSpeed * core.Movement.FacingDirection);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time >= startTime + stateData.chargeTime)
        {
            isChargeTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isDetectingLedge = core.CollisionSenses.LedgeVertical;
        isDetectingWall = core.CollisionSenses.WallFront;
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();

        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
    }
}
