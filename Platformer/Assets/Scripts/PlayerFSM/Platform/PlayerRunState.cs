using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        state = Layer.Sub;
        _isSubState = true;
    }

    public override void EnterState() 
    {
       // AudioManager.instance.Play("Run");
    }
        
    public override void UpdateState()
    {

        
        if (Ctx.CheckIfGrounded())
        {
            Ctx.Animator.SetFloat("SpeedPercent", Ctx.Speed);
        }
        Ctx.CurrentMovementX = Ctx.AppliedMovement.x;
            CheckSwitchState();
    }
    public override void ExitState() 
    { 
        Ctx.Animator.SetFloat("SpeedPercent", Ctx.Speed);
        //AudioManager.instance.Stop("Run");
        

       
    }
    public override void CheckSwitchState()
    {
        if (!Ctx.IsMovePressed)
        {
            
            SwitchState(Factory.platformIdle());
            
        }
        if (Ctx.IsAttacking)
        { 
            
            SwitchState(Factory.platformAttack());
        }
        
    }

    public override void InitializeSuperState() { }
    public override void InitializeSubState() { }

}
