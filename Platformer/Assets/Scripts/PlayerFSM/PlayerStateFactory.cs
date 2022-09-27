using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalStates;


public class PlayerStateFactory
{


    PlayerStateMachine _context;

    public Dictionary<PlayerStates, PlayerBaseState> _states = new Dictionary<PlayerStates, PlayerBaseState>();

    public PlayerStateFactory(PlayerStateMachine currentContext)
    {
        _context = currentContext;
        _states[PlayerStates.Platform] = new PlayerPlatformState(_context, this);
        _states[PlayerStates.PlatformIdle] = new PlayerIdleState(_context, this);
        _states[PlayerStates.PlatformRun] = new PlayerRunState(_context, this);
        _states[PlayerStates.PlatformAttack] = new PlayerAttackState(_context, this);
        _states[PlayerStates.PlatformGrounded] = new PlayerGroundedState(_context, this);
        _states[PlayerStates.PlatformFalling] = new PlayerFallingState(_context, this);
        _states[PlayerStates.PlatformJump] = new PlayerJumpState(_context, this);
        _states[PlayerStates.PlatformJumpCancel] = new PlayerJumpCancelState(_context, this);
        _states[PlayerStates.PlatformWallSliding] = new PlayerWallSliding(_context, this);
        _states[PlayerStates.Wall] = new PlayerWallState(_context, this);
        _states[PlayerStates.WallIdle] = new PlayerWallIdleState(_context, this);
        _states[PlayerStates.WallRun] = new PlayerWallRunState(_context, this);
        _states[PlayerStates.WallDashJump] = new PlayerDashJumpState(_context, this);
        _states[PlayerStates.WallGrounded] = new PlayerWallGroundedState(_context, this);
        _states[PlayerStates.WallFalling] = new PlayerWallFallingState(_context, this);
        _states[PlayerStates.WallUpAttack] = new PlayerUpAttackState(_context, this);
        _states[PlayerStates.wallJumpFalling] = new PlayerWallJumpFalling(_context, this);
    }

    #region Platform
    public PlayerBaseState platform()
    {
        _states[PlayerStates.Platform].states = PlayerStates.Platform;
        return _states[PlayerStates.Platform];
    }
    public PlayerBaseState platformIdle()
    {
        _states[PlayerStates.Platform].states = PlayerStates.PlatformIdle;
        return _states[PlayerStates.PlatformIdle];
    }
    public PlayerBaseState platformGrounded()
    {
        _states[PlayerStates.Platform].states = PlayerStates.PlatformGrounded;
        return _states[PlayerStates.PlatformGrounded];
    }
    public PlayerBaseState platformRun()
    {
        _states[PlayerStates.Platform].states = PlayerStates.PlatformRun;
        return _states[PlayerStates.PlatformRun];
    }
    public PlayerBaseState platformFalling()
    {
        _states[PlayerStates.Platform].states = PlayerStates.PlatformFalling;
        return _states[PlayerStates.PlatformFalling];
    }
    public PlayerBaseState platformAttack()
    {
        _states[PlayerStates.Platform].states = PlayerStates.PlatformAttack;
        return _states[PlayerStates.PlatformAttack];
    }

    public PlayerBaseState platformJump()
    {
        _states[PlayerStates.Platform].states = PlayerStates.PlatformJump;
        return _states[PlayerStates.PlatformJump];
    }
    public PlayerBaseState platformJumpCancel()
    {
        _states[PlayerStates.Platform].states = PlayerStates.PlatformJumpCancel;
        return _states[PlayerStates.PlatformJumpCancel];
    }
    public PlayerBaseState platformWallSliding()
    {
        _states[PlayerStates.Platform].states = PlayerStates.PlatformWallSliding;
        return _states[PlayerStates.PlatformWallSliding];
    }
    #endregion

    #region Wall
    public PlayerBaseState wall()
    {
        return _states[PlayerStates.Wall];
    }
    public PlayerBaseState wallGrounded()
    {
        return _states[PlayerStates.WallGrounded];
    }
    public PlayerBaseState wallFalling()
    {
        return _states[PlayerStates.WallFalling];
    }
    public PlayerBaseState dashJump()
    {
        return _states[PlayerStates.WallDashJump];
    }
    public PlayerBaseState wallRun()
    {
        return _states[PlayerStates.WallRun];
    }
    public PlayerBaseState wallUpAttack()
    {
        return _states[PlayerStates.WallUpAttack];
    }
    public PlayerBaseState wallIdle()
    {
        return _states[PlayerStates.WallIdle];
    }

    public PlayerBaseState wallJumpFalling()
    {
        return _states[PlayerStates.wallJumpFalling];
    }

    #endregion


}
