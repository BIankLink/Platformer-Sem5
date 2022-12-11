using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] PlayerStateMachine player;
    [SerializeField] Image fill;
    private void Start()
    {
        player = GameManager.instance.player;
        
        
    }
    private void Update()
    {
        if (player != null)
        {
            if (player.health != player.startingHealth)
            {
                fill.fillAmount = player.health / player.startingHealth;
            }
        }
        if (player == null && GameManager.instance.player!= null)
        {
            player = GameManager.instance.player;
            fill.fillAmount = player.health / player.startingHealth;
        }
    }
}
