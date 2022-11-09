using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveMachine : MonoBehaviour
{
    
   
    
    public void mysave()
    {
        PlayerData p = new PlayerData(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStateMachine>());

         //put values in p
            SaveData s = new SaveData();
            s.playerData = p;
        
            SerializationManager.Save("Save", s);

        Debug.Log(p.position);
        
    }
   
    
}
