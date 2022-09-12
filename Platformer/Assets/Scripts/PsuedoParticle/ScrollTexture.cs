using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollTexture : MonoBehaviour
{
    public Material scrollableMaterial;
    public Vector2 direction= new Vector2(1,0);
    public float speed=5f;

    Vector2 currentOffset;

    private void Start()
    {
        currentOffset = GetComponent<Renderer>().material.mainTextureOffset;
    }

    private void Update()
    {
        //currentOffset += direction*Mathf.Sin(Time.time)*Time.deltaTime;
        currentOffset+= direction * speed * Time.deltaTime;
        GetComponent<Renderer>().material.mainTextureOffset=currentOffset;
    }
}
