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
    public GameObject SlimeGO { get; private set; }

    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Transform ledgeCheck;
    [SerializeField]
    private Transform playerCheck;

    private Vector2 velocityWorksSpace;

    public virtual void Start()
    {
        facingDirection = 1;


        SlimeGO = transform.Find("Slime").gameObject;
        rb = SlimeGO.GetComponent<Rigidbody2D>();
        anim = SlimeGO.GetComponent<Animator>();

        stateMachine = new FiniteStateMachine();

    }

    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();
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

    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, SlimeGO.transform.right, entityData.wallCheckDistance, entityData.WhatIsGround);
    }

    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.WhatIsGround);
    }

    public virtual bool CheckPlayerInMinAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, SlimeGO.transform.right, entityData.minAgroDistance, entityData.WhatIsPlayer);
    }



    public virtual bool CheckPlayerInMaxAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, SlimeGO.transform.right, entityData.maxAgroDistance, entityData.WhatIsPlayer);

    }


    public virtual void Flip()
    {
        facingDirection *= -1;
        SlimeGO.transform.Rotate(0f, 180f, 0f);
    }

    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.wallCheckDistance));
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3) (Vector2.down * entityData.ledgeCheckDistance));
    }
}
