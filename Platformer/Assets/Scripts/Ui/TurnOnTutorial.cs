using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnTutorial : MonoBehaviour
{
    public GameObject tutorailSprite;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            tutorailSprite.SetActive(true);

        }
    }
}
