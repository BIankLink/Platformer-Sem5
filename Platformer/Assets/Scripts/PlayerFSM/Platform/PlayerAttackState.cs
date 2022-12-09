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
       // AudioManager.instance.Play("Dash");
        Ctx.Animator.SetTrigger("isAttacking");
        HandleForce();
    }
    public override void UpdateState()
    {
        
        CheckSwitchState();
       
    }
    public override void ExitState() 
    {
        Ctx.DashMultiplier= Vector3.zero;
        


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
            Ctx.DashMultiplier = new Vector3 (Ctx.DashVelocity,0,0);
        }else if (Ctx.InputManager.left)
        {
            Ctx.DashMultiplier = new Vector3(-Ctx.DashVelocity, 0, 0);
        }
    }
    void handleDamage()
    {

    }
}
