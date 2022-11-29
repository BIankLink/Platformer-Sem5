using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackedParts : MonoBehaviour
{
    [SerializeField] float timeToDissapear=5f;
    
    [SerializeField]
    GameObject[] parts; 
    [SerializeField] float force=50f;
    [SerializeField] float radius=5f;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject part in parts)
        {
            Rigidbody rb = part.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(force, transform.position, radius, 3.0F);
        }
        Destroy(gameObject,timeToDissapear);
    }

   
}
