using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackedParts : MonoBehaviour
{
    [SerializeField] float timeToDissapear;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,timeToDissapear);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
