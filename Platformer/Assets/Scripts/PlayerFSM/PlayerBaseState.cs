using GlobalStates;
//using System.Diagnostics;
using UnityEngine;
public abstract class PlayerBaseState 
{
    public enum Layer { Parent,Super,Sub};

    public PlayerStates states;
    public Layer state;
    protected bool _isParentState = false;
    protected bool _isSuperState = false;
    protected bool _isSubState=false;
    
    private PlayerStateMachine _ctx;
    private PlayerStateFactory _factory;
    private PlayerBaseState _currentState;
    private PlayerBaseState _currentSubState;
    private PlayerBaseState _currentSuperState;
    private PlayerBaseState _currentParentState;
    
    public PlayerBaseState CurrentParentState { get { return _currentParentState; } }
    public PlayerBaseState CurrentSuperState { get { return _currentSuperState; } }
    public  PlayerBaseState CurrentSubState { get { return _currentSubState; } }

    protected PlayerStateMachine Ctx { get { return _ctx; } }
    protected PlayerStateFactory Factory { get { return _factory; } }


    public PlayerBaseState(PlayerStateMachine currentContext,PlayerStateFactory playerStateFactory)
    {
        _ctx = currentContext;
        _factory = playerStateFactory;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
    public abstract void CheckSwitchState();
    public abstract void InitializeSuperState();
    public abstract void InitializeSubState();

    public void UpdateStates()
    { 
        UpdateState();
        if (_currentSuperState != null )
        {            
            _currentSuperState.UpdateStates();
        }
        if (_currentSubState != null)
        {
            _currentSubState.UpdateState();
        }
    }

    protected void SwitchState(PlayerBaseState newState) 
    { 
        ExitState();

        if (newState._isParentState)
        {
            _ctx.CurrentState = newState;
            
        }

        //Debug.Log("the current state is " + CurrentSuperState.states);
        else if (_currentParentState!= null)
        {
            //Debug.Log("Not null");
            
            _currentParentState.SetSuperState(newState);
        }
        else if(_currentSuperState != null)
        {
            _currentSuperState.SetSubState(newState);
        }

        newState.EnterState();
        //switch (state)
        //{
        //    case Layer.Parent:
        //        _ctx.CurrentState = newState;
              
        //        break;
        //    case Layer.Super:
        //       _currentParentState.SetSuperState(newState);
        //        break;

        //    case Layer.Sub:
        //        _currentSuperState.SetSubState(newState);
        //        break;
        //}    

    }

    protected void SetParentState(PlayerBaseState newParentState)
    {
        _currentParentState = newParentState;
        
    }

    protected void SetSuperState(PlayerBaseState newSuperstate) 
    {
        _currentSuperState = newSuperstate;
        if (this._isParentState)
        {
            newSuperstate.SetParentState(this);
        }
        Debug.Log(_currentSuperState);
    }

    protected void SetSubState(PlayerBaseState newSubState) 
    {
        _currentSubState = newSubState;
        newSubState.SetSuperState(this);
       // Debug.Log(_currentSubState);
    }


}
