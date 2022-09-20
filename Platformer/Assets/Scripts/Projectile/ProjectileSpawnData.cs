using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Projectile/ProjectileSpawnData", order = 1)]
public class ProjectileSpawnData : ScriptableObject
{
    public string nameType;
    public GameObject bulletResource;
    public float minRotation;
    public float maxRotation;
    public int numberOfBullets;
    public bool isRandom;
    public bool isParent = true;
    public float cooldown;
    public float bulletSpeed;
    public int maxBounces;
    public bool hasLifeTime;
 
    public float lifetime;
    

}

