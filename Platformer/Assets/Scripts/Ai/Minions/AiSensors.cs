using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AiSensors : MonoBehaviour
{
    public float distance = 10f;
    public float angle = 30f;
    public float height = 1.0f;
    public Color meshColor=Color.yellow;
    [SerializeField]Vector3[] vertices;
    [SerializeField]int[] triangles;
    //Mesh mesh;
    MeshFilter meshF;
    [SerializeField]MeshCollider colider;
    public Material material;
    // Start is called before the first frame update
    void Start()
    {
        meshF = GetComponent<MeshFilter>();
        colider= meshF.gameObject.AddComponent<MeshCollider>();
        meshF.mesh = CreateWedgeMesh();
        //colider.sharedMesh = meshF.mesh;
        //colider.isTrigger = true;
        //colider.convex = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Mesh CreateWedgeMesh()
    {
        Mesh mesh = new Mesh();
        colider = new MeshCollider();
        int segments = 10;
        int numTriagles = (segments*4)+2+2;
        int numVertices = numTriagles * 3;

        vertices = new Vector3[numVertices];
        triangles= new int[numVertices];

        Vector3 bottomCenter = Vector3.zero;
        Vector3 bottomLeft = Quaternion.Euler(0, -angle, 0) * Vector3.forward * distance;
        Vector3 bottomRight = Quaternion.Euler(0, angle, 0) * Vector3.forward * distance;

        Vector3 topCenter = bottomCenter ;
        Vector3 topLeft = bottomLeft + Vector3.up * height;
        Vector3 topRight = bottomRight + Vector3.up * height;

        int vert = 0;

        // left side
        vertices[vert++] = bottomCenter;
        vertices[vert++] = bottomLeft;
        vertices[vert++] = topLeft;

        vertices[vert++] = topLeft;
        vertices[vert++] = topCenter;
        vertices[vert++] = bottomCenter;


        //Right side
        vertices[vert++] = bottomCenter;
        vertices[vert++] = topCenter;
        vertices[vert++] = topRight;

        vertices[vert++] = topRight;
        vertices[vert++] = bottomRight;
        vertices[vert++] = bottomCenter;

        float currentAngle = -angle;
        float deltaAngle = (angle * 2) / segments;

        for(int i = 0; i < segments; i++)
        {
           
            
            bottomLeft = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * distance;
            bottomRight = Quaternion.Euler(0, currentAngle+ deltaAngle, 0) * Vector3.forward * distance;
           
            
            topLeft = bottomLeft + Vector3.up * height;
            topRight = bottomRight + Vector3.up * height;

            // far side
            vertices[vert++] = bottomLeft;
            vertices[vert++] = bottomRight;
            vertices[vert++] = topRight;

            vertices[vert++] = topRight;
            vertices[vert++] = topLeft;
            vertices[vert++] = bottomLeft;
            //top side
            vertices[vert++] = topCenter;
            vertices[vert++] = topLeft;
            vertices[vert++] = topRight;
            //bottom
            vertices[vert++] = bottomCenter;
            vertices[vert++] = bottomRight;
            vertices[vert++] = bottomLeft;


            currentAngle += deltaAngle;
        }
              
        for(int i = 0; i < numVertices; i++)
        {
            triangles[i] = i;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        
        GetComponent<MeshRenderer>().material = material;
        meshF.mesh = mesh;
        
        return mesh;
    }

    private void OnValidate()
    {
        
    }

    private void OnDrawGizmos()
    {
        if (meshF)
        {
            Gizmos.color = meshColor;
            Gizmos.DrawMesh(meshF.mesh,transform.position,transform.rotation);
        }
    }

}
