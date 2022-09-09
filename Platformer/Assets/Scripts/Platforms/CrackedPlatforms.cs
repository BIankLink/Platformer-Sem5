using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackedPlatforms : MonoBehaviour
{
    public float duration = 2f;
    public float breakduration = 1f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.position.y > transform.position.y || collision.transform.position.y < transform.position.y)
        {
            StartCoroutine(Cracking());
        }
        
    }



    IEnumerator Cracking()
    {
        yield return new WaitForSeconds(breakduration);
        //trigger animation
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(duration);

        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<Collider>().enabled = true;

    }
}
