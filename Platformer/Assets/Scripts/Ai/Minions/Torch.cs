using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;


public class Torch : MonoBehaviour
{
    public Light lightS;
    public FieldOfView pov;
    [Range(0,100)]
    public float intensity;
    public Color color;
    // Start is called before the first frame update
    void Start()
    {
        lightS = GetComponent<Light>();
        
    }


    // Update is called once per frame
    void Update()
    {
        lightS.color = color;
        lightS.range = pov.viewRadius;
        lightS.spotAngle = pov.viewAngle;
        lightS.intensity = intensity;
    }
}
