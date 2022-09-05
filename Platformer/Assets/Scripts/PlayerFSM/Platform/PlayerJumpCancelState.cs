using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpCancelState : PlayerBaseState
{
    public PlayerJumpCancelState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) 
    { 
        _isSubState = true;
        state = Layer.Sub;
    }

    public override void EnterState() 
    {
        
        HandleJumpCancel();
    }
    public override void UpdateState() 
    {
        
        CheckSwitchState();
    }
    public override void ExitState() 
    {
        Ctx.CurrentMovementY = 0;
    }
    public override void CheckSwitchState()
    {
        if (!Ctx.IsMovePressed && Ctx.CheckIfGrounded())
        {
            
            SwitchState(Factory.platformIdle());
        }
        if (Ctx.IsAttacking)
        {
            SwitchState(Factory.platformAttack());
        }
        if (Ctx.IsMovePressed)
        {
            SwitchState(Factory.platformRun());
        }
    }
    public override void InitializeSuperState() { }
    public override void InitializeSubState() { }
    
    void HandleJumpCancel()
    {
        
        Ctx.CurrentMovementY = Ctx.gravity *2;     
    }
}
