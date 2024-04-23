using System;
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
        knockbackDuration,
        touchDamage,
        lastTouchDamageTime,
        touchDamageCooldown,
        touchDamageWidth,
        touchDamageHeight;

    [SerializeField]
    private Transform
        groundCheck,
        wallCheck,
        touchDamageCheck;

    [SerializeField]
    private LayerMask 
        whatIsGround,
        whatIsPlayer;

    [SerializeField]
    private Vector2 
        knockbackSpeed,
        touchDamageBotLeft,
        touchDamageTopRight;

    [SerializeField]
    private GameObject
        hitParticle,
        deathChunkParticle,
        deathBloodParticle;

    private float[] attackDetails = new float[2];

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

    private GameObject Worm;
    private Rigidbody2D WormRb;
    private Animator WormAnim;

    private void Start()
    {
        Worm = transform.Find("Worm").gameObject;
        WormRb = Worm.GetComponent<Rigidbody2D>();
        WormAnim = Worm.GetComponent<Animator>();

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

        Worm.transform.Rotate(0.0f, 180.0f, 0.0f);
    }


    private void EnterMovingState()
    {

    }

    private void UpdateMovingState()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

        wallDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);

        CheckTouchDamage();

        if (!groundDetected || wallDetected)
        {
            Flip();
        }
        else
        {
            movement.Set(movementSpeed * facingDirection, WormRb.velocity.y); // Modify this line
            WormRb.velocity = movement;
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
        WormRb.velocity = movement;
        WormAnim.SetBool("Knockback", true);
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
        WormAnim.SetBool("Knockback", false);
    }
    //--Dead State-----------------------------------------------------------------------------------
    private void EnterDeadState()
    {
        Instantiate(deathChunkParticle, Worm.transform.position, deathChunkParticle.transform.rotation);
        Instantiate(deathBloodParticle, Worm.transform.position, deathBloodParticle.transform.rotation);
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

        Instantiate(hitParticle, Worm.transform.position, Quaternion.Euler(0.0f, 0.0f, UnityEngine.Random.Range(0.0f, 360.0f)));


        if (attackDetails[1] > Worm.transform.position.x)
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

    private void CheckTouchDamage()
    {
        if(Time.time >= lastTouchDamageTime + touchDamageCooldown)
        {
            touchDamageBotLeft.Set(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
            touchDamageTopRight.Set(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));


            Collider2D hit = Physics2D.OverlapArea(touchDamageBotLeft, touchDamageTopRight, whatIsPlayer);

            if(hit != null)
            {
                lastTouchDamageTime = Time.time;
                attackDetails[0] = touchDamage;
                attackDetails[1] = Worm.transform.position.x;
                hit.SendMessage("Damage", attackDetails);
            }

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

        Vector2 botLeft = new Vector2(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
        Vector2 botRight = new Vector2(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
        Vector2 topRight = new Vector2(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));
        Vector2 topLeft = new Vector2(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));

        Gizmos.DrawLine(botLeft, botRight);
        Gizmos.DrawLine(botRight, topRight);
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topLeft, botLeft);

    }

}
