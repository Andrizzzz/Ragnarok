using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerGroundedState : PlayerState
{
    protected Vector2 input;

    protected int xInput;
    //protected int yInput;

    //protected bool isTouchingCeiling;

    private bool JumpInput;
    private bool grabInput;
    private bool isGrounded;
    private bool isTouchingWall;
    //private bool isTouchingLedge;
    //private bool dashInput;

    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.CheckIfGrounded();
        isTouchingWall = player.CheckIfTouchingWall();
        //isTouchingLedge = core.CollisionSenses.LedgeHorizontal;
        //isTouchingCeiling = core.CollisionSenses.Ceiling;
    }

    public override void Enter()
    {
        base.Enter();

        player.JumpState.ResetAmountOfJumpsLeft();
        //player.DashState.ResetCanDash();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.InputHandler.NormInputX;
        //yInput = player.InputHandler.NormInputY;
            JumpInput = player.InputHandler.JumpInput;
            grabInput = player.InputHandler.GrabInput;
        //    dashInput = player.InputHandler.DashInput;

            //    if (player.InputHandler.AttackInputs[(int)CombatInputs.primary] && !isTouchingCeiling)
            //    {
            //        stateMachine.ChangeState(player.PrimaryAttackState);
            //    }
            //    else if (player.InputHandler.AttackInputs[(int)CombatInputs.secondary] && !isTouchingCeiling)
            //    {
            //        stateMachine.ChangeState(player.SecondaryAttackState);
            //    }
            if (JumpInput && player.JumpState.CanJump())
        {
            player.InputHandler.UseJumpInput();
            stateMachine.ChangeState(player.JumpState);
        }
        else if (!isGrounded)
        {
            player.InAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.InAirState);
        }
            else if (isTouchingWall && grabInput)
        {
            stateMachine.ChangeState(player.WallGrabState);
        }
        //    else if (dashInput && player.DashState.CheckIfCanDash() && !isTouchingCeiling)
        //    {
        //        stateMachine.ChangeState(player.DashState);
        //    }
        //
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}