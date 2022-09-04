using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallFallingState : PlayerBaseState
{
    public PlayerWallFallingState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) 
    { 
        _isSuperState = true;
    }

    public override void EnterState() { }
    public override void UpdateState() 
    {
        HandleGravity();
        CheckSwitchState();
    }
    public override void ExitState()
    {
        Ctx.CurrentMovementY = 0;
    }
    public override void CheckSwitchState()
    {
        if (Ctx.CheckIfWallGrounded())
        {
            Debug.Log("WallGrounded");
            SwitchState(Factory.wallGrounded());
        }
    }
    public override void InitializeSuperState() { }
    public override void InitializeSubState()
    {
        if (!Ctx.IsMovePressed && !Ctx.isJumpPressed)
        {
            
            SetSubState(Factory.wallIdle());
        }
    }
    void HandleGravity()
    {
        bool isFalling = Ctx.CurrentMovementZ <= 0 || !Ctx.isJumpPressed;
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
