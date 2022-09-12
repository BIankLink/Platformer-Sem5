using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveMachine : MonoBehaviour
{
    public PlayerStateMachine player;
   
    
    public void mysave()
    {
        PlayerData p = new PlayerData(player);
       
        //put values in p
        SaveData s = new SaveData();
        s.playerData = p;
       
        
        SerializationManager.Save("Save", s);
       
      


    }
   
    
}
