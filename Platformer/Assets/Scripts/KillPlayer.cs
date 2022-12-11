using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
            
        if (other.CompareTag("Player"))
        {
            
            other.GetComponent<PlayerStateMachine>().TakeDamage(other.GetComponent<PlayerStateMachine>().health);
        }
    }
}
