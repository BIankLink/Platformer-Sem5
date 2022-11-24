using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TutorialSprites{up,LeftRight,down,move }
public class TutorialDetection : MonoBehaviour
{
    SwipeInput swipeInput;
    public TutorialSprites current;
    void Start()
    {
        swipeInput = FindObjectOfType<SwipeInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if(swipeInput != null)
        {
            if (current == TutorialSprites.up)
            {
                if (swipeInput.up)
                {
                    transform.parent.gameObject.SetActive(false);
                }
            }
            if(current == TutorialSprites.LeftRight)
            {
                if (swipeInput.left || swipeInput.right)
                {
                    transform.parent.gameObject.SetActive(false);
                }
            }
            if (current == TutorialSprites.down)
            {
                if (swipeInput.down)
                {
                    transform.parent.gameObject.SetActive(false);
                }
            }
            if(current == TutorialSprites.move)
            {
                if(swipeInput.moveDir > 0 || swipeInput.moveDir < 0)
                {
                    transform.parent.gameObject.SetActive(false);
                }
            }
            
        }
    }
}
