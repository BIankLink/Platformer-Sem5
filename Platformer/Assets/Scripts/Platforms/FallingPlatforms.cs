using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatforms : MonoBehaviour
{
    Vector3 StartPos;
    Quaternion StartRot;
    Rigidbody rb;
    public float duration = 2f;
    public float breakduration = 1f;
    private void Start()
    {
        StartPos = transform.position;
        StartRot = transform.rotation;
        rb = GetComponent<Rigidbody>();
    }
    

    public void Fall()
    {
        StartCoroutine(Cracking());
    }

    IEnumerator Cracking()
    {
        yield return new WaitForSeconds(breakduration);
        //trigger animation
        rb.constraints= RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.constraints = RigidbodyConstraints.FreezePositionX;
        rb.constraints = RigidbodyConstraints.FreezePositionZ;
        rb.useGravity = true;

        yield return new WaitForSeconds(duration);
        rb.useGravity = false;
        transform.position = StartPos;
        transform.rotation = StartRot;
        rb.constraints = RigidbodyConstraints.FreezeAll;

    }
}
