using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class MovingPlatform : MonoBehaviour
{
    public Vector2[] wayPoints;

    public float speed= 10;
    public int current;
    public float lookRadius = 0.5f;

    Vector3 previousPosition;
    public Vector3 lastMoveDirection;
    private void Start()
    {
        previousPosition = transform.position;
        lastMoveDirection = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(wayPoints[current], transform.position) < lookRadius)
        {
            current++;
            if (current >= wayPoints.Length)
            {
                current = 0;
            }
        }
        
        transform.position = Vector3.MoveTowards(transform.position, wayPoints[current], Time.deltaTime * speed);
        

        
    }
    private void FixedUpdate()
    {
        if (transform.position != previousPosition)
        {
            lastMoveDirection = (transform.position - previousPosition).normalized;
            previousPosition = transform.position;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(wayPoints[0], wayPoints[1]);
    }



}
