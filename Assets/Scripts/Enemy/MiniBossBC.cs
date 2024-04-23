using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
public class MiniBossBC : MonoBehaviour
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
    [SerializeField]
    private GameObject
        hitParticle,
        deathChunkParticle,
        deathBloodParticle;

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

    private GameObject MiniBossSlime;
    private Rigidbody2D MiniBossSlimeRb;
    private Animator MiniBossSlimeAnim;

    private void Start()
    {
        MiniBossSlime = transform.Find("MiniBossSlime").gameObject;
      
        if (MiniBossSlime != null)
        {
            MiniBossSlimeRb = MiniBossSlime.GetComponent<Rigidbody2D>();
            MiniBossSlimeAnim = MiniBossSlime.GetComponent<Animator>();

            currentHealth = maxHealth;
            facingDirection = 1;
        }
        else
        {
            UnityEngine.Debug.LogError("MiniBossSlime GameObject not found!");
        }

        MiniBossSlimeRb = MiniBossSlime.GetComponent<Rigidbody2D>();
        MiniBossSlimeAnim = MiniBossSlime.GetComponent<Animator>();

        currentHealth = maxHealth;
        facingDirection = 1;
    }


    private void Update()
    {
        switch (currentState)
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
        facingDirection *= -1;

        MiniBossSlime.transform.Rotate(0.0f, 180.0f, 0.0f);
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
            movement.Set(movementSpeed * facingDirection, MiniBossSlimeRb.velocity.y); // Modify this line
            MiniBossSlimeRb.velocity = movement;
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
        MiniBossSlimeRb.velocity = movement;
        MiniBossSlimeAnim.SetBool("Knockback", true);
    }


    private void UpdateKnockbackState()
    {
        if (Time.time >= KnockbackStartTime + knockbackDuration)
        {
            SwitchState(State.Moving);
        }
    }

    private void ExitKnockbackState()
    {
        MiniBossSlimeAnim.SetBool("Knockback", false);
    }
    //--Dead State-----------------------------------------------------------------------------------
    private void EnterDeadState()
    {
        Instantiate(deathChunkParticle, MiniBossSlime.transform.position, deathChunkParticle.transform.rotation);
        Instantiate(deathBloodParticle, MiniBossSlime.transform.position, deathBloodParticle.transform.rotation);
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

        Instantiate(hitParticle, MiniBossSlime.transform.position, Quaternion.Euler(0.0f, 0.0f, UnityEngine.Random.Range(0.0f, 360.0f)));


        if (attackDetails[1] > MiniBossSlime.transform.position.x)
        {
            damageDirection = -1;

        }

        else
        {
            damageDirection = 1;
        }

        //HitParticle

        if (currentHealth > 0.0f)
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
        switch (currentState)
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
