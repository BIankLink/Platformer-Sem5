using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : PlayerBaseState
{
    public PlayerFallingState(PlayerStateMachine currentContext,PlayerStateFactory playerStateFactory):base(currentContext,playerStateFactory) 
    {
        state = Layer.Super;
        _isSuperState = true;
    }

    public override void EnterState() 
    {
        InitializeSubState();
        
    }
    public override void UpdateState()
    {

        HandleGravity();
       
       
        CheckSwitchState();
    }
    public override void ExitState() 
    {
        Ctx.Animator.SetTrigger("Land");
    }
    public override void CheckSwitchState()
    {
        if (Ctx.CheckIfGrounded())
        {
            SwitchState(Factory.platformGrounded());
        }
        if (Ctx.CheckIfWallSliding())
        {
            
            SwitchState(Factory.platformWallSliding());
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
        if (!Ctx.IsMovePressed && !Ctx.IsAttacking)
        {
           
            SetSubState(Factory.platformIdle());
        }
        if (Ctx.JumpCancel)
        {
            SetSubState(Factory.platformJumpCancel());
        }
    }
    void HandleGravity()
    {
        bool isFalling = Ctx.CurrentMovementY <= 0 || !Ctx.isJumpPressed;
        float fallMultiplier = 2f;

        if (isFalling)
        {

            float previousYVelocity = Ctx.CurrentMovementY;
            float newYVelocity = Ctx.CurrentMovementY + (Ctx.Gravity * fallMultiplier * Time.deltaTime);
            float nextYVelocity = Mathf.Max((previousYVelocity + newYVelocity) * 0.5f, -20f);
            Ctx.CurrentMovementY = nextYVelocity;
            
        }
        else
        {
            float previousYVelocity = Ctx.CurrentMovementY;
            float newYVelocity = Ctx.CurrentMovementY + (Ctx.Gravity * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * 0.5f;
            Ctx.CurrentMovementY = nextYVelocity;
        }
    }
}
