using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallIdleState : PlayerBaseState
{
    public PlayerWallIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) 
    { 
        _isSubState = true;
    }

    public override void EnterState() 
    { 
        // idle amimation
    }
    public override void UpdateState()
    { 
        CheckSwitchState();
    }
    public override void ExitState() { }
    public override void CheckSwitchState()
    {
        if (Ctx.IsMovePressed)
        {
            SwitchState(Factory.wallRun());
        }
        if (Ctx.isJumpPressed)
        {
            SwitchState(Factory.wallUpAttack());
        }
    }
    public override void InitializeSuperState() { }
    public override void InitializeSubState() { }
}
