using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveTrigger : MonoBehaviour
{
   
    [SerializeField]SaveMachine save;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            save.mysave();
        }
    }
   
}
