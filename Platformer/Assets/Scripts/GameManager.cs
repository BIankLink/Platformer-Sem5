using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singelton
    public static GameManager instance;
    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);

            
            return;
        }
        instance = this;
        

    }
    #endregion
    [SerializeField] GameObject Player;

    public PlayerStateMachine player;
   
    //public Button Load;

   
   // public GameObject deathPanel;
   // public GameObject instructions;

    private void Start()
    {
        player = Player.GetComponent<PlayerStateMachine>();
        

        
        if (SaveData.current != null && !LevelManager.instance.newGame)
        { 
            Loaded();
        }
        
        if (LevelManager.instance.newGame)
        {
            PlayerData p = new PlayerData(player);
           
            //put values in p
            SaveData s = new SaveData();
            s.playerData = p;
            
            SerializationManager.Save("Save", s);
            LevelManager.instance.newGame = false;
        }
        //deathPanel.SetActive(false);
       // abiltyBar.gameObject.SetActive(true);
        
        //stateControllers = FindObjectsOfType<StateController>();
        //foreach(StateController controller in stateControllers)
        //{
        //    controller.SetupAi(true);
        //}
    }
    private void Update()
    {
      

        //if (deathPanel.activeInHierarchy)
        //{
        //    if (Input.GetButtonDown("AButton"))
        //    {
        //        Reload();
        //    }
        //}
        
       
       
    }

    public void Reload()
    {   
        SaveData.current = (SaveData)SerializationManager.Load(Application.persistentDataPath + "/saves/Save.tadap");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Loaded()
    {
        
        Vector3 pos = SaveData.current.playerData.position;
        Quaternion rot = SaveData.current.playerData.rotation;
        Instantiate(Player, pos, rot);
        
        player.health = SaveData.current.playerData.lives;
  
    }
    public void Die()
    {
        Debug.Log("die");
        //GameObject.Destroy(player.gameObject);
        LevelManager.instance.newGame = false;
        Reload();
       

    }


}
