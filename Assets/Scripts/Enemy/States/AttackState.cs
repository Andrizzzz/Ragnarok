using Lance.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    private Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;

    protected Transform attackPosition;
    protected bool isAnimationFinished;
    protected bool isPlayerInMinAgroRange;


    public AttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition)
    : base(entity, stateMachine, animBoolName)
    {
        this.attackPosition = attackPosition;
    }

    public override void Enter()
    {
        base.Enter();
         isAnimationFinished = false;
         entity.atsm.attackState = this;

         Movement?.SetVelocityX(0f);
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

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();

  
    }

    public virtual void TriggerAttack()
    {
        
    }

    public virtual void FinishAttack()
    {
        isAnimationFinished = true;
    }

}
