/*
 * This script handles basic player functionality, like taking damage, handling input from InputController.cs
 * It also handles sounds for the player and calculates the stamina usage for the spirit world.
 *
 * Made by Luko, Okan & Mats
 */

using System;
using DEV.Scripts.Extensions;
using DEV.Scripts.Input;
using DEV.Scripts.Managers;
using DEV.Scripts.Player;
using DEV.Scripts.WorldSwitching;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    // Constant which is used to define the layer of objects that can be interacted with.
    private const int InteractableLayer = 17;

    // Public child objects
    public Transform ghostTransform;
    public MovementHandeler movementHandeler;
    public CharacterController charController;
    public TrailRenderer trailRenderer;
    public Transform playerTransform;
    public InputController inputController;

    // Sound effects
    public AudioSource[] playerHitSounds;
    public AudioSource[] playerBowFireSounds;
    public AudioSource playerBowChargeSound;
    public AudioSource playerRunningSound;

    private CalculateStamina calculateStamina;
    private PlayerAnimatorHandler playerAnimatorHandler;
    private PlayerController playerController;
    private GhostCheckpointParent ghostCheckPointParent;

    // Global settings
    private SettingsEditor settings;
    private Settings.GhostAndWorldSettingsModel ghostAndWorldSettings;
    private Settings.PlayerHealthSettingsModel playerHealthSettings;
    private Settings.PhysicsSettingsModel physicsModel;

    // All instances of scripts classes
    private WorldSwitchManager worldSwitchManager;
    private GhostStruct ghostStruct;

    //Interactable
    [HideInInspector] public Interactable currentInteractable;
    [SerializeField] private float interactionDistance;
    [SerializeField] private float interactionRadius = 2;
    private Camera mainCamera;
    private Ray interactRayCache = new Ray(Vector3.zero, Vector3.zero);

    //GhostSettings
    private float ghostBuffer;
    [SerializeField] private float ghostBufferCount;

    // Other private vars
    private bool playerAlreadyDied;
    private float totalTime = 5.0f;

    // Event handlers
    public delegate void OnPlayerHealthChangeHandler(object sender, PlayerHealthChangeEventArgs args);

    public delegate void OnPlayerDeath(object sender);

    public event OnPlayerHealthChangeHandler OnPlayerHealthChangeEvent;
    public event OnPlayerDeath OnPlayerDeathEvent;

    /// <summary>
    /// Controls the current player health. Changing this property will trigger OnPlayerHealthChangeEvent
    /// </summary>
    public float PlayerHealth
    {
        get => SettingsEditor.Instance.playerHealthSettingsModel.currentPlayerHealth;
        set
        {
            var newValue = value < 0 ? 0 : value;
            OnPlayerHealthChangeEvent?.Invoke(this, new PlayerHealthChangeEventArgs
            {
                NewValue = newValue,
                OldValue = playerHealthSettings.currentPlayerHealth
            });
            playerHealthSettings.currentPlayerHealth = newValue;
            if (newValue <= 0)
            {
                OnPlayerDeathEvent?.Invoke(this);
            }
        }
    }

    /// <summary>
    /// The awake function is used to assign fields and subscribe to events.
    /// </summary>
    private void Awake()
    {
        // Settings
        var settingsEditor = SettingsEditor.Instance;
        playerHealthSettings = settingsEditor.playerHealthSettingsModel;
        playerAnimatorHandler = GetComponentInChildren<PlayerAnimatorHandler>();
        //Find Scripts and components
        worldSwitchManager = SceneContext.Instance.worldSwitchManager;
        //physicsModel = settings.physicsSettingsModel;
        calculateStamina = SceneContext.Instance.calculateStamina;
        mainCamera = SceneContext.Instance.mainCamera;
        ghostStruct = GetComponent<GhostStruct>();
        playerTransform = SceneContext.Instance.playerTransform;
        trailRenderer = playerTransform.GetComponent<TrailRenderer>();
        inputController = GetComponent<InputController>();
        movementHandeler = playerTransform.GetComponent<MovementHandeler>();
        ghostCheckPointParent = SceneContext.Instance.ghostCheckpointParent;
        ghostTransform = SceneContext.Instance.ghostObject.transform;

        OnPlayerDeathEvent += PlayerDied;
        OnPlayerHealthChangeEvent += OnPlayerHealthChange;
        SettingsEditor.Instance.OnSettingsSave += OnSettingsSaved;
        charController = GetComponentInChildren<CharacterController>();

        ghostBuffer = SettingsEditor.Instance.ghostAndWorldSettingsModel.ghostBuffer;
        playerRunningSound.volume = settingsEditor.audioSettingsModel.effectsVolume *
                                    settingsEditor.audioSettingsModel.masterVolume;
    }

    /// <summary>
    /// Called by OnSettingsSave event. Is sets the component values according to the saved values.
    /// </summary>
    /// <param name="sender">Object that triggered the event.</param>
    /// <param name="args">Arguments that were passed when triggering event.</param>
    private void OnSettingsSaved(object sender, EventArgs args)
    {
        playerRunningSound.volume = SettingsEditor.Instance.audioSettingsModel.effectsVolume *
                                    SettingsEditor.Instance.audioSettingsModel.masterVolume;
    }

    #region Update

    /// <summary>
    /// Update function checks for interactable objects and if the player falls below a certain threshold, to instantly kill him when he is below the scenery.
    /// </summary>
    private void Update()
    {
        GetCurrentInteractable();
        if (playerTransform.position.y < SceneContext.Instance.instantDeathHeight && !playerAlreadyDied)
        {
            PlayerHealth = 0;
        }
    }

    /// <summary>
    /// Called by the OnPlayerDiedEvent, when player health is below 0.
    /// </summary>
    /// <param name="sender">Object that triggered the OnPlayerDiedEvent</param>
    private void PlayerDied(object sender)
    {
        inputController.OnDisable();
        playerAlreadyDied = true;
        playerAnimatorHandler.StartDeathAnimation();
        movementHandeler.changingPlayerPosition = true;
    }

    /// <summary>
    /// Called by the OnPlayerHealthChangeEvent, when player health has changed from value.
    /// </summary>
    /// <param name="sender">Object that triggered the OnPlayerHealthChangeEvent</param>
    /// <param name="args">Arguments that were passed while triggering the OnPlayerHealthChangeEvent</param>
    private void OnPlayerHealthChange(object sender, PlayerHealthChangeEventArgs args)
    {
        if (args.IsDamage)
        {
            Instantiate(playerHitSounds.RandomElement(), transform);
        }
    }

    #endregion

    #region Interactable

    /// <summary>
    /// Function to check if player is looking at an object which can be interacted with (with raycasts).
    /// If it finds an object, it calls the SetCurrentInteractable method.
    /// </summary>
    private void GetCurrentInteractable()
    {
        // Get raycast directions
        interactRayCache.origin = mainCamera.transform.position;
        interactRayCache.direction = mainCamera.transform.forward;

        // Switch case to convert 2^x layer to the corresponding int layer number
        int activeLayer = 0;
        switch (worldSwitchManager.CurrentLayer)
        {
            case 512:
                activeLayer = 9;
                break;
            case 256:
                activeLayer = 8;
                break;
        }

        // Make bitmask with found corresponding layer integer
        int bitmask = 1 << activeLayer;
        bitmask += 1 << InteractableLayer;

        // Raycast to return the object values and arguments that correspond to the interactable item.
        if (Physics.SphereCast(interactRayCache, interactionRadius, out var hit, interactionDistance, bitmask))
        {
            var interactable = hit.collider.gameObject.GetComponent<Interactable>();
            SetCurrentInteractable(interactable);
        }
        else SetCurrentInteractable(null);
    }

    /// <summary>
    /// Called when an interactable object is being hovered over. It displays hint prompts for the item that is being hovered over.
    /// </summary>
    /// <param name="interactable">Object with type 'interactable' to render the hint for.</param>
    private void SetCurrentInteractable(Interactable interactable)
    {
        if (currentInteractable != null) currentInteractable.SetHighlight(false);
        currentInteractable = interactable;
        if (interactable != null) interactable.SetHighlight(true);
        DisplayInteractionPrompt(interactable);
    }

    /// <summary>
    /// This visually applies the interactable sentence to the UI of the scene.
    /// </summary>
    /// <param name="interactable">Object with type 'interactable' to render the hint for.</param>
    private void DisplayInteractionPrompt(Interactable interactable)
    {
        if (interactable == null)
        {
            SceneContext.Instance.hudContext.InteractionText = "";
            return;
        }

        interactable.SetHighlight(true);
        SceneContext.Instance.hudContext.InteractionText = $"Press [E] to {interactable.InteractionVerb}";
    }

    #endregion

    /// <summary>
    /// Function to check stamina before switching worlds. If player has enough stamina, it will call the function
    /// 'EnterSpiritWorld' in WorldSwitchManager.cs to actually switch worlds.
    /// </summary>
    public void SwitchWorlds()
    {
        if (ghostBufferCount <= 0.1f)
        {
            ghostBufferCount = ghostBuffer;

            if (worldSwitchManager.worldIsNormal && calculateStamina.staminaCurrent >= calculateStamina.staminaMax &&
                calculateStamina.staminaCurrent >= 100)
            {
                calculateStamina.spiritTimer = 0.0f;
                worldSwitchManager.EnterSpiritWorld();
                trailRenderer.time = 1.2f;
                ghostStruct.recording = true;
                Vector3 pos = playerTransform.position;
                ghostTransform.DOMove(pos, 0);
                ghostStruct.playing = true;
            }
            else
            {
                if (!worldSwitchManager.worldIsNormal)
                {
                    worldSwitchManager.EnterNormalWorld();
                    ghostStruct.DoStopGhost();
                    ghostStruct.StopGhostFunction();
                }
            }


            DOTween.To(() => ghostBufferCount, x => ghostBufferCount = x, 0, ghostBuffer);
        }
    }

}