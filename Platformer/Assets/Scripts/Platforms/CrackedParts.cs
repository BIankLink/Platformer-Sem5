using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackedParts : MonoBehaviour
{
    [SerializeField] float timeToDissapear;
    Rigidbody rb;
    [SerializeField] float force;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject,timeToDissapear);
    }

    private void OnEnable()
    {
        if (rb != null)
        {
            rb.AddExplosionForce(force, transform.position, 5f);
        }
    }
}
