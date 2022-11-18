using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GlobalEnums;

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




        Debug.Log(SaveData.current);
        if (SaveData.current != null && !LevelManager.instance.newGame)
        {
            Loaded();
        }

        if (LevelManager.instance.newGame)
        {
            SceneManager.LoadScene("Tutorial", LoadSceneMode.Additive);
            StartCoroutine(spawn(Player, startPos.position, startPos.rotation));
            player = Player.GetComponent<PlayerStateMachine>();
            PlayerData p = new PlayerData(player);

            //put values in p
            SaveData s = new SaveData();
            s.playerData = p;

            SerializationManager.Save("Save", s);
            LevelManager.instance.newGame = false;


        }
           
    }
    #endregion
    [SerializeField] GameObject Player;
    [SerializeField] Transform startPos;
    public PlayerStateMachine player;
    [SerializeField] List<SaveTrigger> saveTriggers = new List<SaveTrigger>();
    
    //public Button Load;

   
   // public GameObject deathPanel;
   // public GameObject instructions;

    private void Start()
    {
        
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
        SaveData.current = (SaveData)SerializationManager.Load(Application.persistentDataPath + "/saves/Save.taadap");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
    public void Loaded()
    {
        
        Vector3 pos = SaveData.current.playerData.position;
        for(int i = 0; i < saveTriggers.Count; i++)
        {
            if (Vector3.Distance(saveTriggers[i].transform.position,pos)<=3)
            {
                Debug.Log("Lo");
                SceneManager.LoadScene(saveTriggers[i].level.ToString(), LoadSceneMode.Additive);
            }
        }
        Quaternion rot = SaveData.current.playerData.rotation;
        StartCoroutine(spawn(Player, pos, rot));

        player = Player.GetComponent<PlayerStateMachine>();
        player.health = SaveData.current.playerData.lives;
  
    }
    public void Die()
    {
        Debug.Log("die");
        //GameObject.Destroy(player.gameObject);
        LevelManager.instance.newGame = false;
        Reload();
       

    }

    IEnumerator spawn(GameObject player,Vector3 pos,Quaternion rot)
    {
        yield return new WaitForSeconds(0);
        Instantiate(player, pos, rot);
    }
}
