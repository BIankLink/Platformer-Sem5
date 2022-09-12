using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetButtonDown("StartButton"))
        //{
        //    Debug.Log("Start");
        //    PauseUnpause();
        //}
    }

    public void PauseUnpause()
    {
        if (!pauseMenu.activeInHierarchy)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    //public void LoadLevel()
    //{
    //    if (SaveData.current != null) 
    //    {
    //        Time.timeScale = 1f;
    //        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    //    } 
    //}

    public void quit()
    {
        Application.Quit();
    }
}
