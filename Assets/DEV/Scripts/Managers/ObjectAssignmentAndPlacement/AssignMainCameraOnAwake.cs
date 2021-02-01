using System;
using System.Collections;
using System.Collections.Generic;
using DEV.Scripts.Managers;
using UnityEngine;

public class AssignMainCameraOnAwake : MonoBehaviour
{
    private Camera mainCam;
    private Canvas canvasToSetCameraTo;
    
    private void Awake()
    {
        mainCam = SceneContext.Instance.mainCamera;
        canvasToSetCameraTo = GetComponent<Canvas>();
        canvasToSetCameraTo.worldCamera = mainCam;
    }
}
