using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using TMPro;
using System;

public class MainMenu : MonoBehaviour
{
    public float seconds;
    public bool executed;
    public GameObject loadingInterface;
    public GameObject menu;
    
    public Image loadingProgressBar;
    public GameObject newGameButton;
    public GameObject LoadGameButton;
    [SerializeField]PlayableDirector playableDirector;
    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();

    private void Start()
    {
        SaveData.current = (SaveData)SerializationManager.Load(Application.persistentDataPath + "/saves/Save.taadap");
        executed = false;
        
        if ((SaveData.current.playerData != null))
        {
            LoadGameButton.SetActive(true);
            newGameButton.SetActive(true);
            

        }
        else
        {
            LoadGameButton.SetActive(false);

        }
        Debug.Log(Application.persistentDataPath);

    }
    private void OnEnable()
    {
        //Debug.Log("Paused");
        //playableDirector.Pause();
    }
    public void HideMenu()
    {
        menu.SetActive(false);
    }
   

    public void newGame()
    {
        
        scenesToLoad.Add(SceneManager.LoadSceneAsync("Main"));
        OnStart();
        //scenesToLoad.Add(SceneManager.LoadSceneAsync("Tutorial", LoadSceneMode.Additive));
        LevelManager.instance.newGame = true;

    }
    public void loadGame()
    {
        
        
        scenesToLoad.Add(SceneManager.LoadSceneAsync("Main"));
        OnStart();
        //scenesToLoad.Add(SceneManager.LoadSceneAsync("Tutorial", LoadSceneMode.Additive));
        LevelManager.instance.newGame = false;

    }
    public void OnStart()
    {
        HideMenu();
        ShowLoadingScreen();
        //playableDirector.Resume();
       
        StartCoroutine(LoadingScreen());
        
    }

    private void ShowLoadingScreen()
    {
        loadingInterface.SetActive(true);
    }
    IEnumerator LoadingScreen()
    {
        float totalProgress = 0;
        for(int i = 0; i < scenesToLoad.Count; i++)
        {
            while (!scenesToLoad[i].isDone)
            {
                totalProgress += scenesToLoad[i].progress;
                loadingProgressBar.fillAmount = totalProgress / scenesToLoad.Count;
                //Debug.Log(loadingProgressBar.fillAmount);
                yield return null;
            }
        }
    }

}
