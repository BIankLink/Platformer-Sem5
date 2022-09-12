using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveTrigger : MonoBehaviour
{
   
    [SerializeField]SaveMachine save;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            save.mysave();
        }
    }
   
}
