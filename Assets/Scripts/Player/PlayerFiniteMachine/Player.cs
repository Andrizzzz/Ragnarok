using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SocialPlatforms.Impl;

public class Player : MonoBehaviour
{
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    //public PlayerWallSlideState WallSlideState { get; private set; }
    //public PlayerWallGrabState WallGrabState { get; private set; }
    //public PlayerWallClimbState WallClimbState { get; private set; }
    //public PlayerWallJumpState WallJumpState { get; private set; }
    //public PlayerLedgeClimbState LedgeClimbState { get; private set; }
    //public PlayerDashState DashState { get; private set; }
    //public PlayerCrouchIdleState CrouchIdleState { get; private set; }
    //public PlayerCrouchMoveState CrouchMoveState { get; private set; }
    //public PlayerAttackState PrimaryAttackState { get; private set; }
    //public PlayerAttackState SecondaryAttackState { get; private set; }

    [SerializeField]
    private PlayerData playerData;
    [SerializeField]
    private Transform groundCheck;

    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public Transform DashDirectionIndicator { get; private set; }
    public BoxCollider2D MovementCollider { get; private set; }
    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; }
    // public PlayerInventory Inventory { get; private set; }


    private Vector2 workspace;


    private void Awake()
    {
        //   Core = GetComponentInChildren<Core>();

        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
        //WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "wallSlide");
        //WallGrabState = new PlayerWallGrabState(this, StateMachine, playerData, "wallGrab");
        //WallClimbState = new PlayerWallClimbState(this, StateMachine, playerData, "wallClimb");
        //WallJumpState = new PlayerWallJumpState(this, StateMachine, playerData, "inAir");
        //LedgeClimbState = new PlayerLedgeClimbState(this, StateMachine, playerData, "ledgeClimbState");
        //DashState = new PlayerDashState(this, StateMachine, playerData, "inAir");
        //CrouchIdleState = new PlayerCrouchIdleState(this, StateMachine, playerData, "crouchIdle");
        //CrouchMoveState = new PlayerCrouchMoveState(this, StateMachine, playerData, "crouchMove");
        //PrimaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "attack");
        //SecondaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "attack");
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();

        FacingDirection = 1;
        //DashDirectionIndicator = transform.Find("DashDirectionIndicator");
        //MovementCollider = GetComponent<BoxCollider2D>();
        //Inventory = GetComponent<PlayerInventory>();

        //PrimaryAttackState.SetWeapon(Inventory.weapons[(int)CombatInputs.primary]);
        //SecondaryAttackState.SetWeapon(Inventory.weapons[(int)CombatInputs.primary]);
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        // Core.LogicUpdate();
        CurrentVelocity = RB.velocity;
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }


    //public void SetColliderHeight(float height)
    //{
    //    Vector2 center = MovementCollider.offset;
    //    workspace.Set(MovementCollider.size.x, height);

    //    center.y += (height - MovementCollider.size.y) / 2;

    //    MovementCollider.size = workspace;
    //    MovementCollider.offset = center;
    //}

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimtionFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
    }
    public void CheckIfShouldFlip(int xInput)
    {
        if(xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }

}