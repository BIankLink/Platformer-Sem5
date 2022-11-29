
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallState : PlayerBaseState
{
    public PlayerWallState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) 
    { 
        _isParentState=true;
        state = Layer.Parent;
    }

    public override void EnterState() 
    {
        Ctx.Animator.SetBool("Wall", true);
        Ctx.CurrentMovementX = 0;
        Ctx.CurrentMovementY = 0;
        Ctx.CurrentMovementZ = -0.5f;
        InitializeSuperState();
    }
    public override void UpdateState() 
    {
        
        CheckSwitchState();
    }
    public override void ExitState() 
    {
        //Debug.Log("worked");
        Ctx.CurrentMovementZ = 0.5f;
        Ctx.CurrentMovementY = Ctx.SwitchJump;
        Ctx.CurrentMovementX = 0;
    }
    public override void CheckSwitchState()
    {
        if (!Ctx.IsSwitching && CurrentSuperState == Factory.wallGrounded())
        {
            
            SwitchState(Factory.platform());
        }
    }
    public override void InitializeSuperState() 
    {
        if (Ctx.CheckIfWallGrounded())
        {
            //Debug.Log("Grounded");
            SetSuperState(Factory.wallGrounded());
        }
        else if (!Ctx.CheckIfWallGrounded() && Ctx.IsAttacking)
        {
            //Debug.Log("jumping");
            SetSuperState(Factory.dashJump());
        }
        else if (!Ctx.isJumpPressed && !Ctx.CheckIfWallGrounded())
        {


            SetSuperState(Factory.wallFalling());
            
        }
    }
    public override void InitializeSubState() { }
}
