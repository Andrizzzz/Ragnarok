using Lance.CoreSystem;
using Lance;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
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

    protected D_MoveState stateData;
    protected bool isDetectingWall;
    protected bool isDetectingLedge;
    protected bool isPlayerInMinAgroRange;

    public MoveState(Entity entity, FiniteStateMachine stateMachine, string animBool, D_MoveState stateData) : base(entity, stateMachine, animBool)
    {
       this.stateData = stateData;
    }


    public override void DoChecks()
    {
        base.DoChecks();
        
            isDetectingLedge = CollisionSenses.LedgeVertical;
            isDetectingWall = CollisionSenses.WallFront;
            isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        
      
    }

    public override void Enter()
    {
        base.Enter();
        Movement?.SetVelocityX(stateData.MovementSpeed * Movement.FacingDirection);

       
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
