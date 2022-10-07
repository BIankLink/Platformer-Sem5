using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private void OnEnable()
    {
        if(SaveData.current!= null)
        {
            SaveData.current = (SaveData)SerializationManager.Load(Application.persistentDataPath + "/saves/Save.werks");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            LevelManager.instance.newGame = false;
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            LevelManager.instance.newGame = true;
        }
    }
}
