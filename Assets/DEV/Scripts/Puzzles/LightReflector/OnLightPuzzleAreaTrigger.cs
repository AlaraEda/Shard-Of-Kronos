using System;
using System.Collections;
using System.Collections.Generic;
using DEV.Scripts.Input;
using DEV.Scripts.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DEV.Scripts.Puzzles.LightReflector
{
    public class OnLightPuzzleAreaTrigger : MonoBehaviour
    {
        private static readonly int CmVcamLightactivator = Animator.StringToHash("CM_vcam_lightactivator");
        private static readonly int CmVcamHook = Animator.StringToHash("CM_vcam_hookpoint");
        private static readonly int CmVcamActivateBlock = Animator.StringToHash("LightActivateBllock");


        private CinematicCameraShotsManager shotsManager;
        private List<int> camerasToShow = new List<int>();
        private List<string> hintMessages = new List<string>();
        private void Awake()
        {
            shotsManager = SceneContext.Instance.cinemachineStateDrivenCamera.GetComponent<CinematicCameraShotsManager>();
            camerasToShow.Add(CmVcamHook);
            camerasToShow.Add(CmVcamLightactivator);
            camerasToShow.Add(CmVcamActivateBlock);
            hintMessages.Add("In order to open this door, we have to trigger something...");
            hintMessages.Add("These statues must have a purpose.");
            hintMessages.Add("Maybe we can use this.");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<MovementPlayer>() != null)
            {
                StartPrompt();
            }
        }

        
        
        private void StartPrompt()
        {
            shotsManager.StartCoroutine(shotsManager.ShowCameraView(camerasToShow, 5, hintMessages));
            Destroy(gameObject);
        }
    }
}