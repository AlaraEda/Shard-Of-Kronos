using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DEV.Scripts.Managers;
using DEV.Scripts.Managers.WorldSwitching;
using UnityEngine;
using TMPro;
using DG.Tweening;
using DEV.Scripts.WorldSwitching;

public class Tutorialmanager : MonoBehaviour
{
    public CometScript cometScript;
    public CinemachineVirtualCamera activateDoorCamera;
    
    private WorldSwitchManager worldSwitchManager;

    // booleans to check if colliders has already been triggered 
    private bool wasdColliderComplete;
    private bool bowColliderComplete;
    private bool spiritColliderComplete;
    private bool interactColliderComplete;
    private bool pressureHintColliderComplete;
    private bool pillarHintColliderComplete;
    private bool returnToGhostComplete;
    
    private Animator animatorCam;
    private CinematicCameraShotsManager shotsManager;
    private static readonly int CmVcamlvl1Comet = Animator.StringToHash("CM_vcam_lvl1CometCam");
    private List<int> camerasToShow = new List<int>();
    

    void Start()
    {
        worldSwitchManager = SceneContext.Instance.worldSwitchManager;
        worldSwitchManager.OnWorldSwitchEvent += ShowReturnToGhostText;
        shotsManager = SceneContext.Instance.cinemachineStateDrivenCamera.GetComponent<CinematicCameraShotsManager>();
        camerasToShow.Add(CmVcamlvl1Comet);
    }

    private void ShowReturnToGhostText(object sender, WorldSwitchEventArgs args)
    {
        if (!returnToGhostComplete)
        {
            SceneContext.Instance.hudContext.DisplayHint("Certain objects can only be used in this Spirit state. You can recognize these objects by the red outline.", 10);
            returnToGhostComplete = true;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        var obj = (other.gameObject.name);
        
        switch (obj)
        {
            case ("wasdcollider"):
                if (!wasdColliderComplete)
                {
                    SceneContext.Instance.hudContext.DisplayHint("Use the WASD keys to move and Space to jump.",5);
                    wasdColliderComplete = true;
                }
                break;

            case ("bowcollider"):
                if (!bowColliderComplete)
                {
                    SceneContext.Instance.hudContext.DisplayHint("Use the right mouse to charge your bow and left mouse to shoot.", 5);
                    bowColliderComplete = true;
                }
                break;
                
            case ("spiritcollider"):
                if (!spiritColliderComplete)
                {
                    StartCoroutine(StartCometSequence());
                    spiritColliderComplete = true;
                }
                break;

            case ("interact"):
                if (!interactColliderComplete)
                {
                    SceneContext.Instance.hudContext.DisplayHint("Press 'E' to interact with objects", 5);
                    activateDoorCamera.enabled = true;
                    interactColliderComplete = true;
                }
                break;

            case ("pressurehint"):
                if (!pressureHintColliderComplete)
                {
                    SceneContext.Instance.hudContext.DisplayHint("Press C!", 5);
                    pressureHintColliderComplete = true;
                }

                break;
            case ("pillarhint"):
                if (!pillarHintColliderComplete)
                {
                    SceneContext.Instance.hudContext.DisplayHint("I think the door will open when I destroy these orbs", 5);
                    pillarHintColliderComplete = true;
                }

                break;
        }
    }

    private IEnumerator StartCometSequence()
    {
        StartCoroutine(shotsManager.ShowCameraView(camerasToShow, 6, null));
        yield return new WaitForSeconds(0.6f);
        StartCoroutine(cometScript.TriggerComet());
        yield return new WaitForSeconds(6f - 0.6f);
        SceneContext.Instance.hudContext.DisplayHint("The shard's impact has infused you with a new mysterious ability. Press C to enter the spirit state.", 5);
    }

}
