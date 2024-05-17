using Lance.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Entity : MonoBehaviour
{
    public FiniteStateMachine stateMachine;
    public D_Entity entityData;

    public Animator anim { get; private set; }
    public AnimationToStateMachine atsm {get; private set;}
    public int lastDamageDirection { get; private set; }
    public Core Core { get; private set; }

    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Transform ledgeCheck;
    [SerializeField]
    private Transform playerCheck;
    [SerializeField]
    private Transform groundCheck;


    private float currentHealth;
    private float currentStunResistance;
    private float lastDamageTime;


    private Vector2 velocityWorksSpace;

    protected bool isStunned;
    protected bool isDead;

    public virtual void Awake()
    {
        Core = GetComponentInChildren<Core>();

        currentHealth = entityData.maxHealth;
        currentStunResistance = entityData.stunResistance;
        anim = GetComponent<Animator>();

        stateMachine = new FiniteStateMachine();
        atsm = GetComponent<AnimationToStateMachine>();

    }

    public virtual void Update()
    {

        Core.LogicUpdate();
        stateMachine.currentState.LogicUpdate();

        anim.SetFloat("yVelocity", Core.Movement.RB.velocity.y);


        if (Time.time >= lastDamageTime + entityData.stunRecoveryTime)
        {
            ResetStunResistance();
        }
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }


    public virtual bool CheckPlayerInMinAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, entityData.minAgroDistance, entityData.WhatIsPlayer);
    }



    public virtual bool CheckPlayerInMaxAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, entityData.maxAgroDistance, entityData.WhatIsPlayer);

    }

    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, entityData.closeRangeActionDistance, entityData.WhatIsPlayer);
    }

    public virtual void DamageHop(float velocity)
    {
      velocityWorksSpace.Set(Core.Movement.RB.velocity.x, velocity);
       Core.Movement.RB.velocity = velocityWorksSpace;
    }

    public virtual void ResetStunResistance()
    {
        isStunned = false;
        currentStunResistance = entityData.stunResistance;
    }

    public virtual void Damage(AttackDetails attackDetails)
    {
        lastDamageTime = Time.time;
        currentStunResistance -= attackDetails.stunDamageAmount;

        currentHealth -= attackDetails.damageAmount;

        DamageHop(entityData.damageHopSpeed);

        Instantiate(entityData.hitParticle, transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f,360f)));

        if(attackDetails.position.x > transform.position.x)
        {
            lastDamageDirection = -1;
        }

        else
        {
            lastDamageDirection = 1;
        }

        if(currentStunResistance <= 0)
        {
            isStunned = true;
        }
        if(currentHealth <= 0)
        {
            isDead = true;
        }
    }

     
    public virtual void OnDrawGizmos()
    {
        if(Core != null)
        {
            Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * Core.Movement.FacingDirection * entityData.wallCheckDistance));
            Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));

            Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.closeRangeActionDistance), 0.2f);
            Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.minAgroDistance), 0.2f);
            Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.maxAgroDistance), 0.2f);
        }
    }
}
