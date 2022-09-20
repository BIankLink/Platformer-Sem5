using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public int maxBounces;
    int currentBounces=0;
    public bool hasLifeTime;
    public float timer;
    public float lifeTime;
    public string type;
    public float rotation;

    private void OnEnable()
    {
        timer = lifeTime;
        transform.rotation = Quaternion.Euler(rotation, 0,0);
    }
    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        if (hasLifeTime)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                
                gameObject.SetActive(false);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "BossWall" && currentBounces<= maxBounces)
        {
            ContactPoint point = collision.contacts[0];
            Vector3 newDir = Vector3.zero;
            Vector3 curDire = this.transform.TransformDirection(Vector3.forward);
            currentBounces++;
            newDir = Vector3.Reflect(curDire, point.normal);
            transform.rotation = Quaternion.FromToRotation(Vector3.forward, newDir);
        }
        if (currentBounces > maxBounces)
        {
            gameObject.SetActive(false);
        }
    }
}
