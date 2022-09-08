using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class SwipeInput : MonoBehaviour
{
    PlayerStateMachine player;
    public Vector2 startTouchPosition;
    public Vector2 currentTouchPosition;
    public int pixelDistToDetect = 50;
    [SerializeField]Joystick joystick;
    float tapRange = 10;
    public bool fingerDown;
    float screenWidth;
    float ScreenWidthHalf;
    [SerializeField]public float moveDir;
    public bool up, down, left, right,moveLeft,moveRight,wallRunningInput=false;
    float timer=0f;
    bool attacking;
    float tapCounter;
    float startTime;
    float endTime;
    [SerializeField]float SwitchCoolDown = 1;
    float switchTimer;
    [SerializeField] float startTimer=.25f;
    Touch touch;
    // Start is called before the first frame update
    void Start()
    {
        screenWidth = Screen.width;
        ScreenWidthHalf = screenWidth / 2;
        player = GetComponent<PlayerStateMachine>();
    }


    IEnumerator uponce()
    {
        yield return new WaitForSeconds(0.05f);
        up = false;
    } 
    IEnumerator leftOnce()
    {
        yield return new WaitForSeconds(0.1f);
        left = false;
    }IEnumerator rightOnce()
    {
        yield return new WaitForSeconds(0.1f);
        right = false;
    }IEnumerator Attacking()
    {
        yield return new WaitForSeconds(2f);
        attacking = false;
    }

    public void handleInput()
    {
        Touch[] myTouches = Input.touches;
        moveDir = joystick.Horizontal;
        if (switchTimer <=SwitchCoolDown)
        {
            
            switchTimer += Time.deltaTime;
        }


        for (int i = 0; i < myTouches.Length; i++)
        {
            //if (Input.GetTouch(i).tapCount == 2)
            //{
                
            //    if (wallRunningInput)
            //    {
            //        wallRunningInput = false;
                    
            //    }
            //    else
            //    {
            //        wallRunningInput = true;
                    
            //    }
                
            //}

            timer += Time.deltaTime;


            if (i >= 0 && myTouches[i].phase == TouchPhase.Began && i <= 1)
            {
                //if (upOnce)
                //{
                //    up = false;

                //}

                startTime = Time.time;



                startTouchPosition = myTouches[i].position;
                // Debug.Log("else "+startPos);

                currentTouchPosition = myTouches[i].position;

                fingerDown = true;

            }


            if (i >= 0 && myTouches[i].phase == TouchPhase.Moved && i <= 2)
            {
               currentTouchPosition=myTouches[i].position;
                if (currentTouchPosition.x > screenWidth / 2)
                {
                    CheckSwipe();
                }
            }

            //if (i >= 0 && myTouches[i].phase == TouchPhase.Stationary)
            //{
            //    if (!attacking)
            //    {
            //        if (timer >= startTimer)
            //        {
            //            if (fingerDown)
            //            {
            //                currentTouchPosition = myTouches[i].position;
            //                Vector2 distance = currentTouchPosition - startTouchPosition;
            //                if (Mathf.Abs(distance.x) < tapRange && Mathf.Abs(distance.y) < tapRange)
            //                {
            //                    if(currentTouchPosition.x < screenWidth / 2)
            //                    {
            //                        if (currentTouchPosition.x < ScreenWidthHalf/2)
            //                        {

            //                            if (!moveRight)
            //                            {

            //                                //Debug.Log("moveLeft");
            //                                moveLeft = true;
            //                                //fingerDown = true;
            //                                if (moveDir >= -1)
            //                                {
            //                                    moveDir -= 0.1f;
            //                                }

            //                            }
            //                        }
            //                        else if (currentTouchPosition.x > ScreenWidthHalf / 2)
            //                        {

            //                            if (!moveLeft)
            //                            {

            //                                //Debug.Log("moveRight");
            //                                moveRight = true;
            //                                //fingerDown=true;
            //                                if (moveDir <= 1)
            //                                {
            //                                    moveDir += 0.1f;
            //                                }

            //                            }
            //                        }
            //                    }
                                
            //                }
            //            }

            //        }
            //    }

           // }
            if (i >= 0 && myTouches[i].phase == TouchPhase.Ended)
            {
                if (i == 0)
                {
                    timer = 0;
                }
                endTime = Time.time;

                up = false;
                down = false;
                left = false;
                right = false;
                fingerDown = false;
                StartCoroutine(Attacking());
                if (timer == 0)
                {
                    moveLeft = false;
                    moveRight = false;
                    moveDir = 0;
                }

            }

            if (endTime - startTime < 0.2f && endTime - startTime > 0f)
            {
                tapCounter++;
                endTime = 0f;
                startTime = 0f;
                StartCoroutine(Countdown());
            }

            if (tapCounter == 2)
            {
                
                   //if(startTouchPosition.x < screenWidth / 2)
                   //{
                   //     Debug.Log("right");
                   //     left = true;
                   //     attacking = true;
                   //     if (!wallRunningInput)
                   //     {
                   //         moveRight = false;
                   //         moveLeft = false;
                   //         moveDir = 0;
                   //     }
                   //     StartCoroutine(leftOnce());
                   //     fingerDown = false;

                   //}
                   //else if(startTouchPosition.x > screenWidth / 2)
                   //{
                   //     Debug.Log("right");
                   //     right = true;
                   //     attacking = true;
                   //     if (!wallRunningInput)
                   //     {
                   //         moveRight = false;
                   //         moveLeft = false;
                   //         moveDir = 0;
                   //     }
                   //     StartCoroutine(rightOnce());
                   //     fingerDown = false;
                   //}


                        //if (wallRunningInput)
                        //{
                        //    switchTimer = 0f;
                        //    Debug.Log("DoubleTap1");
                        //    wallRunningInput = false;

                        //}
                        //else
                        //{
                        //    switchTimer = 0f;
                        //    Debug.Log("DoubleTap2");
                        //    wallRunningInput = true;

                        //}
                
            }

        }
     

    }
    private IEnumerator Countdown()
    {
        yield return new WaitForSeconds(0.5f);
        tapCounter = 0;
    }

    bool SwipeDistanceCheckMet()
    {
        return VerticalMovementDistance() > pixelDistToDetect || HorizontalMovementDistance() > pixelDistToDetect;
    }

    float VerticalMovementDistance()
    {
        return Mathf.Abs(currentTouchPosition.y - startTouchPosition.y);
    }
    float HorizontalMovementDistance()
    {
        return Mathf.Abs(currentTouchPosition.x - startTouchPosition.x);
    }

    bool IsVerticalSwipe()
    {
        return VerticalMovementDistance() > HorizontalMovementDistance();
    }

    void CheckSwipe()
    {
        if (fingerDown)
        {
            if (SwipeDistanceCheckMet())
            {
                if (IsVerticalSwipe())
                {


                    if (currentTouchPosition.y - startTouchPosition.y > 0)
                    {
                        Debug.Log("up");
                        up = true;
                        if (player.CanSwitch)
                        {
                            wallRunningInput = false;
                        }

                        StartCoroutine(uponce());
                        fingerDown = false;

                    }
                    else if (currentTouchPosition.y - startTouchPosition.y < 0)
                    {
                        Debug.Log("down");
                        down = true;
                        if (player.CanSwitch)
                        {
                            wallRunningInput = true;
                        }
                        fingerDown = false;
                    }
                }
                else
                {
                    if (currentTouchPosition.x - startTouchPosition.x > 0)
                    {
                        right = true;
                        attacking = true;
                        if (!wallRunningInput)
                        {
                            moveRight = false;
                            moveLeft = false;
                            moveDir = 0;
                        }
                        StartCoroutine(rightOnce());
                        fingerDown = false;
                        //Debug.Log("right");
                    }
                    else if (currentTouchPosition.x - startTouchPosition.x < 0)
                    {
                        left = true;
                        attacking = true;
                        if (!wallRunningInput)
                        {
                            moveRight = false;
                            moveLeft = false;
                            moveDir = 0;
                        }
                        StartCoroutine(leftOnce());
                        fingerDown = false;
                        ////Debug.Log("left");
                    }

                }

            }
        }
    }


    //void DashLeft()
    //{
    //    left = true;
    //    attacking = true;
    //    if (!wallRunningInput)
    //    {
    //        moveRight = false;
    //        moveLeft = false;
    //        moveDir = 0;
    //    }
    //    StartCoroutine(leftOnce());
    //    fingerDown = false;
    //    Debug.Log("left");
    //}
    //void DashRight()
    //{
    //    right = true;
    //    attacking = true;
    //    if (!wallRunningInput)
    //    {
    //        moveRight = false;
    //        moveLeft = false;
    //        moveDir = 0;
    //    }
    //    StartCoroutine(rightOnce());
    //    fingerDown = false;
    //    Debug.Log("left");
    //}
}
