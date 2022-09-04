using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;

    Vector2 currentMovementInput;
    Vector3 currentMovement;
    SwipeInput inputManager;
    bool isMovePressed;
    Animator animator;
    [SerializeField]float moveSpeed=7;
    float rotationFactorPerFrame = 15f;
    public bool isJumpPressed;

    public float gravity = -9.8f;
    float groundedGravity = -.05f;

    float initialJumpVelocity;
    [SerializeField]float maxJumpHeight=0.85f;
    [SerializeField]float maxJumpTime=0.85f;
    bool isJumping=false;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        characterController= GetComponent<CharacterController>();
        inputManager= GetComponent<SwipeInput>();
        SetUpJumpVariables();
       
    }


    // Update is called once per frame
    void Update()
    {
        HandleInput();
        handleAnimation();
        handleRotation();
        //Debug.Log(characterController.isGrounded);
        characterController.Move(currentMovement * Time.deltaTime * moveSpeed);
        handleGravity();
        handleJump();
        
    }

    public void HandleInput()
    {
        inputManager.handleInput();
        currentMovementInput = new Vector2(inputManager.moveDir, 0);
        currentMovement.x = currentMovementInput.x;
        isMovePressed = currentMovementInput.x != 0;

        if (inputManager.up)
        {
            isJumpPressed = true;
        }
        if (!inputManager.up)
        {
            isJumpPressed = false;
        }

        
    }

    void handleAnimation()
    {

        animator.SetFloat("Speed", Mathf.Abs(currentMovementInput.x));
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
            transform.rotation= Quaternion.Slerp(currentRotation, targetRotation,rotationFactorPerFrame);
        }
    }

    void handleGravity()
    {
        bool isFalling = currentMovement.y <= 0||!isJumpPressed;
        float fallMultiplier = 2f;

        if (characterController.isGrounded)
        {
            animator.SetBool("Jump", false);
            currentMovement.y = groundedGravity;

        }else if (isFalling)
        {
            
            float previousYVelocity = currentMovement.y;
            float newYVelocity = currentMovement.y + (gravity * fallMultiplier*Time.deltaTime);
            float nextYVelocity = Mathf.Max((previousYVelocity + newYVelocity) * 0.5f,-20f);
            currentMovement.y = nextYVelocity;
        }
        else
        {
            float previousYVelocity=currentMovement.y;
            float newYVelocity = currentMovement.y + (gravity * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * 0.5f;
            currentMovement.y = nextYVelocity;
        }
    }

    void SetUpJumpVariables()
    {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }

    void handleJump()
    {
        if(!isJumping && characterController.isGrounded&& isJumpPressed)
        {
            animator.SetBool("Jump", true);
            isJumping = true;
            currentMovement.y =initialJumpVelocity*0.5f;

        }else if (!isJumpPressed && isJumping && characterController.isGrounded)
        {
            isJumping=false;
        }
    }
}
