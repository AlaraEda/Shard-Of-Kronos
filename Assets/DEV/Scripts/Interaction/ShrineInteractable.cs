using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DEV.Scripts.Input;
using DEV.Scripts.Managers;
using UnityEngine;
using DG.Tweening;

namespace DEV.Scripts.Interaction
{
    public class ShrineInteractable : Interactable
    {
        // public vars
        public Item shrineActivationKey;
        public float doorCloseTime = 43.0f;
        
        public GameObject doorLeft;
        public GameObject doorRight;
        public float doorLeftEndRotY;
        public float doorRightEndRotY;
        private Inventory.Inventory inventory;
        private GameObject inventoryCanvas;

        // Other private used vars
        private Animator animatorCam;
        private CinematicCameraShotsManager shotsManager;
        private static readonly int CmVcamlvl1door = Animator.StringToHash("CM_vcam_lvl1Door");
        private List<int> camerasToShow = new List<int>();
        private List<string> hintMessages = new List<string>();
        private bool isOpen;

        //private DisplayInventory displayInventory;

        protected override void Awake()
        {
            base.Awake();
            //inventory = Resources.Load<Inventory>("PlayerInventory");
            inventory = Resources.Load<Inventory.Inventory>("PlayerInventory");
        }

        private void Start()
        {
            shotsManager = SceneContext.Instance.cinemachineStateDrivenCamera.GetComponent<CinematicCameraShotsManager>();
            camerasToShow.Add(CmVcamlvl1door);
            hintMessages.Add("A door has opened. It will close soon!");
        }

        public override void OnPlayerInteract()
        {
            if (isOpen)
            {
                SceneContext.Instance.hudContext.DisplayHint("The door is already opened and will close soon!");
                return;
            }
            if (inventory.GetInventorySlotByItem(shrineActivationKey) != null)
            {
                //StartCoroutine(shotsManager.ShowCameraView(camerasToShow, 6, hintMessages));
                StartCoroutine(OpenDoorsSequence());
                SceneContext.Instance.hudContext.DisplayHint("You have temporarily opened a door somewhere!");
            }
            else
            {
                SceneContext.Instance.hudContext.DisplayHint("You do not have the shrine key in your possession!");
                //Debug.Log("Player does not have shrine key in inventory");
            }

        }

        private IEnumerator OpenDoorsSequence()
        {
            //if(doorLeft.transform.localRotation != Quaternion.identity){}

            isOpen = true;
            doorLeft.transform.localRotation = Quaternion.Euler(0,0,0);
            doorRight.transform.localRotation = Quaternion.Euler(0,0,0);
            yield return new WaitForSeconds(1);
            doorLeft.transform.DORotate(new Vector3(0,doorLeftEndRotY,0), 1.5f, RotateMode.LocalAxisAdd);
            doorRight.transform.DORotate(new Vector3(0,doorRightEndRotY,0), 1.5f, RotateMode.LocalAxisAdd);
            yield return new WaitForSeconds(4);
            var action = doorLeft.transform.DORotate(new Vector3(0,doorLeftEndRotY *-1,0), doorCloseTime, RotateMode.LocalAxisAdd);
            doorRight.transform.DORotate(new Vector3(0,doorRightEndRotY*-1,0), doorCloseTime, RotateMode.LocalAxisAdd).SetEase(Ease.Linear);
            action.SetEase(Ease.Linear);
            yield return action.WaitForCompletion();
            isOpen = false;

        }

    }
    
    
}