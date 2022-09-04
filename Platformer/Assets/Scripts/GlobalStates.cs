using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalStates 
{

    public enum PlayerStates
    {
        Platform,
        PlatformIdle,
        PlatformRun,
        PlatformAttack,
        PlatformGrounded,
        PlatformFalling,
        PlatformJump,
        PlatformJumpCancel,
        Wall,
        WallIdle,
        WallRun,
        WallDashJump,
        WallUpAttack,
        WallGrounded,
        WallFalling,
    };
}
