    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    protected FiniteStateMachine stateMachine;
    protected Entity entity;

    protected string animBoolName;

    public float startTime { get; protected set; }


    public State(Entity entity, FiniteStateMachine stateMachine, string animBool)
    {
        this.entity = entity;
        this.stateMachine = stateMachine;
        this.animBoolName = animBool;
    }

    public virtual void Enter()
    {
        startTime = Time.time;
        entity.anim.SetBool(animBoolName, true);

        DoChecks();
    }


    public virtual void Exit() 
    {

        entity.anim.SetBool(animBoolName, false);

    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks()
    {

    } 
}
