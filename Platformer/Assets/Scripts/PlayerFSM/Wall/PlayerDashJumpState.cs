using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashJumpState : PlayerBaseState
{
    public PlayerDashJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) 
    {
        _isSuperState = true;
    }
    public override void EnterState() 
    { 
        InitializeSubState();
        HandleJump();
    }
    public override void UpdateState() 
    {
        
        CheckSwitchState();
    }
    public override void ExitState() 
    { 
        //exit Animation
    }
    public override void CheckSwitchState()
    {
        if(Ctx.CheckIfWallJumpGrounded() && !Ctx.IsAttacking)
        {
            SwitchState(Factory.wallJumpFalling());
        }
        if (Ctx.CheckIfWallGrounded() && !Ctx.IsAttacking)
        {
            SwitchState(Factory.wallGrounded());

        }
        if (!Ctx.CheckIfWallGrounded() && !Ctx.IsAttacking && !Ctx.CheckIfWallJumpGrounded())
        {
            SwitchState(Factory.wallFalling());

        } 
    }
    public override void InitializeSuperState() { }
    public override void InitializeSubState()
    {
        
        if (Ctx.IsMovePressed)
        {
            SetSubState(Factory.wallRun());
        }
    }
    void HandleJump()
    {
        Ctx.Animator.SetBool("Jump", true);
        Ctx.IsJumping = true;
        Ctx.CurrentMovementZ = /*-(Ctx.InitialWallJumpVelocity * 0.5f)*/-2f;
    }
   
}
