using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Joystick joystick;
    public Vector2 moveDir;

    public bool left;
    public bool right;
    public bool wallRunningInput;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    
    public void handleInput()
    {
        moveDir.x = joystick.Horizontal;
        moveDir.y = joystick.Vertical;
    }
}
