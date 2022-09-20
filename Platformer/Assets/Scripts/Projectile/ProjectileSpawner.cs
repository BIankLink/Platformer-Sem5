using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public ProjectileSpawnData[] spawnDatas;
    public int index;
    public bool isSequenceRandom;
    public bool spawnAutomatically;
    public GameObject weapon;

    [HideInInspector] public ProjectileSpawnData GetSpawnData()
    {
        return spawnDatas[index];
    }
    [HideInInspector] public float timer;
    /*[HideInInspector]*/ public float[] rotations;

    private void Start()
    {
        timer = GetSpawnData().cooldown;
    }
    private void Update()
    {
        if (spawnAutomatically)
        {
            if(timer <= 0)
            {
                SpawnBullets();
                timer = GetSpawnData().cooldown;

                rotations = new float[GetSpawnData().numberOfBullets];

            }
            timer -= Time.deltaTime;
        }
    }
    public float[] RandomRotations()
    {
        for(int i = 0; i < GetSpawnData().numberOfBullets; i++)
        {
            rotations[i] = Random.Range(GetSpawnData().minRotation, GetSpawnData().maxRotation);
        }
        return rotations;
    }
    public float[] DistributedRotations()
    {
        for(int i = 0; i < GetSpawnData().numberOfBullets; i++)
        {
            float fraction;
            if (GetSpawnData().numberOfBullets <= 1)
            {
                fraction = (float)i / ((float)GetSpawnData().numberOfBullets);
            }
            else
            {
                fraction = (float)i / ((float)GetSpawnData().numberOfBullets - 1);
            }

            var difference = GetSpawnData().maxRotation - GetSpawnData().minRotation;
            var fractionOfDifference = fraction * difference;
            rotations[i] = fractionOfDifference + GetSpawnData().minRotation;
        }

        return rotations;
    }
    public GameObject[] SpawnBullets()
    {
        rotations = new float[GetSpawnData().numberOfBullets];
        if (GetSpawnData().isRandom)
        {
            RandomRotations();
        }
        else
        {
            DistributedRotations();
        }

        GameObject[] spawnedBullets = new GameObject[GetSpawnData().numberOfBullets];
        for (int i = 0; i < GetSpawnData().numberOfBullets; i++)
        {
            spawnedBullets[i] = ProjectileManager.GetBulletFromPool();

            if (spawnedBullets[i] == null)
            {
                spawnedBullets[i] = Instantiate(GetSpawnData().bulletResource, transform.position, weapon.transform.rotation);
                ProjectileManager.bullets.Add(spawnedBullets[i]);
            }
            else
            {
                spawnedBullets[i].transform.SetParent(transform);
                //spawnedBullets[i].transform.rotation = Quaternion.Euler(weapon.transform.rotation.x, weapon.transform.rotation.y, rotations[i]);
                spawnedBullets[i].transform.rotation = Quaternion.Euler(rotations[i], weapon.transform.rotation.y, weapon.transform.rotation.z);
                spawnedBullets[i].transform.localPosition = Vector3.zero;
            }

            var b = spawnedBullets[i].GetComponent<Projectile>();
            b.rotation = rotations[i];
            b.speed = GetSpawnData().bulletSpeed;
            b.hasLifeTime = GetSpawnData().hasLifeTime;
            b.lifeTime = GetSpawnData().lifetime;
            b.maxBounces = GetSpawnData().maxBounces;
            if (!GetSpawnData().isParent)
            {
                spawnedBullets[i].transform.SetParent(null);
            }
        }
        return spawnedBullets;
    }
}
