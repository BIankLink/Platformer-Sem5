using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class Projectile : MonoBehaviour
{
    public float speed;
    public int maxBounces;
    [SerializeField]int currentBounces=0;
    public bool hasLifeTime;
    public float timer;
    public float lifeTime;
    public string type;
    public float rotationtobe;
    Vector3 moveDirection;
    
    private void Start()
    {
        timer = lifeTime;
        transform.rotation = Quaternion.Euler(0, 0, rotationtobe);
        
    }
    private void OnEnable()
    {
        transform.rotation = Quaternion.Euler(0, 0, rotationtobe);
    }

    private void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);

        if (hasLifeTime)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                
                gameObject.SetActive(false);
            }
        }
        
    }
    public void SetMoveDirection(Vector3 dir)
    {
        moveDirection = dir;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "BossWall" && currentBounces<= maxBounces)
        {

            ContactPoint point = collision.contacts[0];
            Vector3 newDir = Vector3.zero;
            Vector3 curDire = this.transform.TransformDirection(moveDirection);
            currentBounces++;
            newDir = Vector3.Reflect(curDire, point.normal);         
            transform.rotation = Quaternion.FromToRotation(moveDirection, newDir);

        }
        if (currentBounces > maxBounces)
        {
            gameObject.SetActive(false);
        }
    }
}
