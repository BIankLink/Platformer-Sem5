using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitchToWallState : PlayerBaseState
{
    public PlayerSwitchToWallState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

    public override void EnterState() { }
    public override void UpdateState() { }
    public override void ExitState() { }
    public override void CheckSwitchState() 
    {
        
    }
    public override void InitializeSuperState() { }
    public override void InitializeSubState() { }
}
