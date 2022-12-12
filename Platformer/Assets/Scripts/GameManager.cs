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
        save = GetComponent<SaveMachine>();
        AudioManager.instance.Play("Ambience");


        //Debug.Log(SaveData.current);
        if (SaveData.current != null && !LevelManager.instance.newGame)
        {
            Loaded();
        }

        if (LevelManager.instance.newGame)
        {
            SceneManager.LoadScene("Tutorial", LoadSceneMode.Additive);
            player = Instantiate(Player, startPos.position, startPos.rotation).GetComponent<PlayerStateMachine>();
            //StartCoroutine(spawn(Player, startPos.position, startPos.rotation));

            //PlayerData p = new PlayerData(player);

            ////put values in p
            //SaveData s = new SaveData();
            //s.playerData = p;

            //SerializationManager.Save("Save", s);
            player.health = player.startingHealth;
            save.mysave();
            
            LevelManager.instance.newGame = false;

        }
           
    }
    #endregion
    [SerializeField] GameObject Player;
    [SerializeField] Transform startPos;
    public PlayerStateMachine player;
    [SerializeField] List<SaveTrigger> saveTriggers = new List<SaveTrigger>();
    public bool firstDestroyedGrave=false;
    public GameObject fadeblackSplash2;
    [SerializeField] HealthBar healthBar;
    SaveMachine save;
    [HideInInspector]public bool onlyOnce;
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
        if (firstDestroyedGrave)
        {
            
            if (!onlyOnce)
            {
                fadeblackSplash2.SetActive(true);
                onlyOnce = true;
                save.mysave();
            }
        }
        
    }
    public void Reload()
    {   
        SaveData.current = (SaveData)SerializationManager.Load(Application.persistentDataPath + "/saves/Save.taadap");
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
    public void Loaded()
    {
        
        Vector3 pos = new Vector3(SaveData.current.playerData.position.x, SaveData.current.playerData.position.y+5f,0);
        for(int i = 0; i < saveTriggers.Count; i++)
        {
            if (Vector3.Distance(saveTriggers[i].transform.position,pos)<=3)
            {
                Debug.Log("Lo");
                SceneManager.LoadScene(saveTriggers[i].level.ToString(), LoadSceneMode.Additive);
            }
        }
        Quaternion rot = SaveData.current.playerData.rotation;
        //StartCoroutine(spawn(Player, pos, rot));
        player = Instantiate(Player, pos, rot).GetComponent<PlayerStateMachine>();
        onlyOnce = SaveData.current.playerData.firstBlood;
        //player = Player.GetComponent<PlayerStateMachine>();
        player.health = SaveData.current.playerData.lives;
        healthBar.fill.fillAmount = player.health / player.startingHealth;
  
    }
    public void Die()
    {
        Debug.Log("die");
        //GameObject.Destroy(player.gameObject);
        LevelManager.instance.newGame = false;
        Reload();
       

    }

    IEnumerator spawn(GameObject PlayerObj,Vector3 pos,Quaternion rot)
    {
        yield return new WaitForSeconds(0);
        player = Instantiate(PlayerObj, pos, rot).GetComponent<PlayerStateMachine>();
        

    }
}
