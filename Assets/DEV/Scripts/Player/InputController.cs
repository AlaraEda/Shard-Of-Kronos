/*
 * This script handles all the input received by the new Unity Input System.
 * It calls the corresponding function(s) in other scripts to handle the actions that are linked to specific input.
 *
 * Made by: Mats.
 */

using DEV.Scripts.BowAndArrow;
using DEV.Scripts.Input;
using DEV.Scripts.Managers;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    // PlayerInput Setup *This is the global PlayerInputAction!*
    private PlayerInputActions playerInputActions;

    // Other script instances
    private PlayerController playerController;
    private MovementPlayer movementPlayer;
    private CursorLockState cursorLockState;
    private GhostStruct ghostStruct;
    private PlayerBow playerBow;
    private PlayerAnimatorHandler animatorHandler;
    private CalculateStamina calculateStamina;
    private CinematicCameraShotsManager shotsManager;
    private CheatDebugController cheatDebugController;

    private void Awake()
    {
        // Load global GO's
        playerController = SceneContext.Instance.playerManager;
        animatorHandler = SceneContext.Instance.playerManager.GetComponentInChildren<PlayerAnimatorHandler>();
        cursorLockState = SceneContext.Instance.cursorLockState;
        playerBow = GetComponentInChildren<PlayerBow>();
        movementPlayer = GetComponentInChildren<MovementPlayer>();
        ghostStruct = playerController.GetComponent<GhostStruct>();
        shotsManager = SceneContext.Instance.cinemachineStateDrivenCamera.GetComponent<CinematicCameraShotsManager>();
        cheatDebugController = SceneContext.Instance.cheatDebugController;

        //PlayerInput
        playerInputActions = new PlayerInputActions();

        //Read InputActions
        playerInputActions.Actions.Interact.performed += x => playerController.currentInteractable?.OnPlayerInteract();
        playerInputActions.Movement.Movement.performed += x => SetMovementInput(x.ReadValue<Vector2>());
        playerInputActions.Actions.Jump.performed += x => Jump();
        playerInputActions.Actions.SwitchWorld.performed += x => playerController.SwitchWorlds();
        playerInputActions.Actions.ChargeBowPress.performed += x => ChargeBowPress();
        playerInputActions.Actions.ChargeBowRelease.performed += x => ChargeBowRelease();
        playerInputActions.Actions.ReelGrapplePress.performed += x => ReelInGrapplePressed();
        playerInputActions.Actions.ReelGrappleRelease.performed += x => ReelInGrappleReleased();
        playerInputActions.Actions.Attack.performed += x => AttackBow();
        playerInputActions.Actions.LockCursor.performed += x => LockCursor();
        playerInputActions.Actions.UnlockCursor.performed += x => UnlockCursor();

        playerInputActions.Actions.AddCheckpoint.performed += x => AddCheckpoint();
        playerInputActions.Actions.GoBackOneCheckpoint.performed += x => GoBackOneCheckpoint();
        playerInputActions.Actions.Pause.performed += OnPause;

        playerInputActions.CinematicSkip.SkipCinematic.performed += x => SkipThisCinematic();

        playerInputActions.Cheats.ToggleCheatPanel.performed += x => ToggleCheatPanel();
        playerInputActions.Cheats.OnReturn.performed += x => UserPressedReturn();
    }

    #region Enable/Disable PlayerInputActions & Cursorlocking

    /// <summary>
    /// Specifies which action mappings need to be enabled when OnEnable() is called.
    /// </summary>
    public void OnEnable()
    {
        playerInputActions.Actions.Enable();
        playerInputActions.Cheats.Enable();
        playerInputActions.Movement.Enable();
        playerInputActions.CinematicSkip.Enable();
    }

    /// <summary>
    /// Specifies which action mappings need to be disabled when OnEnable() is called.
    /// </summary>
    public void OnDisable()
    {
        playerInputActions.Actions.Disable();
        playerInputActions.Cheats.Disable();
        playerInputActions.Movement.Disable();
    }

    /// <summary>
    /// Calls LockCursor method in CursorLockState.cs
    /// </summary>
    private void LockCursor()
    {
        cursorLockState.LockCursor();
    }

    /// <summary>
    /// Calls UnlockCursor method in CursorLockState.cs
    /// </summary>
    private void UnlockCursor()
    {
        cursorLockState.UnlockCursor();
    }

    /// <summary>
    /// Calls SkipThisCinematic method in CinematicCameraShotsManager.cs
    /// </summary>
    private void SkipThisCinematic()
    {
        shotsManager.SkipCurrentCinematic();
    }

    /// <summary>
    /// Method that is called when the player tries to pause the game.
    /// </summary>
    /// <param name="context">Callback context from the InputActions class when the pause button is pressed.</param>
    private void OnPause(InputAction.CallbackContext context)
    {
        SceneContext.Instance.pauseMenu.Toggle();
        if (!SceneContext.Instance.pauseMenu.IsPaused) LockCursor();
    }

    #endregion

    #region Bow and Arrow

    /// <summary>
    /// Called when the right mouse button has been pressed, to charge the bow.
    /// </summary>
    private void ChargeBowPress()
    {
        playerBow.SetPlayerIsChargingBow(true);
        if (playerBow.GetCurrentArrows() >= 1)
        {
            playerBow.SetActiveChargeUI(true);
        }
    }

    /// <summary>
    /// Called when the right mouse button has been released.
    /// </summary>
    private void ChargeBowRelease()
    {
        SceneContext.Instance.aimReticle.gameObject.SetActive(false);
        playerBow.SetPlayerIsChargingBow(false);
        playerBow.SetActiveChargeUI(false);
        playerBow.SetCurrentBowCharge(0);
    }


    /// <summary>
    /// Called when the left mouse button has been pressed. The ReleaseBowString() method checks if the player was charging his bow before he pressed the left mouse button.
    /// </summary>
    private void AttackBow()
    {
        playerBow.ReleaseBowString();
    }

    /// <summary>
    /// Called when the Reel In Grapple actions has been pressed ('F' key)
    /// </summary>
    private void ReelInGrapplePressed()
    {
        playerBow.SetReelInButtonIsActive(true);
    }

    /// <summary>
    /// Called when the Reel In Grapple actions has been released ('F' key)
    /// </summary>
    private void ReelInGrappleReleased()
    {
        playerBow.SetReelInButtonIsActive(false);
    }

    #endregion

    #region Ghost Trail

    /// <summary>
    /// Called when player clicks 'v' to add a checkpoint when in spirit world.
    /// </summary>
    private void AddCheckpoint()
    {
        ghostStruct.AddCheckpoint();
    }

    /// <summary>
    /// Called when player clicks 'b' to go back one checkpoint when in spirit world.
    /// </summary>
    private void GoBackOneCheckpoint()
    {
        ghostStruct.GoBackOneCheckpoint();
    }

    #endregion

    #region Movement

    /// <summary>
    /// Passes the movement input values to other scripts when the player moves using 'wasd' or a controller.
    /// </summary>
    /// <param name="inp">Movement vector.</param>
    public void SetMovementInput(Vector2 inp)
    {
        movementPlayer.SetMovementInput(inp);
        animatorHandler.SetMovementInput(inp);
    }

    /// <summary>
    /// Sets the boolean in the animation controller handler, to tell it that the player wants to jump.
    /// </summary>
    private void Jump()
    {
        animatorHandler.SetJump();
    }

    /// <summary>
    /// Opens the cheat panel when the cheat button has been pressed (Currently '~' button).
    /// It also disables all other input and unlocks/locks the cursor correspondingly.
    /// </summary>
    public void ToggleCheatPanel()
    {
        cheatDebugController.OnToggleDebug();
        if (cheatDebugController.showConsole)
        {
            playerInputActions.Actions.Disable();
            playerInputActions.Movement.Disable();
            playerInputActions.CinematicSkip.Disable();
            UnlockCursor();
        }
        else
        {
            playerInputActions.Actions.Enable();
            playerInputActions.Movement.Enable();
            playerInputActions.CinematicSkip.Enable();
            LockCursor();
        }
    }

    /// <summary>
    /// Called when user pressed the return button (to confirm the cheats that have been typed into the cheat console).
    /// </summary>
    private void UserPressedReturn()
    {
        cheatDebugController.OnReturn();
    }

    #endregion
}