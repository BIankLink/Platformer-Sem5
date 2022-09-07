using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) 
    {
        state=Layer.Super;
        _isSuperState = true;
    }

    public override void EnterState() 
    {
        Ctx.CanSwitch = true;
        InitializeSubState();
        
        Ctx.CurrentMovementZ = 0;
        Ctx.CurrentMovementY = Ctx.GroundedGravity;
    }
    public override void UpdateState() 
    {
        
        CheckSwitchState();
    }
    public override void ExitState()
    {
        Ctx.CanSwitch = false;
        // Debug.Log("in the grounded EXIT");
    }
    public override void CheckSwitchState() 
    {
        if (Ctx.isJumpPressed && Ctx.CheckIfGrounded())
        {
            
            SwitchState(Factory.platformJump());

        }
        if(!Ctx.CheckIfGrounded()&& !Ctx.isJumpPressed)
        {
            
            SwitchState(Factory.platformFalling());
           
        }
       
    }
    public override void InitializeSuperState() { }
    public override void InitializeSubState() 
    {
        if(!Ctx.IsMovePressed && !Ctx.isJumpPressed)
        {
            
            SetSubState(Factory.platformIdle());
        }else if(Ctx.IsMovePressed && !Ctx.isJumpPressed)
        {
            //Debug.Log("run is beign set");
            SetSubState(Factory.platformRun());
        }
        if (Ctx.IsAttacking)
        {
            SetSubState(Factory.platformAttack());
        }     

    }
}
