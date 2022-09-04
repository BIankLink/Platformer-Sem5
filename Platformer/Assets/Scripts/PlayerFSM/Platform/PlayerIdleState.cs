using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) 
    {
        state = Layer.Sub;
        _isSubState = true;
    }

    public override void EnterState() 
    {
        
        //Debug.Log("idle");
        Ctx.Animator.SetFloat("SpeedPercent", Ctx.Speed);
        Ctx.CurrentMovementX = Ctx.AppliedMovement.x;
        //Ctx.CurrentMovementX = 0;
    }
    public override void UpdateState()
    {
        //Ctx.CurrentMovementX = Ctx.AppliedMovement.x;
        CheckSwitchState();
    }
    public override void ExitState() { }
    public override void CheckSwitchState()
    {
        if (Ctx.IsMovePressed)
        {
            SwitchState(Factory.platformRun());
            
        }
        if (Ctx.IsAttacking)
        {
            
            SwitchState(Factory.platformAttack());
            
        }
        if (Ctx.JumpCancel && !Ctx.CharacterController.isGrounded)
        {
            
            SwitchState(Factory.platformJumpCancel());
        }

    }
    public override void InitializeSuperState() { }
    public override void InitializeSubState() { }
}
