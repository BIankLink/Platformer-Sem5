using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GlobalEnums;

public class SaveTrigger : MonoBehaviour
{
   
    [SerializeField]SaveMachine save;
    public Levels level;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
                save.mysave();
            
        }
    }
   
}
