using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallRunState : PlayerBaseState
{
    public PlayerWallRunState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) 
    { 
        _isSubState = true;
    }

    public override void EnterState() 
    { 

    }
    public override void UpdateState()
    {
        Ctx.Animator.SetFloat("SpeedPercent", Ctx.Speed);
        Ctx.CurrentMovementX = Ctx.AppliedMovement.x;
        CheckSwitchState();
    }
    public override void ExitState() 
    {
        Ctx.Animator.SetFloat("SpeedPercent", Ctx.Speed);
    }
    public override void CheckSwitchState()
    {

        if (!Ctx.IsMovePressed)
        {
            SwitchState(Factory.wallIdle());

        }
        if (Ctx.isJumpPressed)
        {

            SwitchState(Factory.wallUpAttack());
        }
    }
    public override void InitializeSuperState() { }
    public override void InitializeSubState() { }
}
