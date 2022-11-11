using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region Singelton
    public static LevelManager instance;
    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);

           
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);


    }
    #endregion
    public bool newGame;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
