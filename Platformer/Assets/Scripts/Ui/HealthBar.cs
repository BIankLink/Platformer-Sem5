using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] PlayerStateMachine player;
    [SerializeField]public Image fill;
    private void Start()
    {
        player = GameManager.instance.player;

        fill.fillAmount = player.health / player.startingHealth;
    }
    private void Update()
    {
        if (player != null)
        {
            fill.fillAmount = player.health / player.startingHealth;
        }
    }
}
