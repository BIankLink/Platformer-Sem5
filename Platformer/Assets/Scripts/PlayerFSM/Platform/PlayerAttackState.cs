using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState :PlayerBaseState
{
    public PlayerAttackState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        state = Layer.Sub;
        _isSubState = true; 
    }

    public override void EnterState() 
    {
        HandleForce();
        Ctx.Animator.SetTrigger("isAttacking");
    }
    public override void UpdateState()
    {
        
        CheckSwitchState();
       
    }
    public override void ExitState() 
    {
        Ctx.CurrentMovementX= 0;
        
        
       
    }
    public override void CheckSwitchState() 
    {
        if ( !Ctx.IsAttacking)
        {
            SwitchState(Factory.platformIdle());
            
        }
        //if(Ctx.IsMovePressed && !Ctx.IsAttacking)
        //{
        //    SwitchState(Factory.platformRun());
            
        //}
      
    }
    public override void InitializeSuperState() { }
    public override void InitializeSubState() { }

    void HandleForce()
    {
        Debug.Log("attackBeing Called");
        if (Ctx.InputManager.right)
        {
            Ctx.CurrentMovementX = Ctx.DashVelocity;
        }else if (Ctx.InputManager.left)
        {
            Ctx.CurrentMovementX = -Ctx.DashVelocity;
        }
    }
    void handleDamage()
    {

    }
}
