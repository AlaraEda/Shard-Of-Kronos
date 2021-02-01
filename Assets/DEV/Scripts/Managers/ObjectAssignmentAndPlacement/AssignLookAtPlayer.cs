using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DEV.Scripts.Managers;
using UnityEngine;

public class AssignLookAtPlayer : MonoBehaviour
{
    private CinemachineFreeLook thirdPersonCam;
    private Transform playerTransform;

    private void Awake()
    {
        playerTransform = SceneContext.Instance.lookAtPlayerShoulderLocation.transform;
        thirdPersonCam = GetComponent<CinemachineFreeLook>();
        thirdPersonCam.LookAt = playerTransform;
        thirdPersonCam.Follow = playerTransform;
    }
}
