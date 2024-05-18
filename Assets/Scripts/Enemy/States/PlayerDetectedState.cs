using Lance.CoreSystem;
using Lance;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class PlayerDetectedState : State
{

    protected Movement Movement
    {
        get => movement ?? core.GetCoreComponent(ref movement);
    }

    private Movement movement;

    private CollisionSenses CollisionSenses
    {
        get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses);
    }

    private CollisionSenses collisionSenses;

    protected D_PlayerDetected stateData;
    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;
    protected bool performLongRangeAction;
    protected bool performCloseRangeAction;
    protected bool isDetectingLedge;

    public PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBool, D_PlayerDetected stateData) : base(entity, stateMachine, animBool)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        
            isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
            isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
            isDetectingLedge = CollisionSenses.LedgeVertical;

            performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
        
        
    }

    public override void Enter()
    {
        base.Enter();
        Movement?.SetVelocityX(0f);
        performLongRangeAction = false;

      
    }

    public override void Exit()
    {
        base.Exit();
    }
        
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + stateData.longRangeActionTime)
        {
            performLongRangeAction = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
  
    }

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}
