using System;
using System.Collections;
using System.Collections.Generic;
using DEV.Scripts.BowAndArrow;
using DEV.Scripts.Input;
using DEV.Scripts.Managers;
using DEV.Scripts.SaveLoad;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Made by Mats
/// This class retrieves data from InputController.cs and MovementPlayer.cs and sends the corresponding animation to the Animator. 
/// </summary>
public class PlayerAnimatorHandler : MonoBehaviour
{
    // Public vars
    
    [Header("Hover over params for tooltips.")]
    [Tooltip(
        "These 3 parameters show the percentage of chance that this idle animation will be triggered after the previous idle anim has finished. Make sure these 3 vars add up to 100. Idle anim3 inspects his bow.")]
    public int idle3AnimChance = 2;

    [Tooltip("Look over his left shoulder.")]
    public int idle2AnimChance = 20;

    [Tooltip("Simple animation that bobs his head slightly.")]
    public int idle1AnimChance = 78;


    // References
    private Animator playerAnim;
    private MovementHandeler movementHandeler;
    private ForceReceiver forceReceiver;
    private CharacterController characterController;
    private PlayerStatus playerStatus;
    private SettingsEditor settingsEditor;
    private Settings.PhysicsSettingsModel physicsModel;
    private PlayerBow playerBow;
    
    // Anim param ints that get inputs from InputController.cs
    private static readonly int AnimIntMovementX = Animator.StringToHash("MovementX");
    private static readonly int AnimIntMovementZ = Animator.StringToHash("MovementZ");
    private static readonly int AnimIntMovementXZ = Animator.StringToHash("MovementXZ");
    private static readonly int AnimIntWIsPressed = Animator.StringToHash("W_IsPressed");
    private static readonly int AnimIntDIsPressed = Animator.StringToHash("D_IsPressed");
    private static readonly int AnimIntAIsPressed = Animator.StringToHash("A_IsPressed");
    private static readonly int AnimIntSIsPressed = Animator.StringToHash("S_IsPressed");
    private static readonly int AnimIntPressedJump = Animator.StringToHash("PressedJump");
    private static readonly int AnimIntHasDied = Animator.StringToHash("HasDied");
    private static readonly int AnimIntIsChargingBow = Animator.StringToHash("IsChargingBow");
    private static readonly int AnimIntDrawArrowCompleted = Animator.StringToHash("DrawArrowCompleted");
    private static readonly int AnimIntFallingForce = Animator.StringToHash("FallingForce");
    private static readonly int AnimIntIdleSelector = Animator.StringToHash("IdleSelector");
    private static readonly int AnimIntShootAnimCompleted = Animator.StringToHash("ShootAnimCompleted");
    private static readonly int AnimIntShoot = Animator.StringToHash("Shoot");

    // Lists
    private List<IMovementModifier> movementModifiers = new List<IMovementModifier>();

    // Private vars used in this script
    private Vector2 movementInput;
    private bool isJumpingInPlace;

    // Animation Controller Parameters
    private float movementX;
    private float movementZ;
    private float movementXZ;
    private bool wIsPressed;
    private bool dIsPressed;
    private bool aIsPressed;
    private bool sIsPressed;
    private bool pressedJump;
    private bool hasDied;
    private bool isChargingBow;
    private bool drawArrowCompleted;
    private float fallingForce;
    private float idleSelector = 1;

    public int checkIndex;
    private Transform transformCopy;

    private void Awake()
    {
        transformCopy = SceneContext.Instance.ghostSpawner;
        checkIndex = transformCopy.childCount;
        
        if (checkIndex == 0)
        {
            playerAnim = GetComponent<Animator>();
            movementHandeler = GetComponentInParent<MovementHandeler>();
            characterController = SceneContext.Instance.playerManager.charController;
            playerStatus = SceneContext.Instance.playerStatus;
            settingsEditor = SettingsEditor.Instance;
            physicsModel = settingsEditor.physicsSettingsModel;
            movementModifiers = movementHandeler.GetMovementModifiers();
            forceReceiver = GetComponentInParent<ForceReceiver>();
            playerBow = SceneContext.Instance.playerBowParent.GetComponent<PlayerBow>();
            playerAnim.SetFloat(AnimIntIdleSelector, idleSelector);
        }
        else
        {
            Destroy(GetComponent<Animator>());
            Destroy(this);
        }
    }

    /// <summary>
    /// Checks if the player has moved
    /// </summary>
    private void FixedUpdate()
    {
        if (checkIndex == 0)
        {
            movementX = movementInput.x;
            movementZ = movementInput.y;
            movementXZ = Mathf.Clamp(Mathf.Abs(movementX) + Mathf.Abs(movementZ), 0, 1);
            CheckWhichKeyIsPressed();
            CheckIfPlayerIsFalling();
            SetAnimatorControllerParameters();
        }
        else
        {
            Debug.Log("Object already exist");
        }
    }

