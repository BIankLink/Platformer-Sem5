using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public static List<GameObject> bullets;
    private void Start()
    {
        bullets = new List<GameObject>();
    }
    public static GameObject GetBulletFromPool()
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            if (!bullets[i].activeInHierarchy)
            {
                var b = bullets[i].GetComponent<Projectile>();
                b.timer = b.lifeTime;
                bullets[i].SetActive(true);
                return bullets[i];
            }
        }
        return null;
    }
    public static GameObject GetBulletFromPoolWithType(string type)
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            if (!bullets[i].activeInHierarchy && bullets[i].GetComponent<Projectile>().type == type)
                return bullets[i];
        }
        return null;
    }
}
