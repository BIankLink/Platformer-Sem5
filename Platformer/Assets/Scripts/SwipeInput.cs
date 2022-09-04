using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class SwipeInput : MonoBehaviour
{

    public Vector2 startTouchPosition;
    public Vector2 currentTouchPosition;
    public int pixelDistToDetect = 50;
    float tapRange = 10;
    public bool fingerDown;
    float screenWidth;
    [SerializeField]public float moveDir;
    public bool up, down, left, right,moveLeft,moveRight,wallRunningInput=false;
    float timer=0f;
    bool attacking;
    float tapCounter;
    float startTime;
    float endTime;
    [SerializeField] float startTimer=.25f;
    Touch touch;
    // Start is called before the first frame update
    void Start()
    {
        screenWidth = Screen.width;
    }


    IEnumerator uponce()
    {
        yield return new WaitForSeconds(0.2f);
        up = false;
    } 
    IEnumerator leftOnce()
    {
        yield return new WaitForSeconds(0.2f);
        left = false;
    }IEnumerator rightOnce()
    {
        yield return new WaitForSeconds(0.2f);
        right = false;
    }IEnumerator Attacking()
    {
        yield return new WaitForSeconds(2f);
        attacking = false;
    }

    public void handleInput()
    {
        Touch[] myTouches = Input.touches;




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



                fingerDown = true;

            }


            if (i >= 0 && myTouches[i].phase == TouchPhase.Moved && i <= 2)
            {
                currentTouchPosition = myTouches[i].position;
                Vector2 distance = currentTouchPosition - startTouchPosition;

                if (fingerDown)
                {
                    if (distance.y > pixelDistToDetect)
                    {
                        Debug.Log("up");
                        up = true;

                        StartCoroutine(uponce());
                        fingerDown = false;

                    }
                    else if (distance.y < -pixelDistToDetect)
                    {
                        Debug.Log("down");
                        down = true;
                        fingerDown = false;

                    }
                    if (distance.x < -pixelDistToDetect && Mathf.Abs(distance.x) > Mathf.Abs(distance.y) + 300)
                    {
                        left = true;
                        attacking = true;
                        moveLeft = false;
                        moveRight = false;
                        moveDir = 0;
                        StartCoroutine(leftOnce());
                        fingerDown = false;
                        Debug.Log("left");
                    }
                    else if (distance.x > pixelDistToDetect && Mathf.Abs(distance.x) > Mathf.Abs(distance.y) + 300)
                    {
                        right = true;
                        attacking = true;
                        moveRight = false;
                        moveLeft = false;
                        moveDir = 0;
                        StartCoroutine(rightOnce());
                        fingerDown = false;
                        //Debug.Log("right");
                    }
                }


            }
            if (i >= 0 && myTouches[i].phase == TouchPhase.Stationary)
            {
                if (!attacking)
                {
                    if (timer >= startTimer)
                    {
                        if (fingerDown)
                        {
                            currentTouchPosition = myTouches[i].position;
                            Vector2 distance = currentTouchPosition - startTouchPosition;
                            if (Mathf.Abs(distance.x) < tapRange && Mathf.Abs(distance.y) < tapRange)
                            {
                                if (currentTouchPosition.x < screenWidth / 2)
                                {

                                    if (!moveRight)
                                    {

                                        //Debug.Log("moveLeft");
                                        moveLeft = true;
                                        //fingerDown = true;
                                        if (moveDir >= -1)
                                        {
                                            moveDir -= 0.1f;
                                        }

                                    }
                                }
                                else if (currentTouchPosition.x > screenWidth / 2)
                                {

                                    if (!moveLeft)
                                    {

                                        //Debug.Log("moveRight");
                                        moveRight = true;
                                        //fingerDown=true;
                                        if (moveDir <= 1)
                                        {
                                            moveDir += 0.1f;
                                        }

                                    }
                                }
                            }
                        }

                    }
                }

            }
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
                if (wallRunningInput)
                {
                    Debug.Log("DoubleTap1");
                    wallRunningInput = false;

                }
                else
                {
                    Debug.Log("DoubleTap2");
                    wallRunningInput = true;

                }
            }

        }
     

    }
    private IEnumerator Countdown()
    {
        yield return new WaitForSeconds(0.7f);
        tapCounter = 0;
    }

}
