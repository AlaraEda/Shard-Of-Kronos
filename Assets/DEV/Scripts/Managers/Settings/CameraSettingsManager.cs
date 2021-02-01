using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DEV.Scripts.Managers;
using UnityEngine;

/// <summary>
/// This script handles the properties for the camerasettings. 
/// </summary>
public class CameraSettingsManager : MonoBehaviour
{
    [Header("Main third person camera settings")]
    public float ySpeed = 2;
    public float yAccelTime = 0.2f;
    public float yDecelTime = 0.1f;
    public float xSpeed = 150;
    public float xAccelTime = 0.1f;
    public float xDecelTime = 0.1f;
    public bool invertX;
    public bool invertY;

    [Header("Divide factors for aim camera")]
    public float aimCamYSpeedDivideFactor = 8;
    public float aimCamYAccelDivideFactor = 2;
    public float aimCamYDecelDivideFactor = 2;
    public float aimCamXSpeedDivideFactor = 4;
    public float aimCamXAccelDivideFactor = 2;
    public float aimCamXDecelDivideFactor = 2;
    
    [SerializeField]   private CinemachineFreeLook thirdPersonCam;
    private CinemachineFreeLook aimCam;
    private Camera mainCam;
    private CinemachineBrain brain;
    private CinemachineCollider thirdPersonCamCollider;
    private CinemachineCollider aimCamCollider;
    private Settings.CameraSettingsModel cameraSettings;

    private void Awake()
    {
        cameraSettings = SettingsEditor.Instance.cameraSettingsModel;
        thirdPersonCam = SceneContext.Instance.cinemachineFreeLook;
        aimCam = SceneContext.Instance.cinemachineAimCam;
        mainCam = SceneContext.Instance.mainCamera;
        thirdPersonCamCollider = thirdPersonCam.GetComponent<CinemachineCollider>();
        aimCamCollider = aimCam.GetComponent<CinemachineCollider>();
        
    }

    private void Start()
    {
        
        // Brain settings
        brain = mainCam.GetComponent<CinemachineBrain>();
        brain.m_UpdateMethod = CinemachineBrain.UpdateMethod.FixedUpdate;
        brain.m_BlendUpdateMethod = CinemachineBrain.BrainUpdateMethod.LateUpdate;
        
        // Cam collider settings
        thirdPersonCamCollider.m_Strategy = CinemachineCollider.ResolutionStrategy.PullCameraForward;
        aimCamCollider.m_Strategy = CinemachineCollider.ResolutionStrategy.PullCameraForward;
        
        // Aim axis invertions
        thirdPersonCam.m_XAxis.m_InvertInput = cameraSettings.invertedX;
        thirdPersonCam.m_YAxis.m_InvertInput = cameraSettings.invertedY;
        aimCam.m_XAxis.m_InvertInput = cameraSettings.invertedX;
        aimCam.m_YAxis.m_InvertInput = cameraSettings.invertedY;

        // third person cam
        thirdPersonCam.m_YAxis.m_MaxSpeed = cameraSettings.sensitivityY;
        thirdPersonCam.m_YAxis.m_AccelTime = yAccelTime;
        thirdPersonCam.m_YAxis.m_DecelTime = yDecelTime;
        
        thirdPersonCam.m_XAxis.m_MaxSpeed = cameraSettings.sensitivityX;
        thirdPersonCam.m_XAxis.m_AccelTime = xAccelTime;
        thirdPersonCam.m_XAxis.m_DecelTime = xDecelTime;
        
        // aim cam
        aimCam.m_YAxis.m_MaxSpeed = cameraSettings.sensitivityY/cameraSettings.aimCamYDivider;
        aimCam.m_YAxis.m_AccelTime = yAccelTime/aimCamYAccelDivideFactor;
        aimCam.m_YAxis.m_DecelTime = yDecelTime/aimCamYDecelDivideFactor;
        
        aimCam.m_XAxis.m_MaxSpeed = cameraSettings.sensitivityX/cameraSettings.aimCamXDivider;
        aimCam.m_XAxis.m_AccelTime = xAccelTime/aimCamXAccelDivideFactor;
        aimCam.m_XAxis.m_DecelTime = xDecelTime/aimCamXDecelDivideFactor;
    }

  
    public void UpdateSettingsToComponents()
    {
        thirdPersonCam.m_XAxis.m_InvertInput = cameraSettings.invertedX;
        thirdPersonCam.m_YAxis.m_InvertInput = cameraSettings.invertedY;
        aimCam.m_XAxis.m_InvertInput = cameraSettings.invertedX;
        aimCam.m_YAxis.m_InvertInput = cameraSettings.invertedY;
        thirdPersonCam.m_YAxis.m_MaxSpeed = cameraSettings.sensitivityY;
        thirdPersonCam.m_XAxis.m_MaxSpeed = cameraSettings.sensitivityX;
        aimCam.m_YAxis.m_MaxSpeed = cameraSettings.sensitivityY/cameraSettings.aimCamYDivider;
        aimCam.m_XAxis.m_MaxSpeed = cameraSettings.sensitivityX/cameraSettings.aimCamXDivider;
    }
}
