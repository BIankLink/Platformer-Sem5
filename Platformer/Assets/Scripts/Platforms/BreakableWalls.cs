using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWalls : LivingEntity
{
    // Start is called before the first frame update
    public GameObject crackedWall;
    // Update is called once per frame
    void Update()
    {
        if(health == 0)
        {
            crackedWall.SetActive(true);
        }
    }
}
