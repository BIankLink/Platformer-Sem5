using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpFalling : PlayerBaseState
{
    public PlayerWallJumpFalling(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        state = Layer.Super;
        _isSuperState = true;
    }

public override void EnterState()
{
    InitializeSubState();
        Debug.Log("EnteredJumpFalling");

}
public override void UpdateState()
{
        Debug.Log("JumpFalling");
        HandleGravity();


    CheckSwitchState();
}
public override void ExitState()
{
    Ctx.Animator.SetTrigger("Land");
}
public override void CheckSwitchState()
{
    if (Ctx.CheckIfWallGrounded())
    {

        SwitchState(Factory.wallGrounded());

    }

}
public override void InitializeSuperState() { }
public override void InitializeSubState()
{

   
    if (Ctx.IsMovePressed)
    {

        SetSubState(Factory.wallRun());
    }
    if (!Ctx.IsMovePressed && !Ctx.IsAttacking)
    {

        SetSubState(Factory.wallIdle());
    }

}
void HandleGravity()
{
    bool isFalling = Ctx.CurrentMovementY <= 0 || !Ctx.isJumpPressed;
    float fallMultiplier = 2f;

    if (isFalling)
    {

        float previousYVelocity = Ctx.CurrentMovementZ;
        float newYVelocity = Ctx.CurrentMovementZ + (Ctx.WallGravity * fallMultiplier * Time.deltaTime);
        float nextYVelocity = Mathf.Max((previousYVelocity + newYVelocity) * 0.5f, -20f);
        Ctx.CurrentMovementZ = nextYVelocity;
    }
    else
    {
        float previousYVelocity = Ctx.CurrentMovementZ;
        float newYVelocity = Ctx.CurrentMovementZ + (Ctx.WallGravity * Time.deltaTime);
        float nextYVelocity = (previousYVelocity + newYVelocity) * 0.5f;
        Ctx.CurrentMovementZ = nextYVelocity;
        Debug.Log(nextYVelocity);
    }
}
}
