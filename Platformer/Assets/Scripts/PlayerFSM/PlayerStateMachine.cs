using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalStates;


public class PlayerStateMachine : LivingEntity
{
    public CharacterController characterController;

    [SerializeField]Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 appliedMovement;
    SwipeInput inputManager;
    [SerializeField]bool isMovePressed;
    Animator animator;
    [SerializeField] float moveSpeed = 7;
    float rotationFactorPerFrame = 15f;
    public bool _isJumpPressed;
    [SerializeField]bool _isAttacking;
    [SerializeField]bool _isSwitching;
    Vector3 platformPos;
    [SerializeField] GameObject orientation;
    public PlayerStates _current;
    [SerializeField]float dashVelocity=3f;
    [SerializeField] float distToWallGround;
    [SerializeField] float distToGround;
    public float gravity = -9.8f;
    public float Wallgravity = 9.8f;
    float groundedGravity = -.05f;
    [SerializeField] float distanceToWall;
    float initialJumpVelocity;
    float initialWallJumpVelocity;
    [SerializeField] float maxJumpHeight = 0.85f;
    [SerializeField] float maxJumpTime = 0.85f;
    [SerializeField] float maxWallJumpHeight = 0.85f;
    [SerializeField] float maxWallJumpTime = 0.85f;
    bool isJumping = false;
    [SerializeField] float damage=1f;
    bool jumpCancel;
    [SerializeField] LayerMask wall;
    [SerializeField]Transform rayOrigin;
    //State Variable
    PlayerBaseState _currentState;
    PlayerStateFactory _states;

    //Getters Abnd Setters
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; }}
    public bool isJumpPressed { get { return _isJumpPressed; }}
    public Animator Animator { get { return animator; } }
    public bool IsJumping { set { isJumping= value; }}
    public float MaxJumpHeight { get { return maxJumpHeight; }}
    public float MaxJumpTime { get { return maxJumpTime; }}
    public float CurrentMovementY { get { return currentMovement.y; } set { currentMovement.y = value; }}
    public float InitialJumpVelocity { get { return initialJumpVelocity; }}
    public float InitialWallJumpVelocity { get { return initialWallJumpVelocity; }}

    public CharacterController CharacterController { get { return characterController; } }

    public float GroundedGravity { get { return groundedGravity; }}
    public float Gravity { get { return gravity; }}
    public float WallGravity { get { return Wallgravity; }}

    public bool IsMovePressed { get { return isMovePressed; }set { isMovePressed = value; } }

    public bool IsAttacking { get { return _isAttacking; } set { _isAttacking = value; } }

    public bool IsSwitching { get { return _isSwitching; }}
    public float MoveSpeed { get { return moveSpeed; }}

    public float Speed { get { return Mathf.Abs(currentMovementInput.x); } }

    public float CurrentMovementX { get { return currentMovement.x; } set { currentMovement.x = value; }}
    public float CurrentMovementZ { get { return currentMovement.z; } set { currentMovement.z = value; } }
    public Vector3 AppliedMovement { get { return appliedMovement; }set{ appliedMovement = value; } }
    public float DashVelocity { get { return dashVelocity; }}

    public bool JumpCancel { get { return jumpCancel; }}
    public SwipeInput InputManager { get { return inputManager; }set { } }

    public Vector3 PlatformPosition { get { return platformPos; }set { platformPos = value; } } 
    protected override void Start()
    {
        base.Start();
    }
    void Awake()
    {

        animator = GetComponentInChildren<Animator>();
        characterController = GetComponent<CharacterController>();
        inputManager = GetComponent<SwipeInput>();

        //setup state
        _states = new PlayerStateFactory(this);
        _currentState = _states.platform();
        _currentState.EnterState();
        //_currentState.InitializeSuperState();

        
        
        SetUpJumpVariables();
        SetUpWallJumpVariables();
    }
   
    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(orientation.transform.position, Vector3.forward, Color.red, distToWallGround); 
        //Debug.DrawRay(orientation.transform.position, Vector3.down, Color.red, distToGround);
        //Debug.Log(CheckIfWallGrounded());
        //Debug.Log(CheckIfGrounded());
        HandleInput();
        handleRotation();
        _currentState.UpdateStates();
       // Debug.Log(_currentState._currentParentState);
       // Debug.Log(_currentState._currentSuperState);
        //Debug.Log(_currentState._currentSubState);
        //Debug.Log(CurrentState);
        characterController.Move(currentMovement * Time.deltaTime * moveSpeed);
    }
    public void HandleInput()
    {
        inputManager.handleInput();
        currentMovementInput = new Vector2(inputManager.moveDir, 0);
        isMovePressed = currentMovementInput.x != 0;
        appliedMovement.x = currentMovementInput.x;

        if (inputManager.up)
        {
            _isJumpPressed = true;
        }
        if (!inputManager.up)
        {
            _isJumpPressed = false;
        }
        if (inputManager.left || inputManager.right)
        {
            _isAttacking = true;
        }else if (!inputManager.left || inputManager.right)
        {
            _isAttacking= false;
        } 

        if (inputManager.wallRunningInput)
        {
            _isSwitching = true;
        }
        else
        {
            _isSwitching = false;
        }
        if(inputManager.down)
        {
            jumpCancel = true;
        }
        else { jumpCancel = false; }
    }

    void handleRotation()
    {
        Vector3 positionToLookAt;

        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0;
        positionToLookAt.z = 0;

        Quaternion currentRotation = transform.rotation;

        if (isMovePressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame);
        }
        if (inputManager.left || inputManager.right)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame);
        }
    }

    void SetUpJumpVariables()
    {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }
    void SetUpWallJumpVariables()
    {
        float timeToApex = maxWallJumpTime / 2;
        Wallgravity = (-2 * maxWallJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialWallJumpVelocity = (2 * maxWallJumpHeight) / timeToApex;
    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (IsAttacking)
        {
            IDamageable damageableObject = hit.gameObject.GetComponent<IDamageable>();
            if (damageableObject != null)
            {

                
                damageableObject.TakeDamage(damage);
                //if (inputManager.right)
                //{
                //    CurrentMovementX = 30;
                //}
                //if (inputManager.left)
                //{
                //    CurrentMovementX = -30;
                //}
            }
        }
    }

    public bool CheckIfWallGrounded()
    {
        return Physics.Raycast(orientation.transform.position, Vector3.forward, distToWallGround + 0.1f,wall);
        
    } 
    public bool CheckIfWallJumpGrounded()
    {
        return Physics.Raycast(orientation.transform.position, Vector3.forward, distanceToWall,wall);
        
    }
    public bool CheckIfGrounded()
    {
      return  Physics.CheckSphere(rayOrigin.position, distToGround ,wall);
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(rayOrigin.position, distToGround);
    }
}