    private void CheckWhichKeyIsPressed()
    {
        dIsPressed = movementX > 0.01f;
        aIsPressed = movementX < -0.01f;
        wIsPressed = movementZ > 0.01f;
        sIsPressed = movementZ < -0.01f;
        isChargingBow = playerBow.GetIsChargingBow();
        if (!isChargingBow)
        {
            drawArrowCompleted = false;
        }
    }

    private void CheckIfPlayerIsFalling()
    {
        Vector3 gravityMod = Vector3.zero;
        foreach (var mod in movementModifiers)
        {
            if (mod.Name == "Gravity")
            {
                gravityMod = mod.Value;
            }
        }

        if (!characterController.isGrounded)
        {
            fallingForce = gravityMod.y + physicsModel.groundedPullMagnitude;
            if (fallingForce < -1.0f)
            {
                pressedJump = false;
            }
        }
        else
        {
            fallingForce = 0;
        }
    }


    private void SetAnimatorControllerParameters()
    {
        playerAnim.SetFloat(AnimIntMovementX, movementX);
        playerAnim.SetFloat(AnimIntMovementZ, Mathf.Abs(movementZ));
        playerAnim.SetFloat(AnimIntMovementXZ, movementXZ);
        playerAnim.SetBool(AnimIntWIsPressed, wIsPressed);
        playerAnim.SetBool(AnimIntDIsPressed, dIsPressed);
        playerAnim.SetBool(AnimIntAIsPressed, aIsPressed);
        playerAnim.SetBool(AnimIntSIsPressed, sIsPressed);
        playerAnim.SetBool(AnimIntPressedJump, pressedJump);
        playerAnim.SetBool(AnimIntHasDied, hasDied);
        playerAnim.SetBool(AnimIntIsChargingBow, isChargingBow);
        playerAnim.SetBool(AnimIntDrawArrowCompleted, drawArrowCompleted);
        playerAnim.SetFloat(AnimIntFallingForce, fallingForce);
        if (isChargingBow && !drawArrowCompleted)
        {
            movementHandeler.disableWasdMovement = true;
        }
        else
        {
            movementHandeler.disableWasdMovement = false;
        }
    }

    private float PickRandomIdleAnimation()
    {
        var rnd = Random.Range(1, 100);
        var chosenAnim = 1.0f;
        switch (rnd)
        {
            case var x when x <= idle1AnimChance:
                chosenAnim = 1.0f;
                break;
            case var x when x > idle1AnimChance && x <= 100 - idle3AnimChance:
                chosenAnim = 2.0f;
                break;
            case var x when x > 100 - idle3AnimChance:
                chosenAnim = 3.0f;
                break;
        }

        return chosenAnim;
    }


    public void AnimEvent_AddJumpForce()
    {
        forceReceiver.Jump();
    }

    public void AnimEvent_DrawArrowCompleted()
    {
        drawArrowCompleted = true;
    }

    public void AnimEvent_ResetJumpBoolean()
    {
        pressedJump = false;
        isJumpingInPlace = false;
        movementHandeler.disableWasdMovement = false;
    }

    public void AnimEvent_IdleStarted(int index)
    {
        //Debug.Log("Start idle " + index);
        idleSelector = PickRandomIdleAnimation();
        playerAnim.SetFloat(AnimIntIdleSelector, idleSelector);
    }

    public void AnimEvent_ShootArrow()
    {
        playerBow.ShootArrow();
    }

    public void AnimEvent_ShootAnimFinished()
    {
        playerAnim.SetTrigger(AnimIntShootAnimCompleted);
    }

    public void AnimEvent_DeathFinished()
    {
        playerStatus.GameOver();
    }

    public void AnimEvent_JumpInPlaceStarted()
    {
        isJumpingInPlace = true;
        movementHandeler.disableWasdMovement = true;
    }

    public void StartDeathAnimation()
    {
        hasDied = true;
    }


    public void SetMovementInput(Vector2 value)
    {
        movementInput = value;
    }

    public void SetShootTriggerInAnimator()
    {
        playerAnim.SetTrigger(AnimIntShoot);
        drawArrowCompleted = false;
        playerAnim.SetBool(AnimIntDrawArrowCompleted, drawArrowCompleted);
    }

    public void SetJump()
    {
        if (wIsPressed && !isChargingBow)
        {
            pressedJump = true;
        }

        if (!wIsPressed && !sIsPressed && !aIsPressed && !dIsPressed && !isChargingBow)
        {
            pressedJump = true;
        }
    }
}