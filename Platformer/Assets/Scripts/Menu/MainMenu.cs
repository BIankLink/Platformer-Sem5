using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public float seconds;
    public bool executed;
    public TMP_Text buttonText;
    [SerializeField]PlayableDirector playableDirector;

    
    private void Start()
    {
        executed = false;
        if ((SaveData.current != null))
        {
            buttonText.text = "Continue";
        }
        else
        {
            buttonText.text = "New Game";
        }
        

    }
    private void OnEnable()
    {
        Debug.Log("Paused");
        playableDirector.Pause();
    }
    public void onLoadGame()
    {
        executed = true;
        StartCoroutine(loadGame());
    }

    public void NewGame()
    {
        executed = true;
        StartCoroutine(newGame());
    }

    IEnumerator newGame()
    {
        yield return new WaitForSeconds(seconds);
        
        LevelManager.instance.newGame = true; 
       
    }
    IEnumerator loadGame()
    {
        yield return new WaitForSeconds(seconds);
        SaveData.current = (SaveData)SerializationManager.Load(Application.persistentDataPath + "/saves/Save.tadap");
        
        LevelManager.instance.newGame = false; 
       
    }
    public void OnStart()
    {
        playableDirector.Resume();
        
    }
}
