using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour
{
    private enum State
    {
        Moving,
        Knockback,
        Dead
    }
    private State currentState;

    [SerializeField]
    private float
        groundCheckDistance,
        wallCheckDistance,
        movementSpeed,
        maxHealth,
        knockbackDuration;

    [SerializeField]
    private Transform
        groundCheck,
        wallCheck;

    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private Vector2 knockbackSpeed;

    private float 
        currentHealth,
        KnockbackStartTime;

    private int 
        facingDirection,
        damageDirection;

    private Vector2 movement;


    private bool
        groundDetected,
        wallDetected;
        
    private GameObject alive;
    private Rigidbody2D aliveRb;
    private Animator aliveAnim;

    private void Start()
    {
        alive = transform.Find("Alive").gameObject;
        aliveRb = alive.GetComponent<Rigidbody2D>();
        aliveAnim = GetComponent<Animator>();


        facingDirection = 1;
    }


    private void Update()
    {
        switch(currentState)
        {
            case State.Moving:
                UpdateMovingState();
                break;
                case State.Knockback:
                UpdateKnockbackState();
                break;
                case State.Dead:
                UpdateDeadState();
                break;

        }
    }


    //--Walking state---------------------------------------------------------------------------
    private void Flip()
    {
        facingDirection *= -1; // Change from facingDirection *= 1 to facingDirection *= -1
        Vector3 scale = alive.transform.localScale;
        scale.x *= -1; // Flip the scale instead of rotating
        alive.transform.localScale = scale;
    }


    private void EnterMovingState()
    {

    }

    private void UpdateMovingState()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

        wallDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);

        if (!groundDetected || wallDetected)
        {
            Flip();
        }
        else
        {
            movement.Set(movementSpeed * facingDirection, aliveRb.velocity.y); // Modify this line
            aliveRb.velocity = movement;
        }
    }



    private void ExitMovingState()
    {

    }

   //--Knockback state-----------------------------------------------------------------------------

    private void EnterKnockbackState() 
    {
        KnockbackStartTime = Time.time;
        movement.Set(knockbackSpeed.x * damageDirection, knockbackSpeed.y);
        aliveRb.velocity = movement;
        aliveAnim.SetBool("Knockback", true);
    }

    private void UpdateKnockbackState()
    {
        if(Time.time >= KnockbackStartTime + knockbackDuration)
        {
            SwitchState(State.Moving);
        }
    }

    private void ExitKnockbackState() 
    {
        aliveAnim.SetBool("Knockback", false);
    }
    //--Dead State-----------------------------------------------------------------------------------
    private void EnterDeadState()
    {
        //spawnchunks
        Destroy(gameObject);
    }

    private void UpdateDeadState()
    {

    }

    private void ExitDeadState()
    {

    }

    //OTHER FUNCTION--------------------------------------------------------------------------

    private void Damage(float[] attackDetails)
    {
        currentHealth -= attackDetails[0];

        if (attackDetails[1] > alive.transform.position.x) 
        {
            damageDirection = -1;

        }

        else
        {
            damageDirection = 1;
        }

        //HitParticle

        if(currentHealth > 0.0f)
        {
            SwitchState(State.Knockback);
        }
        
        else if (currentHealth <= 0.0f)
        {
            SwitchState(State.Dead);
        }
    }

    private void SwitchState(State state)
    {
        switch(currentState)
        {
            case State.Moving:
                ExitMovingState();
                break;
                case State.Knockback:
                ExitKnockbackState();
                break;
                case State.Dead:
                ExitDeadState();
                break;
        }
                
        switch (state)
        {
            case State.Moving:
                EnterMovingState();
                break;
            case State.Knockback:
                EnterKnockbackState();
                break;
            case State.Dead:
                EnterDeadState();
                break;
        }

        currentState = state; 
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));

        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }
        
}