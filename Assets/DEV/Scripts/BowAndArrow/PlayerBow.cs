/* PlayerBow.cs
This script is attached to the PlayerBowParent.prefab
It keeps track of how many arrows the player has in his quiver and updates UI accordingly (currently unused, but kept in case we need it later).
It also handles the firing and instantiating of an arrow.
The grappling handler functions are also handled in this script (currently unused, but kept in case we need it later).

Made by: Mats.

*/

using System.Collections;
using Cinemachine;
using DEV.Scripts.Input;
using DEV.Scripts.Managers;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace DEV.Scripts.BowAndArrow
{
    public class PlayerBow : MonoBehaviour
    {
        private CalculateStamina calculateStamina;

        //Settings
        private SettingsEditor settings;
        private Settings.BowAndArrowSettingsModel bowAndArrowSettings;
        private Settings.MovementSettingsModel movementSettingsModel;


        // Private fields used in script
        private int curArrows;
        private float curCharge;
        private bool isChargingBow;
        private bool hasSelectedSpecialArrows;
        private float playerMoveSpeedNormal;
        private float playerMoveSpeedAiming;

        private Camera cam;
        private CinemachineFreeLook thirdPersonCam;
        private CinemachineFreeLook aimCamera;
        private GameObject player;
        private GameObject quiverUI;
        private GameObject aimReticle;
        private Transform playerTransform;
        private Transform arrowSpawnParent;
        [SerializeField] private Transform arrowSpawnLoc;


        // UI fields
        private TMP_Text quiverUIText;
        private UnityEngine.UI.Image bowChargeIndicator;
        private MovementPlayer movementPlayer;
        private PlayerAnimatorHandler playerAnimatorHandler;


        // Private fields for Grapple Function
        private bool grappleIsEngaged;
        private bool reelInButtonIsActive;
        [SerializeField] private LineRenderer grappleLr;
        private Vector3 grappleArrowLoc;
        private Vector3 normalizedDifference;
        private BowGrappleMovement grappleMovement;
        private Gravity gravity;
        private ForceReceiver forceReceiver;
        private bool updateCamRotation;

        public bool isNearBoxCol;

        /// <summary>
        /// Awake function sets values of fields.
        /// </summary>
        private void Awake()
        {
            // Setting the Settings vars
            settings = SettingsEditor.Instance;
            bowAndArrowSettings = settings.bowAndArrowSettingsModel;
            movementSettingsModel = settings.movementSettingsModel;


            player = SceneContext.Instance.playerTransform.gameObject;
            playerTransform = player.transform;
            playerMoveSpeedNormal = movementSettingsModel.forwardSpeed;
            playerMoveSpeedAiming = movementSettingsModel.forwardSpeed / 2;

            cam = SceneContext.Instance.mainCamera;
            thirdPersonCam = SceneContext.Instance.cinemachineFreeLook;
            aimReticle = SceneContext.Instance.aimReticle;
            aimCamera = SceneContext.Instance.cinemachineAimCam;
            playerAnimatorHandler =
                SceneContext.Instance.playerTransform.GetComponentInChildren<PlayerAnimatorHandler>();
            quiverUI = SceneContext.Instance.quiverUI;
            quiverUIText = quiverUI.GetComponent<TMP_Text>();
            movementPlayer = player.GetComponent<MovementPlayer>();
            bowChargeIndicator = SceneContext.Instance.bowChargeIndicator;
            arrowSpawnParent = SceneContext.Instance.objectsSpawnedByPlayer;
            grappleMovement = player.GetComponent<BowGrappleMovement>();
            gravity = player.GetComponent<Gravity>();
            forceReceiver = player.GetComponent<ForceReceiver>();
            calculateStamina = SceneContext.Instance.calculateStamina;
            aimReticle.SetActive(false);
            //aimCamera.enabled = false;
            if (arrowSpawnParent == null)
            {
                GameObject parent = Instantiate(bowAndArrowSettings.arrowSpawnParentPrefab);
                arrowSpawnParent = parent.transform;
            }

            grappleLr = GetComponent<LineRenderer>();

            AddArrowsToQuiver(bowAndArrowSettings.startAmountArrows);
        }

        /// <summary>
        /// It adds an specified amount of arrows to the quiver and updates the UI. Also checks if added amount fit in quiver.
        /// </summary>
        /// <returns>Bool returns true when added arrows were more than maxArrows, returns false is all added arrows fit in the quiver.</returns>>
        /// <param name="amount">Amount of arrows to add to quiver of player.</param>
        public bool AddArrowsToQuiver(int amount)
        {
            curArrows += amount;
            if (curArrows >= bowAndArrowSettings.maxAmountArrows)
            {
                curArrows = bowAndArrowSettings.maxAmountArrows;
                quiverUIText.text = curArrows + " / " + bowAndArrowSettings.maxAmountArrows;
                return true;
            }

            quiverUIText.text = curArrows + " / " + bowAndArrowSettings.maxAmountArrows;
            return false;
        }

        /// <summary>
        /// It removes an specified amount of arrows from the quiver and updates the UI.
        /// </summary>
        /// <param name="amount">Amount of arrows to remove from the quiver of player.</param>
        private void RemoveArrowsFromQuiver(int amount)
        {
            curArrows -= amount;
            quiverUIText.text = curArrows + " / " + bowAndArrowSettings.maxAmountArrows;
        }

        /// <summary>
        /// Updates current charge, when right mouse button is pressed.
        /// (Currently the UI has been disabled, but charge amount is used internally by other scripts).
        /// </summary>
        private void Update()
        {
            if (curArrows != 0)
            {
                if (isChargingBow)
                {
                    if (curCharge <= bowAndArrowSettings.chargeMax)
                    {
                        curCharge += Time.deltaTime * bowAndArrowSettings.chargeRate;
                        SetActiveChargeUI(true);
                        UpdateChargeUI();
                    }
                }
            }
        }

        /// <summary>
        /// Late update to make sure that LineRenderer (for grappling) is drawn after moving the player, so that it is at correct location.
        /// </summary>
        private void LateUpdate()
        {
            if (grappleIsEngaged)
            {
                grappleLr.SetPosition(1, transform.GetChild(0).position);
            }
        }

        /// <summary>
        /// This fixed update moves the player to the grapple location, if the reel in button is pressed and a grapple is engaged.
        /// If the reel in button is not pressed, the movement vector will be set to 0.
        /// </summary>
        private void FixedUpdate()
        {
            if (updateCamRotation)
            {
                Quaternion camRotQuat = cam.transform.rotation;
                Vector3 camRotAll = camRotQuat.eulerAngles;
                Vector3 camRotXZ = new Vector3(0, camRotAll.y, 0);
                playerTransform.DORotate(camRotXZ, .1f);
            }

            if (grappleIsEngaged && reelInButtonIsActive)
            {
                gravity.Value /= bowAndArrowSettings.grappleGravityModifier;
                var diff = grappleArrowLoc - playerTransform.position;
                normalizedDifference = diff.normalized;
                grappleMovement.Value = normalizedDifference * bowAndArrowSettings.grappleSpeed;
                var extraJumpDistance = bowAndArrowSettings.extraJumpDistance;
                if (Mathf.Abs(diff.x) <= extraJumpDistance || Mathf.Abs(diff.y) <= extraJumpDistance ||
                    Mathf.Abs(diff.z) <= extraJumpDistance)
                {
                    forceReceiver.SetAllowedOneExtraJump(true);
                }
            }
            else
            {
                grappleMovement.Value = Vector3.zero;
            }
        }

        /// <summary>
        /// Updates the value of the bow charge indicator, based on curCharge and chargeMax values.
        /// </summary>
        private void UpdateChargeUI()
        {
            float charge = Mathf.Clamp(curCharge / bowAndArrowSettings.chargeMax, 0, 1);
            bowChargeIndicator.fillAmount = charge;
        }

        /// <summary>
        /// Called when charge key is released.
        /// It tells the animation handler that the player want to shoot, and removes an arrow from the quiver.
        /// </summary>
        public void ReleaseBowString()
        {
            if (curArrows != 0 && isChargingBow)
            {
                playerAnimatorHandler.SetShootTriggerInAnimator();
            }
        }

        /// <summary>
        /// Instantiation of the correct type of arrow to shoot. 
        /// </summary>
        public void ShootArrow()
        {
            calculateStamina.state = CalculateStamina.StateEnum.ATTACK;

            Vector3 spawnLoc = arrowSpawnLoc.position +
                               (arrowSpawnLoc.right.x * bowAndArrowSettings.cameraOffsetWhenDrawingBow);
            ArrowType typeToSpawn = ArrowType.Default;
            if (hasSelectedSpecialArrows)
            {
                typeToSpawn = ArrowType.IceAoE;
            }


            GameObject arrowObjectToSpawn;
            switch (typeToSpawn)
            {
                case ArrowType.IceAoE:
                    arrowObjectToSpawn = bowAndArrowSettings.iceAoEPrefab;
                    break;
                default:
                    arrowObjectToSpawn = bowAndArrowSettings.defaultArrowPrefab;
                    break;
            }

            GameObject arrow = Instantiate(arrowObjectToSpawn, spawnLoc, playerTransform.rotation,
                arrowSpawnParent);
            arrow.GetComponent<PlayerArrowBase>().AddForceToArrow(arrow, false);
            curCharge = 0;

            RemoveArrowsFromQuiver(1);
            if (curArrows <= 0)
            {
                SetActiveChargeUI(false);
            }
        }

        /// <summary>
        /// Sets grapple location to the point where the arrow hit.
        /// </summary>
        /// <param name="arrowLoc">Location at which to start the grapple at.</param>
        public void EngageArrowGrapple(Transform arrowLoc)
        {
            grappleIsEngaged = true;
            grappleArrowLoc = arrowLoc.position;
            grappleLr.SetPosition(0, grappleArrowLoc);
        }

        /// <summary>
        /// Sets private var to parameter 'set'
        /// </summary>
        /// <param name="set">bool with true or false, if reelInButton is held down.</param>
        public void SetReelInButtonIsActive(bool set)
        {
            reelInButtonIsActive = set;
        }

        /// <summary>
        /// Stops the current grapple action, and resets all necessary fields back to default. 
        /// </summary>
        public void StopArrowGrapple()
        {
            grappleIsEngaged = false;
            grappleLr.SetPosition(0, Vector3.zero);
            grappleLr.SetPosition(1, Vector3.zero);
        }


        /// <summary>
        /// Used to set local float 'curCharge'. To change how far the bow has been drawn back.
        /// </summary>
        /// <param name="charge">Amount of charge to set to the bow. Value is clamped between 0 and chargeMax</param>
        public void SetCurrentBowCharge(float charge)
        {
            curCharge = Mathf.Clamp(charge, 0, bowAndArrowSettings.chargeMax);
        }

        /// <summary>
        /// Used to set local boolean for if charge key is pressed.
        /// It also calls the method to change the camera to the aiming camera if the charging button is being held.
        /// </summary>
        /// <param name="isCharging">True if player is pressing the charge key, false if player is not pressing charge key.</param>
        public void SetPlayerIsChargingBow(bool isCharging)
        {
            isChargingBow = isCharging;
            movementPlayer.isChargingBow = isCharging;
            if (isCharging)
            {
                StartCoroutine(ChangeCameras(0.1f, "aim"));
                movementSettingsModel.forwardSpeed = playerMoveSpeedAiming;
            }

            if (!isCharging)
            {
                StartCoroutine(ChangeCameras(0, "default"));
                movementSettingsModel.forwardSpeed = playerMoveSpeedNormal;
                aimReticle.SetActive(false);
            }
        }

        /// <summary>
        /// Method to change camera to the aiming camera, or normal third person camera.
        ///
        /// Possible values for 'camToSetActive' are 'aim' and 'default'.
        /// </summary>
        /// <param name="timeToWait">Time to wait before switching cameras</param>
        /// <param name="camToSetActive">Which camera to currently set as active.</param>
        /// <returns></returns>
        private IEnumerator ChangeCameras(float timeToWait, string camToSetActive)
        {
            if (camToSetActive == "aim")
            {
                Quaternion camRotQuat = cam.transform.rotation;
                Vector3 camRotAll = camRotQuat.eulerAngles;
                Vector3 camRotXZ = new Vector3(0, camRotAll.y, 0);
                playerTransform.DORotate(camRotXZ, timeToWait);
                updateCamRotation = true;
            }

            //yield return new WaitForSeconds(timeToWait + 0.1f);
            switch (camToSetActive)
            {
                case "aim":
                    thirdPersonCam.Priority = 11;
                    aimCamera.Priority = 12;
                    aimReticle.SetActive(true);
                    break;
                case "default":
                    thirdPersonCam.Priority = 12;
                    aimCamera.Priority = 11;
                    aimReticle.SetActive(false);
                    updateCamRotation = false;
                    break;
            }

            yield return null;
        }


        /// <summary>
        /// Sets the UI for the bow charge indicator to active or inactive, depending on the parameter 'setActive' 
        /// </summary>
        /// <param name="setActive">True if you want to activate charge UI, false if you want to disable charge UI</param>
        public void SetActiveChargeUI(bool setActive)
        {
            bowChargeIndicator.gameObject.SetActive(setActive);
        }

        /// <summary>
        /// Getter to return the value of private field 'isChargingBow'
        /// </summary>
        /// <returns>Returns the value of private field 'isChargingBow'</returns>
        public bool GetIsChargingBow()
        {
            return isChargingBow;
        }

        /// <summary>
        /// Returns private field value of how many arrows the player has in his quiver.
        /// </summary>
        public int GetCurrentArrows()
        {
            return curArrows;
        }
        
   
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.gameObject.name);
            if (other.gameObject.name == "BoxColSpeed")
            {
                isNearBoxCol = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            Debug.Log(other.gameObject.name);
            if (other.gameObject.name == "BoxColSpeed")
            {
                isNearBoxCol = false;
            }
        }
    }
}