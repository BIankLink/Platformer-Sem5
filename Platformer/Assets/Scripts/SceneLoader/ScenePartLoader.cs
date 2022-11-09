using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum CheckMethod 
{
    Distance,
    Trigger
}

public class ScenePartLoader : MonoBehaviour
{
    public Transform player;
    public CheckMethod checkMethod;
    public float loadRange;
    
    //Scene State
    [SerializeField]private bool isLoaded;
    private bool shouldLoad;

    // Start is called before the first frame update
    void Awake()
    {
        if(SceneManager.sceneCount > 0)
        {
            for( int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if(scene.name == gameObject.name)
                {
                    isLoaded = true;
                    Debug.Log("enable");
                    
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(checkMethod == CheckMethod.Distance)
        {
            DistanceCheck();
        }
        else if (checkMethod == CheckMethod.Trigger)
        {
            TriggerCheck();
        }
    }

    void DistanceCheck()
    {
        if (Vector3.Distance(player.position, transform.position) < loadRange)
        {
            LoadScene();
        }
        else
        {
            UnloadScene();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            shouldLoad = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            shouldLoad = false;
        }
    }
    private void TriggerCheck()
    {
        if (shouldLoad)
        {
            LoadScene();
        }
        else
        {
            UnloadScene();
        }
    }
    private void LoadScene()
    {
        if (!isLoaded)
        {
            SceneManager.LoadSceneAsync(gameObject.name, LoadSceneMode.Additive);
            isLoaded = true;
        }
    }
    private void UnloadScene()
    {
        if (isLoaded)
        {
            SceneManager.UnloadSceneAsync(gameObject.name);
            isLoaded = false;
        }
    }

}
