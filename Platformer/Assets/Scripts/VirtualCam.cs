using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCam : MonoBehaviour
{
    CinemachineVirtualCamera virtualCamera;
    CinemachineCameraOffset offset;
    GameObject player;
    Transform followTarget;
    [SerializeField] float offsetSpeed=10;
    // Start is called before the first frame update
    void Start()
    {
        
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        offset = GetComponent<CinemachineCameraOffset>();
        
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
        if (player != null)
        {
            if (player.transform.rotation.y < 0)
            {
                offset.m_Offset = Vector3.MoveTowards(offset.m_Offset,new Vector3(-4,0,0),offsetSpeed*Time.deltaTime);
            }
            else
            {
                offset.m_Offset = Vector3.MoveTowards(offset.m_Offset, new Vector3(4, 0, 0), offsetSpeed*Time.deltaTime);
            }
        }
    }
}
