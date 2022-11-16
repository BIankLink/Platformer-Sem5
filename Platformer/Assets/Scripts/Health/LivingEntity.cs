using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour,IDamageable
{
    public float startingHealth;
    public float health { get;  set; }
    protected bool dead;
    public float slowDuration=1f;
    public event System.Action OnDeath;

    protected virtual void Start()
    {
        health = startingHealth;
    }

    public virtual void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        // Do some stuff here with hit var
        TakeDamage(damage);
    }

    public virtual void TakeDamage(float damage)
    {
        
        health -= damage;
        if (gameObject.CompareTag("Player"))
        {
            Debug.Log("Player  " + health);
            
            //gameObject.GetComponent<PlayerStateMachine>().CanSwitch=false;
        }
        if (health <= 0 && !dead)
        {
            Die();
        }
    }

    

    [ContextMenu("Self Destruct")]
    public virtual void Die()
    {
        dead = true;
        if (OnDeath != null)
        {
            OnDeath();
        }
        GameObject.Destroy(gameObject);
    }
}
