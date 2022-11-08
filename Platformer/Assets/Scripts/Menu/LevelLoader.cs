using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();
    private void OnEnable()
    {
        if(SaveData.current!= null)
        {
            SaveData.current = (SaveData)SerializationManager.Load(Application.persistentDataPath + "/saves/Save.tadap");
            scenesToLoad.Add(SceneManager.LoadSceneAsync("Main"));
            scenesToLoad.Add(SceneManager.LoadSceneAsync("Tutorial", LoadSceneMode.Additive));
            LevelManager.instance.newGame = false;
        }
        else
        {
            
            scenesToLoad.Add(SceneManager.LoadSceneAsync("Main"));
            scenesToLoad.Add(SceneManager.LoadSceneAsync("Tutorial", LoadSceneMode.Additive));
            LevelManager.instance.newGame = true;
        }
    }
}
