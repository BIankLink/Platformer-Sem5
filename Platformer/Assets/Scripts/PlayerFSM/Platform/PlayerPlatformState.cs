using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformState : PlayerBaseState
{
    public PlayerPlatformState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        state = Layer.Parent;
        _isParentState = true;

    }

    public override void EnterState()
    {
        InitializeSuperState();
        

    }
    public override void UpdateState() 
    {
        
        CheckSwitchState();
        
    }
    public override void ExitState() 
    {
        
    }
    public override void CheckSwitchState()
    {
        if (Ctx.IsSwitching)
        {
            
            SwitchState(Factory.wall());
            
        }
        
    }
    public override void InitializeSuperState() 
    {
        if (Ctx.CharacterController.isGrounded)
        {
            //Debug.Log("Grounded");
            SetSuperState(Factory.platformGrounded());
        }
        else if (!Ctx.characterController.isGrounded && Ctx.isJumpPressed)
        {
            //Debug.Log("jumping");
            SetSuperState(Factory.platformJump());
        }
        else if(!Ctx.isJumpPressed && !Ctx.characterController.isGrounded)
        {
            
            
            SetSuperState(Factory.platformFalling());
            //Debug.Log(CurrentSuperState);
        }
    }
    public override void InitializeSubState() 
    {
    }
}
