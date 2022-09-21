using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectiles : MonoBehaviour
{
    [SerializeField]
    int projectileCount = 5;

    [SerializeField]
    float startAngle = 0f, endAngle = 270f;

    Vector3 projectileMoveDirection;

    // Start is called before the first frame update
    void Start()
    {
        Fire();
    }

    void Fire()
    {
        float angleStep = (endAngle - startAngle) / projectileCount;
        float angle = startAngle;

        for(int i = 0; i < projectileCount+1; i++)
        {
            float proDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float proDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 proMoveVector = new Vector3(proDirX, proDirY, 0f);
            Vector3 proDir = (proMoveVector - transform.position).normalized;

            GameObject pro = ProjectilePool.instance.GetProjectile();
            pro.transform.position = transform.position;
            pro.transform.rotation = transform.rotation;
            pro.SetActive(true);
            pro.GetComponent<Projectile>().SetMoveDirection(proDir);

            angle += angleStep;
        }
    }
   
}
