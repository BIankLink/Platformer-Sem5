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
        Ctx.CurrentMovementY = 0;
        Ctx.CurrentMovementZ = -Ctx.GroundedGravity;
    }
    public override void UpdateState() { }
    public override void ExitState() { }
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
            Debug.Log("idle is beign set");
            SetSubState(Factory.wallIdle());
        }
        else if (Ctx.IsMovePressed && !Ctx.IsAttacking)
        {
            Debug.Log("run is beign set");
            SetSubState(Factory.wallRun());
        }
        if (Ctx.isJumpPressed)
        {
            SetSubState(Factory.wallUpAttack());
        }
    }
}
