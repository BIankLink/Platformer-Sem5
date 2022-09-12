using System.Collections;
using System.Collections.Generic;
using UnityEngine;using UnityEngine.UI;
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

            //Debug.LogWarning("More than one instance of Inventory found");
            return;
        }
        instance = this;



    }
    #endregion
   

    public PlayerStateMachine player;
   
    //public Button Load;

   
   // public GameObject deathPanel;
   // public GameObject instructions;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStateMachine>();
        if(SaveData.current!= null&&!LevelManager.instance.newGame)
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
        
            Time.timeScale = 1;
            SaveData.current = (SaveData)SerializationManager.Load(Application.persistentDataPath + "/saves/Save.werks");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            
        
    }
    public void Loaded()
    {

        player.transform.position = new Vector3(SaveData.current.playerData.position[0], SaveData.current.playerData.position[1], SaveData.current.playerData.position[2]);
        player.transform.rotation = Quaternion.Euler(SaveData.current.playerData.rotation[0], SaveData.current.playerData.rotation[1], SaveData.current.playerData.rotation[2]);
        player.health = SaveData.current.playerData.lives;
       

        
           
        
    }
    public void Die()
    {
       
        GameObject.Destroy(player.gameObject);
        LevelManager.instance.newGame = false;
       

    }


}
