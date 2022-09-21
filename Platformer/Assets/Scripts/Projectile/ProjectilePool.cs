using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public static ProjectilePool instance;

    [SerializeField] GameObject pooledProjectile;
    private bool notEnoughProjectileInPool = true;

    public List<GameObject> projectiles;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        projectiles= new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public GameObject GetProjectile()
    {
        if (projectiles.Count > 0)
        {
            for(int i =0; i < projectiles.Count; i++)
            {
                if (projectiles[i].activeInHierarchy)
                {
                    return projectiles[i];
                }
            }
        }
        if (notEnoughProjectileInPool)
        {
            GameObject pro = Instantiate(pooledProjectile);
            pro.SetActive(false);
            projectiles.Add(pro);
            return pro;
        }
        return null;
    }
}
