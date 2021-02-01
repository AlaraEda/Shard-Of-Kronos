using System;
using System.Collections;
using System.Collections.Generic;
using DEV.Scripts;
using DEV.Scripts.BowAndArrow;
using DEV.Scripts.Input;
using DEV.Scripts.Managers;
using DEV.Scripts.WorldSwitching;
using UnityEngine;

/// <summary>
/// This class retrieves data from InputController.cs and sends the data via IMovementModifier.cs to MovementHandler.cs. This data is a Y value and makes the player go Up/Jump.
/// -------------
/// Jump() is called when the player press "space" from InputController.playerInputActions.Actions.Jump.performed() Then it has to add force on AddForce() to let the player go up. Since the normal force is always lower than the preferable force. 
/// </summary>
public class ForceReceiver : MonoBehaviour, IMovementModifier
{
    //refs
    private CharacterController characterController;
    private MovementHandeler movementHandeler;
    private PlayerBow playerBow;
    private CalculateStamina calculateStamina;

    private WorldSwitchManager worldSwitchManager;
    //private TakeStamina switchEnums;

    //Settings
    private SettingsEditor settingsEditor;
    private Settings.PhysicsSettingsModel physicsModel;

    //settings

    private bool wasGroundedLastFrame; //Store lastGrounded
    private bool allowedOneExtraJump;

    private void Awake()
    {
        calculateStamina = SceneContext.Instance.calculateStamina;
        movementHandeler = SceneContext.Instance.playerManager.movementHandeler;
        worldSwitchManager = SceneContext.Instance.worldSwitchManager;
        settingsEditor = SettingsEditor.Instance;
        physicsModel = settingsEditor.physicsSettingsModel;
        playerBow = SceneContext.Instance.playerBowParent.GetComponent<PlayerBow>();
        //switchEnums = SceneContext.Instance.switchEnums;
        characterController = SceneContext.Instance.playerTransform.GetComponent<CharacterController>();
        Name = GetType().ToString();
    }

    public Vector3 Value { get; private set; }
    public string Name { get; set; }


    private void OnEnable() => movementHandeler.AddModifier(this);

    private void OnDisable() => movementHandeler.RemoveModifier(this);

    /// <summary>
    /// The update constantly checks whenever the player has jumped. If not, the Current Y value, for example 5, gets lerped to 0;
    /// </summary>
    private void Update()
    {
        //This statement checks if the player has not jumped last frame and is Grounded. 
        if (!wasGroundedLastFrame && characterController.isGrounded)
        {
            Value = new Vector3(Value.x, 0f, Value.z);
        }

        wasGroundedLastFrame = characterController.isGrounded;

        if (Value.magnitude < 0.2f)
        {
            Value = Vector3.zero;
        }


        //Lerp bcs we dont want to instant go to a point
        Value = Vector3.Lerp(Value, Vector3.zero, physicsModel.drag * Time.deltaTime);
    }

    /// <summary>
    /// This Method lets the player jump. When the player press "space" from InputController.playerInputActions.Actions.Jump.performed() this method gets called
    /// If the method is activated, The physicsModel.jumpForce gets added to the Y Value from IMovementModifier.cs. Then the Addforce() method is getting called.
    /// </summary>
    public void Jump()
    {
        if (characterController.isGrounded)
        {
            calculateStamina.state = CalculateStamina.StateEnum.JUMP;

            Vector3 force = new Vector3(0, physicsModel.jumpForce, 0);
            AddForce(force);
        }

        if (allowedOneExtraJump)
        {
            Vector3 force = new Vector3(0, physicsModel.jumpForce, 0);
            AddForce(force);
            allowedOneExtraJump = false;
            playerBow.StopArrowGrapple();
        }
    }

    /// <summary>
    /// This method checks how much force the player has jumped. Then its gets divided by the mass of the player, to see how much drag the player has to the ground.
    /// For example, the force is 10, and the forceCalc is the force(10) dived by 2. So 5 is the amount of drag. This lets the MovementPlayer.cs know how much the player has so to go down. 
    /// </summary>
    /// <param name="force"></param>
    public void AddForce(Vector3 force)
    {
        Vector3 forceCalc = force / physicsModel.mass;
        Value += Vector3.Lerp(forceCalc, Vector3.zero, 1 * Time.deltaTime);
    }

    public void SetAllowedOneExtraJump(bool set)
    {
        allowedOneExtraJump = set;
    }
}