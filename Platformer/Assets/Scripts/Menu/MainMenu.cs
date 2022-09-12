using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public float seconds;
    public bool executed;

    private void Start()
    {
        executed = false;
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        LevelManager.instance.newGame = true;
    }
    IEnumerator loadGame()
    {
        yield return new WaitForSeconds(seconds);
        SaveData.current = (SaveData)SerializationManager.Load(Application.persistentDataPath + "/saves/Save.werks");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        LevelManager.instance.newGame = false;
    }

}
