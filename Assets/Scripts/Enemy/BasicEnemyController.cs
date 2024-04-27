using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
<<<<<<< HEAD
        lastTouchDamageTime,
        touchDamageCoolDown,
        touchDamage,
=======
        touchDamage,
        lastTouchDamageTime,
        touchDamageCooldown,
>>>>>>> f9594ac2c9a4404ac333ca3aa6d1904365082f39
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
<<<<<<< HEAD
=======

>>>>>>> f9594ac2c9a4404ac333ca3aa6d1904365082f39
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

<<<<<<< HEAD
    private float
        currentHealth,
        KnockbackStartTime;

    private float[] attackDetails = new float[2];
=======
    
    private float[] attackDetails = new float[2];

    private float
        currentHealth,
        knockbackStartTime;
>>>>>>> f9594ac2c9a4404ac333ca3aa6d1904365082f39

    private int
        facingDirection,
        damageDirection;

    private Vector2
        movement,
        touchDamageBotLeft,
        touchDamageTopRight;

    private bool
        groundDetected,
        wallDetected;

<<<<<<< HEAD
    private GameObject lavaworm; // Changed from "alive" to "lavaworm"
    private Rigidbody2D lavawormRb; // Changed from "aliveRb" to "lavawormRb"
    private Animator lavawormAnim; // Changed from "aliveAnim" to "lavawormAnim"

    private void Start()
    {
        lavaworm = transform.Find("Lavaworm").gameObject; // Changed from "Alive" to "Lavaworm"
        lavawormRb = lavaworm.GetComponent<Rigidbody2D>(); // Changed from "alive" to "lavaworm"
        lavawormAnim = GetComponent<Animator>();
=======
    private GameObject worm;
    private Rigidbody2D wormRb;
    private Animator wormAnim;

    private void Start()
    {
        worm = transform.Find("Worm").gameObject;
        wormRb = worm.GetComponent<Rigidbody2D>();
        wormAnim = worm.GetComponent<Animator>();
>>>>>>> f9594ac2c9a4404ac333ca3aa6d1904365082f39

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

    private void Flip()
    {
        facingDirection *= -1;
<<<<<<< HEAD

        lavaworm.transform.Rotate(0.0f, 180.0f, 0.0f); // Changed from "alive" to "lavaworm"
=======
        worm.transform.Rotate(0.0f, 180.0f, 0.0f);
>>>>>>> f9594ac2c9a4404ac333ca3aa6d1904365082f39
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
<<<<<<< HEAD
            movement.Set(movementSpeed * facingDirection, lavawormRb.velocity.y); // Changed from "aliveRb" to "lavawormRb"
            lavawormRb.velocity = movement; // Changed from "aliveRb" to "lavawormRb"
        }
    }

    private void CheckTouchDamage()
    {
        if (Time.time >= lastTouchDamageTime + touchDamageCoolDown)
        {
            touchDamageBotLeft.Set(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
            touchDamageTopRight.Set(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));

            Collider2D hit = Physics2D.OverlapArea(touchDamageBotLeft, touchDamageTopRight, whatIsPlayer);

            if (hit != null)
            {
                lastTouchDamageTime = Time.time;
                attackDetails[0] = touchDamage;
                attackDetails[1] = lavaworm.transform.position.x; // Changed from "alive" to "lavaworm"
                //hit.SendMessage("Damage", attackDetails);
            }

        }
    }

=======
            movement.Set(movementSpeed * facingDirection, wormRb.velocity.y);
            wormRb.velocity = movement;
        }
    }

>>>>>>> f9594ac2c9a4404ac333ca3aa6d1904365082f39
    private void ExitMovingState()
    {
    }

<<<<<<< HEAD
    //--Knockback state-----------------------------------------------------------------------------

