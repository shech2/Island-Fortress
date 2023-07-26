using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    public Camera mainCamera; 
    public Camera otherCamera;

    void Start()
    {
        mainCamera.enabled = true;
        otherCamera.enabled = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            SwapCameras();
        }
    }

    void SwapCameras()
    {
        if(mainCamera.enabled)
        {
            mainCamera.enabled = false;
            otherCamera.enabled = true;
        }
        else
        {
            otherCamera.enabled = false;
            mainCamera.enabled = true;
        }
    }
}
