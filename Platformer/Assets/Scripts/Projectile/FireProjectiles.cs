using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type{Bouncy};
public class FireProjectiles : MonoBehaviour
{
    [SerializeField]
    int projectileCount = 5;

    [SerializeField]bool isRandom;
    [SerializeField]
    float startAngle = 0f, endAngle = -90f;
    public Type type;
    Vector3 projectileMoveDirection;
    /*[HideInInspector]*/ public float[] rotations;
    // Start is called before the first frame update
    void Start()
    {
        Fire();
    }
    public float[] RandomRotations()
    {
        for (int i = 0; i <projectileCount; i++)
        {
            rotations[i] = Random.Range(startAngle, endAngle);
        }
        return rotations;

    }

    // This will set random rotations evenly distributed between the min and max Rotation.
    public float[] DistributedRotations()
    {
        for (int i = 0; i < projectileCount; i++)
        {
            float fraction;
            if (projectileCount <= 1)
            {
                fraction = (float)i / ((float)projectileCount);
            }
            else
            {
                fraction = (float)i / ((float)projectileCount - 1);
            }

            var difference = endAngle - startAngle;
            var fractionOfDifference = fraction * difference;
            rotations[i] = fractionOfDifference + startAngle; // We add minRotation to undo Difference

        }

        return rotations;
    }
    void Fire()
    {
        rotations = new float[projectileCount];
        if (isRandom)
        {
            RandomRotations();
        }
        else
        {
            DistributedRotations();
        }

        for (int i = 0; i < projectileCount; i++)
        {
            
            GameObject pro = ProjectilePool.instance.SpwanFromPool(type.ToString(), transform.position,transform.rotation);
            pro.GetComponent<Projectile>().SetMoveDirection(gameObject.transform.forward);
            
            pro.GetComponent<Projectile>().rotationtobe = rotations[i];
            
        }
    }
   
}
