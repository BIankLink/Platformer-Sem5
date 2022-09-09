using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatforms : MonoBehaviour
{
    Vector3 StartPos;
    Rigidbody rb;
    public float duration = 2f;
    public float breakduration = 1f;
    private void Start()
    {
        StartPos = transform.position;  
        rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Cracking());
        }
        

    }



    IEnumerator Cracking()
    {
        yield return new WaitForSeconds(breakduration);
        //trigger animation
        rb.useGravity = true;

        yield return new WaitForSeconds(duration);
        transform.position = StartPos;
        rb.useGravity = false;

    }
}
