using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCutscene : MonoBehaviour
{
    [SerializeField]GameObject splashArt;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            splashArt.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
