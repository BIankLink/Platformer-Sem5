using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerData 
{
    public float lives;
    //public float[] position;
    //public float[] rotation;
    public Vector3 position;
    public Quaternion rotation;
    public bool firstBlood;

 

    public PlayerData(PlayerStateMachine player)
    {
        lives = player.health;
        position = player.transform.position;
        rotation = player.transform.rotation;
        firstBlood = GameManager.instance.onlyOnce;
        //position = new float[3];
        //position[0] = player.transform.position.x;
        //position[1] = player.transform.position.y;
        //position[2] = player.transform.position.z;
        
        //rotation = new float[3];
        //rotation[0] = player.transform.rotation.x;
        //rotation[1] = player.transform.rotation.y;
        //rotation[2] = player.transform.rotation.z;

        
    }

}
