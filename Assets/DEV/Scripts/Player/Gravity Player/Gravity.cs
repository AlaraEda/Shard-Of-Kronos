using System;
using System.Collections;
using System.Collections.Generic;
using DEV.Scripts;
using DEV.Scripts.Input;
using DEV.Scripts.Managers;
using UnityEngine;

/// <summary>
/// This class retrieves data from InputController.cs and sends the data via IMovementModifier.cs to MovementHandler.cs. This data is a Y value and makes the player go Down.
/// -------------
/// ProcesGravity() pulls the player downwards therefore acts as a gravity force.
/// </summary>

public class Gravity : MonoBehaviour, IMovementModifier
{
    private MovementHandeler movementHandeler;
    private CharacterController characterController;

    //Settings
    private SettingsEditor settingsEditor;
    private Settings.PhysicsSettingsModel physicsModel;

    private float gravityMagnitude = Physics.gravity.y;

    private bool wasGroundedLastFrame;

    private void Awake()
    {
        characterController = SceneContext.Instance.playerManager.charController;
        movementHandeler = SceneContext.Instance.playerManager.movementHandeler;
        settingsEditor = SettingsEditor.Instance;
        physicsModel = settingsEditor.physicsSettingsModel;
        Name = GetType().ToString();
    }

    public Vector3 Value { get; set; }
    public string Name { get; set; }


    private void OnEnable() => movementHandeler.AddModifier(this);

    private void OnDisable() => movementHandeler.RemoveModifier(this);

    private void FixedUpdate() => ProcesGravity();


    /// <summary>
    /// This method pulls the player downwards therefore acts as a gravity force. If it is grounded, it constantly checks if the player
    /// is still on a ramp. If the CharacterController gave the signal that the player is not on the ground anymore, the player is being
    /// dragged down.
    /// </summary>
    private void ProcesGravity()
    {
        //If we are on the ground, we have groundedPullMagnitude, so we stay on the ramp.
        if (characterController.isGrounded)
        {
            Value = new Vector3(Value.x, -physicsModel.groundedPullMagnitude, Value.z);
        }
        //We just jumped, resets value to 0 , since groundedPullMagnitude is really low in the -
        else if (wasGroundedLastFrame)
        {
            Value = Vector3.zero;
        }
        //If we are falling
        else
        {
            Value = new Vector3(Value.x, Value.y + gravityMagnitude * Time.deltaTime, Value.z);
        }

        wasGroundedLastFrame = characterController.isGrounded;
    }

    /// <summary>
    /// Sets the player gravity downwards force. If the player is in the spirit mode, the gravity is turned up, therefore makes the player
    /// more "float".
    /// </summary>
    /// <param name="magnitude"></param>
    public void SetGravityMagnitude(float magnitude)
    {
        gravityMagnitude = magnitude;
    }
}