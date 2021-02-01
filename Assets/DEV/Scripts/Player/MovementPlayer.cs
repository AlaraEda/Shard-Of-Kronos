using System;
using DEV.Scripts.BowAndArrow;
using DEV.Scripts.Managers;
using DG.Tweening;
using UnityEngine;



namespace DEV.Scripts.Input
{
    /// <summary>
    /// This class retrieves data from InputController.cs and sends the data via IMovementModifier.cs to MovementHandler.cs.
    /// -------------
    /// SetMovementInput(Vector2 movementInputNew) retrieves the data from InputController.cs This updates the Vector2 movementInput.
    /// With the X and Z value from this vector, the script knows what direction the player is looking and has to move.
    /// -------------
    /// Movement() lets the player Move and Shoot.
    /// If the movementInput is larger than the moveTreshold it lets the script know that the player now has to move or Aim. This data then
    /// adds to the IMovementModifier modifier list. 
    /// </summary>
    public class MovementPlayer : MonoBehaviour, IMovementModifier
    {
        //Scripts that retrieve/Sends data from this script
        private MovementHandeler movementHandeler;
        private CalculateStamina calculateStamina;
        
        //Settings
        private SettingsEditor settingsEditor;
        private Settings.MovementSettingsModel movementSettingsModel;

        private PlayerController playerController;
        private CharacterController characterController;
        private GhostStruct ghoststruct;
        private PlayerBow playerBow;
        
        private GameObject Player;
        private Vector3 playerMovement;

        //Camera Objects
        public float turnSmoothTime = 0.1f;
        private float turnSmoothVelocity;
        private Camera cam;

        public bool isChargingBow;

        private Vector3 gravityDirection; //For multiple gravity center objects (Spaceship, planet etc)
        private Vector3 gravityMovement;
        
        //The data from IMovementModifier
        public Vector3 Value { get; private set; }
        public string Name { get; set; }

        //The retrieved data from InputController
        private Vector2 movementInput;
        
        //The forward speed
        private float curForwardSpeed;

        

        private void Awake()
        {
            Player = SceneContext.Instance.playerTransform.gameObject;
            playerController = SceneContext.Instance.playerManager;
            cam = SceneContext.Instance.mainCamera;
            settingsEditor = SettingsEditor.Instance;
            movementSettingsModel = settingsEditor.movementSettingsModel;
            playerBow = SceneContext.Instance.playerBowParent.GetComponent<PlayerBow>();
            characterController = playerController.charController;
            movementHandeler = playerController.movementHandeler;
            curForwardSpeed = movementSettingsModel.forwardSpeed;
            ghoststruct=transform.parent.gameObject.GetComponent<GhostStruct>();
            Name = GetType().ToString();
            calculateStamina = SceneContext.Instance.calculateStamina;
        }
        
        private void OnEnable() => movementHandeler.AddModifier(this);

        private void OnDisable() => movementHandeler.RemoveModifier(this);


        private void FixedUpdate()
        {
            Movement();
        }

        /// <summary>
        /// Gets data from InputController.cs with a x and z value and updates the movementInput;
        /// </summary>
        /// <param name="movementInputNew">
        /// Gets the param from InputController.SetMovementInput(Vector2 inp)
        /// </param>
        public void SetMovementInput(Vector2 movementInputNew)
        {
            movementInput = movementInputNew;
        }

        /// <summary>
        /// This method gets called in the FixedUpdate. It either returns, No moving (Value = 0), Moving or Aiming.
        /// When it returns Moving or Aiming, the value from the movement calculation gets send to the IMovementModifier Value.
        /// </summary>
        private void Movement()
        {
            
            playerMovement = new Vector3(movementInput.x, 0, movementInput.y).normalized;
            
            //Treshold in whenever the script thinks the player is moving
            float moveTreshold = 0.01f;
            
            //Gets what Angle the player is looking and moves the player to that direction (x,z)
            float targetAngle = Mathf.Atan2(playerMovement.x, playerMovement.z) * Mathf.Rad2Deg +
                                cam.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,
                turnSmoothTime);
            
            //PlayerBow.cs sets bool isChargingBow active when the player is aiming. This drops the player speed by *4*
            if (isChargingBow)
            {
                if (movementInput.magnitude >= moveTreshold)
                {
                    Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                    Value = moveDir.normalized * curForwardSpeed / 4;
                }
                else
                {
                    Value = Vector3.zero;
                }
            }
            
            //If the player is not moving
            else if (movementInput.magnitude >= moveTreshold)
            {
                calculateStamina.state = CalculateStamina.StateEnum.RUN;
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                Value = moveDir.normalized * curForwardSpeed;
            }
            else
            {
                calculateStamina.state = CalculateStamina.StateEnum.IDLE;
                Value = Vector3.zero;
            }
            
            if (movementInput.magnitude >= moveTreshold && !isChargingBow)
            {
                SceneContext.Instance.hasMoved = true;
            }
            else
            {
                SceneContext.Instance.hasMoved = false;  
            }
         

        }

        /// <summary>
        /// This method gets the param "set" from GhostStruct.cs and the Cheat Panel. "curForwardSpeed" is the speed in which the player
        /// could move forward. 
        /// </summary>
        /// <param name="set"></param>
        public void SetCurrentForwardSpeed(float set)
        {
            curForwardSpeed = set;
        }

       
    }
}
