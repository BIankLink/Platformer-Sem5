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
    public TMP_Text buttonText;
    [SerializeField]PlayableDirector playableDirector;
    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();

    private void Start()
    {
        SaveData.current = (SaveData)SerializationManager.Load(Application.persistentDataPath + "/saves/Save.taadap");
        executed = false;
        
        if ((SaveData.current.playerData != null))
        {
            buttonText.text = "Continue";
        }
        else
        {
            buttonText.text = "New Game";
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
    public void onLoadGame()
    {
        executed = true;
        //StartCoroutine(loadGame());
    }

    public void NewGame()
    {
        executed = true;
        //StartCoroutine(newGame());
    }

    void newGame()
    {
        
        scenesToLoad.Add(SceneManager.LoadSceneAsync("Main"));
        //scenesToLoad.Add(SceneManager.LoadSceneAsync("Tutorial", LoadSceneMode.Additive));
        LevelManager.instance.newGame = true;

    }
    void loadGame()
    {
        
        
        scenesToLoad.Add(SceneManager.LoadSceneAsync("Main"));
        //scenesToLoad.Add(SceneManager.LoadSceneAsync("Tutorial", LoadSceneMode.Additive));
        LevelManager.instance.newGame = false;

    }
    public void OnStart()
    {
        HideMenu();
        ShowLoadingScreen();
        //playableDirector.Resume();
        if(SaveData.current.playerData != null)
        {
            loadGame();
        }
        else
        {
            newGame();
        }
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
