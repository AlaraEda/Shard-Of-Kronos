using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DEV.Scripts.Input;
using DEV.Scripts.Managers;
using DG.Tweening;
using UnityEngine;

/*
Manages all the Cinametic Shots in our game by using "Cinemachine". 
*/
public class CinematicCameraShotsManager : MonoBehaviour
{
    private CinemachineStateDrivenCamera stateDrivenCamera;
    private Animator camAnimator;
    private InputController inputController;
    private MovementHandeler movementHandeler;
    private CinematicCameraShotsManager shotsManager;
    Sequence camTween;

    private List<int> allCamParameters = new List<int>();
    
    private static readonly int CmVcamLightactivator = Animator.StringToHash("CM_vcam_lightactivator");
    private static readonly int CmVcamHook = Animator.StringToHash("CM_vcam_hookpoint");
    private static readonly int CmVcamMaze = Animator.StringToHash("CM_vcam_MazePuzzle");
    private static readonly int CmVcamlvl1door = Animator.StringToHash("CM_vcam_lvl1Door");
    private static readonly int CmVcamlvl1Comet = Animator.StringToHash("CM_vcam_lvl1CometCam");

    //When awake, set up access to InputController, Movementhandeler & CinemachineStateDrivenCamera.
    //Also add/create camera scene shots you want. Set all scenes to false.
    private void Awake()
    {
        inputController = SceneContext.Instance.playerManager.gameObject.GetComponent<InputController>();
        movementHandeler = SceneContext.Instance.playerTransform.gameObject.GetComponent<MovementHandeler>();
        stateDrivenCamera = GetComponent<CinemachineStateDrivenCamera>();
        shotsManager = SceneContext.Instance.cinemachineStateDrivenCamera.GetComponent<CinematicCameraShotsManager>();
        //Debug.Log("CinematicManager: "+CmVcamlvl1Comet);
        
        //All camera scenes
        camAnimator = SceneContext.Instance.camAnimator;
        allCamParameters.Add(CmVcamLightactivator);
        allCamParameters.Add(CmVcamHook);
        allCamParameters.Add(CmVcamMaze);
        allCamParameters.Add(CmVcamlvl1door);
        allCamParameters.Add(CmVcamlvl1Comet);
        
        //Set al cam parameters to false.
        foreach (var cam in allCamParameters)
        {
            camAnimator.SetBool(cam, false);
        }
        stateDrivenCamera.m_AnimatedTarget = camAnimator;
    }

    //While playing a shot, disable the in game movement of the player until shot is finished.
    private void DisablePlayerMovement(bool disable)
    {
        if (disable)
        {
            inputController.OnDisable();
            movementHandeler.changingPlayerPosition = true;
            inputController.SetMovementInput(Vector2.zero);
        }
        else
        {
            movementHandeler.changingPlayerPosition = false;
            inputController.OnEnable();
        }
    }

    private void ActivateStateDrivenCamera(bool activate)
    {
        stateDrivenCamera.Priority = activate ? 13 : 4;
        
    }

    private void ActivateNeededVirtualCamAndDisableTheRestOfVirtualCams(int camToEnable)
    {
        
    }

    public void SkipCurrentCinematic()
    {
        camTween.Complete();
    }

    //Show the Camera shot. Activate the State driven Camera and disable playermovement. 
    // CameraParameterID = the scene you want to show
    // timeBetweenSwitch = how long the switch takes place
    // hintMessage = the message you get from the camera show
    public IEnumerator ShowCameraView(List<int> cameraParameterId, float timeBetweenSwitch, List<string> hintMessage)
    {
        //Disable game
        var time = timeBetweenSwitch;
        ActivateStateDrivenCamera(true);
        DisablePlayerMovement(true);
        int counter = 0;

        //Show camera scene
        foreach (var cam in cameraParameterId)
        {
            camTween = DOTween.Sequence();
            camTween.Append(DOTween.To(() => time, x => time = x, timeBetweenSwitch, timeBetweenSwitch));
            foreach (var allCams in allCamParameters)
            {
                if (allCams != cam)
                {
                    camAnimator.SetBool(allCams, false);
                }
            }

            ActivateNeededVirtualCamAndDisableTheRestOfVirtualCams(cam);
            camAnimator.SetBool(cam, true);
            
            if (hintMessage != null)
            {
                SceneContext.Instance.hudContext.DisplayHint(hintMessage[counter]);
            }

            counter++;
            //yield return new WaitForSeconds(timeBetweenSwitch);
            yield return camTween.WaitForCompletion();
            camAnimator.SetBool(cam, false);
            
        }

        //After camera scene is finished
        DisablePlayerMovement(false);
        ActivateStateDrivenCamera(false);
    }


}
