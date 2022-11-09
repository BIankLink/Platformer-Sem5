using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCam : MonoBehaviour
{
    CinemachineVirtualCamera virtualCamera;
    GameObject player;
    Transform followTarget;
    // Start is called before the first frame update
    void Start()
    {
        
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                followTarget = player.transform;
                virtualCamera.LookAt = followTarget;
                virtualCamera.Follow = followTarget;
            }
        }
    }
}
