using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        _isSuperState = true;
        state = Layer.Super;
    }

    public override void EnterState() 
    {
        InitializeSubState();
        HandleJump();
        
        //Debug.Log("jump");
    }
    public override void UpdateState()
    {
        //Debug.Log("Blah");
        CheckSwitchState();
    }
    public override void ExitState()
    {
        Ctx.Animator.SetBool("Jump", false);
        
    }
    public override void CheckSwitchState() 
    {
        if(!Ctx.CharacterController.isGrounded &&!Ctx.isJumpPressed)
        {
            SwitchState(Factory.platformFalling());
            
        }
       
    }
    public override void InitializeSuperState() { }
    public override void InitializeSubState() 
    {
        if (Ctx.IsAttacking)
        {
            SetSubState(Factory.platformAttack());
        }
        if (Ctx.IsMovePressed)
        {
            SetSubState(Factory.platformRun());
        }
        if (Ctx.JumpCancel)
        {
            SetSubState(Factory.platformJumpCancel());
        }
    }

    void HandleJump()
    {
        Ctx.Animator.SetBool("Jump", true);
        Ctx.IsJumping = true;
        Ctx.CurrentMovementY = Ctx.InitialJumpVelocity * 0.5f;
    }

    
}
