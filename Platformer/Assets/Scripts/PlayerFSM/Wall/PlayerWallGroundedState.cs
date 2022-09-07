using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallGroundedState : PlayerBaseState
{
    public PlayerWallGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) 
    { 
        _isSuperState = true;
    }

    public override void EnterState() 
    { 

        InitializeSubState();
        Ctx.CanSwitch = true;
        Ctx.Animator.SetTrigger("Land");
        Ctx.CurrentMovementY = 0;
        Ctx.CurrentMovementZ = -Ctx.GroundedGravity;
    }
    public override void UpdateState() 
    { 
        CheckSwitchState();
    }
    public override void ExitState() 
    {
        Ctx.CanSwitch = false;
    }
    public override void CheckSwitchState()
    {
        if (Ctx.IsAttacking && Ctx.CheckIfWallGrounded())
        {

            SwitchState(Factory.dashJump());

        }
        if (!Ctx.CheckIfWallGrounded() && !Ctx.IsAttacking)
        {

            SwitchState(Factory.wallFalling());

        }
    }
    public override void InitializeSuperState() { }
    public override void InitializeSubState() 
    {
        if (!Ctx.IsMovePressed && !Ctx.IsAttacking)
        {
           
            SetSubState(Factory.wallIdle());
        }
        else if (Ctx.IsMovePressed && !Ctx.IsAttacking)
        {
            
            SetSubState(Factory.wallRun());
        }
        if (Ctx.isJumpPressed)
        {
            SetSubState(Factory.wallUpAttack());
        }
    }
}