=======
>>>>>>> f9594ac2c9a4404ac333ca3aa6d1904365082f39
    private void EnterKnockbackState()
    {
        knockbackStartTime = Time.time;
        movement.Set(knockbackSpeed.x * damageDirection, knockbackSpeed.y);
<<<<<<< HEAD
        lavawormRb.velocity = movement; // Changed from "aliveRb" to "lavawormRb"
        lavawormAnim.SetBool("Knockback", true); // Changed from "aliveAnim" to "lavawormAnim"
=======
        wormRb.velocity = movement;
        wormAnim.SetBool("Knockback", true);
>>>>>>> f9594ac2c9a4404ac333ca3aa6d1904365082f39
    }

    private void UpdateKnockbackState()
    {
<<<<<<< HEAD
        if (Time.time >= KnockbackStartTime + knockbackDuration)
=======
        if (Time.time >= knockbackStartTime + knockbackDuration)
>>>>>>> f9594ac2c9a4404ac333ca3aa6d1904365082f39
        {
            SwitchState(State.Moving);
        }
    }

    private void ExitKnockbackState()
    {
<<<<<<< HEAD
        lavawormAnim.SetBool("Knockback", false); // Changed from "aliveAnim" to "lavawormAnim"
=======
        wormAnim.SetBool("Knockback", false);
>>>>>>> f9594ac2c9a4404ac333ca3aa6d1904365082f39
    }

    private void EnterDeadState()
    {
<<<<<<< HEAD
        Instantiate(deathChunkParticle, lavaworm.transform.position, deathChunkParticle.transform.rotation); // Changed from "alive" to "lavaworm"
        Instantiate(deathBloodParticle, lavaworm.transform.position, deathBloodParticle.transform.rotation); // Changed from "alive" to "lavaworm"
=======
        Instantiate(deathChunkParticle, worm.transform.position, deathChunkParticle.transform.rotation);
        Instantiate(deathBloodParticle, worm.transform.position, deathBloodParticle.transform.rotation);
>>>>>>> f9594ac2c9a4404ac333ca3aa6d1904365082f39
        Destroy(gameObject);
    }

    private void UpdateDeadState()
    {
    }

    private void ExitDeadState()
    {
    }

    private void Damage(float[] attackDetails)
    {
        currentHealth -= attackDetails[0];
        Instantiate(hitParticle, worm.transform.position, Quaternion.Euler(0.0f, 0.0f, UnityEngine.Random.Range(0.0f, 360.0f)));

<<<<<<< HEAD
        Instantiate(hitParticle, lavaworm.transform.position, Quaternion.Euler(0.0f, 0.0f, UnityEngine.Random.Range(0.0f, 360.0f))); // Changed from "alive" to "lavaworm"


        if (attackDetails[1] > lavaworm.transform.position.x)  // Changed from "alive" to "lavaworm"
=======
        if (attackDetails[1] > worm.transform.position.x)
>>>>>>> f9594ac2c9a4404ac333ca3aa6d1904365082f39
        {
            damageDirection = -1;
        }
        else
        {
            damageDirection = 1;
        }

<<<<<<< HEAD
        //HitParticle

=======
>>>>>>> f9594ac2c9a4404ac333ca3aa6d1904365082f39
        if (currentHealth > 0.0f)
        {
            SwitchState(State.Knockback);
        }
<<<<<<< HEAD

=======
>>>>>>> f9594ac2c9a4404ac333ca3aa6d1904365082f39
        else if (currentHealth <= 0.0f)
        {
            SwitchState(State.Dead);
        }
    }


    private void CheckTouchDamage()
    {
        if (Time.time >= lastTouchDamageTime + touchDamageCooldown)
        {
            touchDamageBotLeft.Set(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
            touchDamageTopRight.Set(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));

            Collider2D hit = Physics2D.OverlapArea(touchDamageBotLeft, touchDamageTopRight, whatIsPlayer);

            if (hit != null)
            {
                lastTouchDamageTime = Time.time;
                attackDetails[0] = touchDamage;
                attackDetails[1] = worm.transform.position.x;
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
<<<<<<< HEAD
    }
=======
    }   
>>>>>>> f9594ac2c9a4404ac333ca3aa6d1904365082f39

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
<<<<<<< HEAD

=======
>>>>>>> f9594ac2c9a4404ac333ca3aa6d1904365082f39
}
