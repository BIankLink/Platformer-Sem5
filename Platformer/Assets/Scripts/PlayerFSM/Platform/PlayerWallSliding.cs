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
        if(Ctx.CheckIfWallSliding() && Ctx.isJumpPressed && !Ctx.IsMovePressed)
        {
            SwitchState(Factory.platformJump());
        }
    }

    public override void EnterState()
    {
        
        InitializeSubState();
        
       
       
    }

    public override void ExitState()
    {
        if (!Ctx.CheckIfGrounded())
        {
            Debug.Log("blah");
            if (Ctx.transform.rotation == Quaternion.Euler(0, 90, 0))
            {
                
                Ctx.CurrentMovementX = -1f;
            }
            else if (Ctx.transform.rotation == Quaternion.Euler(0, -90, 0))
            {
                
                Ctx.CurrentMovementX = 1f;
            }
        }
       
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
        Ctx.CurrentMovementY = Ctx.GroundedGravity;

        if (Ctx.IsMovePressed && Ctx.CheckIfWallSliding())
        {
            
            Ctx.CurrentMovementY = 0;
        }
        CheckSwitchState();
    }

}
