using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public FiniteStateMachine stateMachine;
    public D_Entity entityData;

    public int facingDirection { get; private set; }

    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }
    public GameObject alive { get; private set; } 
    public AnimationToStateMachine atsm {get; private set;}
    public int lastDamageDirection { get; private set; }

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

    public virtual void Start()
    {
        facingDirection = 1;
        currentHealth = entityData.maxHealth;
        currentStunResistance = entityData.stunResistance;

        alive = transform.Find("Alive").gameObject;
        rb = alive.GetComponent<Rigidbody2D>();
        anim = alive.GetComponent<Animator>();

        stateMachine = new FiniteStateMachine();
        atsm = alive.GetComponent<AnimationToStateMachine>();

    }

    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();

        if (Time.time >= lastDamageTime + entityData.stunRecoveryTime)
        {
            ResetStunResistance();
        }
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    public virtual void SetVelocity(float velocity)
    {
        velocityWorksSpace.Set(facingDirection * velocity, rb.velocity.y);
        rb.velocity = velocityWorksSpace;
    }

    public virtual void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        velocityWorksSpace.Set(angle.x * velocity * direction, angle.y * velocity);

        rb.velocity = velocityWorksSpace;
    }

    public virtual bool CheckGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, entityData.groundCheckRadius, entityData.WhatIsGround);
    } 
        
    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, alive.transform.right, entityData.wallCheckDistance, entityData.WhatIsGround);
    }
    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.WhatIsGround);
    }

    public virtual bool CheckPlayerInMinAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, alive.transform.right, entityData.minAgroDistance, entityData.WhatIsPlayer);
    }



    public virtual bool CheckPlayerInMaxAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, alive.transform.right, entityData.maxAgroDistance, entityData.WhatIsPlayer);

    }

    public virtual void DamageHop(float velocity)
    {
      velocityWorksSpace.Set(rb.velocity.x, velocity);
       rb.velocity = velocityWorksSpace;
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

        Instantiate(entityData.hitParticle, alive.transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f,360f)));

        if(attackDetails.position.x > alive.transform.position.x)
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

    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.Raycast(playerCheck.position, alive.transform.right, entityData.closeRangeActionDistance, entityData.WhatIsPlayer);
    }
    public virtual void Flip()
    {
        facingDirection *= -1;  
        alive.transform.Rotate(0f, 180f, 0f);
    }
    

    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.wallCheckDistance));
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3) (Vector2.down * entityData.ledgeCheckDistance));

        Gizmos.DrawWireSphere(playerCheck.position +(Vector3)(Vector2.right * entityData.closeRangeActionDistance), 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position +(Vector3)(Vector2.right * entityData.minAgroDistance), 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position +(Vector3)(Vector2.right * entityData.maxAgroDistance), 0.2f);

    }
}
