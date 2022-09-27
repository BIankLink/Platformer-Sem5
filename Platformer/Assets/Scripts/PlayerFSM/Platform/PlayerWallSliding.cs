using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSliding : PlayerBaseState
{
    public PlayerWallSliding(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        _isSuperState = true;
        state = Layer.Super;
    }
    public override void CheckSwitchState()
    {
        if (!Ctx.CheckIfGrounded() && !Ctx.isJumpPressed && !Ctx.CheckIfWallSliding())
        {
            SwitchState(Factory.platformFalling());
        }
        if (Ctx.CheckIfGrounded())
        {
            SwitchState(Factory.platformGrounded());
        }
    }

    public override void EnterState()
    {
        Ctx.CurrentMovementX += 0.5f;
        Ctx.CurrentMovementY = Ctx.GroundedGravity;
    }

    public override void ExitState()
    {
        Ctx.CurrentMovementX -= 0.5f;
    }

    public override void InitializeSubState()
    {
        if (Ctx.IsMovePressed)
        {
            SetSubState(Factory.platformRun());
        }
        if (Ctx.JumpCancel)
        {
            SetSubState(Factory.platformJumpCancel());
        }
    }

    public override void InitializeSuperState()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState()
    {
        CheckSwitchState();
    }

}
