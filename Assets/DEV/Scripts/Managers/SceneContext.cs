using System;
using System.Collections.ObjectModel;
using Cinemachine;
using DEV.Scripts.Audio;
using DEV.Scripts.Enemy;
using DEV.Scripts.Extensions;
using DEV.Scripts.Input;
using DEV.Scripts.Puzzles.LightReflector;
using DEV.Scripts.SaveLoad;
using DEV.Scripts.UI;
using DEV.Scripts.WorldSwitching;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script is on a gameobject in Unity. We use this script for references to another scripts. 
/// All the scripts are public, so we can drag and drop the gameobject in the inspector 
/// </summary>
namespace DEV.Scripts.Managers
{
    public class SceneContext : MonoBehaviour
    {
        [BoxGroup("DELETE LATER!")] public DoorClearance doorClearance;
        
        
        [BoxGroup("Scene Specific Settings")] [Tooltip("The height at which the player will be instantly killed.")]
        public float instantDeathHeight = -60;
        
        [BoxGroup("General objects")] public WorldSwitchManager worldSwitchManager;
        [BoxGroup("General objects")] public Transform objectsSpawnedByPlayer;
        [BoxGroup("General objects")] public GhostCheckpointParent ghostCheckpointParent;
        [BoxGroup("General objects")] public Transform bulletTransform;
        [BoxGroup("General objects")]public CalculateStamina calculateStamina;
        [BoxGroup("General objects")]public GhostStruct ghostStruct;
        [BoxGroup("General objects")] public PlayerController playerController;
        [BoxGroup("General objects")] public LightActivatorPillar lightActivatorPillar;
        [BoxGroup("General objects")] public MeteoorrExplode meteoorrExplode;
        [BoxGroup("General objects")] public SpawnFracturedMeteoorr spawnFracturedMeteoorr;
        [BoxGroup("General objects")] public LevelAudioManager levelAudioManager;
        [BoxGroup("Player")] public PlayerController playerManager;
        [BoxGroup("Player")] public Transform playerTransform;
        [BoxGroup("Player")] public GameObject playerBowParent;
        [BoxGroup("Player")] public PlayerStatus playerStatus;
        [BoxGroup("Player")] public GameObject ghostObject;
        



        [BoxGroup("Copy")] public MovementHandeler movementHandeler;


        [BoxGroup("UI objects")] public Canvas hudCanvas;
        [BoxGroup("UI objects")] public Canvas gameOverCanvas;

        [BoxGroup("UI objects")] [HideInInspector]
        public HudContext hudContext;

        [BoxGroup("UI objects")]
        public CheatDebugController cheatDebugController;

        [BoxGroup("UI objects")] public PauseMenu pauseMenu;
        [BoxGroup("UI objects")] public CursorLockState cursorLockState;
        [BoxGroup("UI objects")] public GameObject quiverUI;
        [BoxGroup("UI objects")] public Image bowChargeIndicator;
        //[BoxGroup("UI objects")] public TutorialUI tutorialUI;
        [BoxGroup("UI objects")] public TMP_Text bufferTxt;
        [BoxGroup("UI objects")] public Slider sliderTimeLeft;
        [BoxGroup("UI objects")] public Slider sliderStamina;
        [BoxGroup("UI objects")] public GameObject aimReticle;


        [BoxGroup("Camera objects")] public Camera mainCamera;
        [BoxGroup("Camera objects")] public CinemachineFreeLook cinemachineFreeLook;
        [BoxGroup("Camera objects")] public CinemachineFreeLook cinemachineAimCam;
        [BoxGroup("Camera objects")] public CinemachineStateDrivenCamera cinemachineStateDrivenCamera;
        [BoxGroup("Camera objects")] public Animator camAnimator;
        [BoxGroup("Camera objects")] public GameObject lookAtPlayerShoulderLocation;

        [BoxGroup("Respawn")] public RespawnManager respawnManager;
        [BoxGroup("Respawn")] public Transform startLocation;

        [BoxGroup("Enemies")] public GameObject enemySpawnParent;
        
        [BoxGroup("Ghost")]  public Transform ghostSpawner;
        [BoxGroup("Ghost")] public GhostSettings ghostSettings;
        [BoxGroup("Ghost")] public bool hasMoved;


        [BoxGroup("Spirit Shaders")] public GlobalSpiritShaderManager globalSpiritShaderManager;

        [HideInInspector] public Inventory.Inventory playerInventory;


        // Event handlers
        public delegate void OnEnemyHitHandler(object sender, EnemyHitEventArgs args);
        public event OnEnemyHitHandler OnEnemyHitEvent;

        public static SceneContext Instance { get; private set; }

        public ObservableCollection<EnemyBase> ActiveEnemies { get; private set; }


        private void Awake()
        {
            playerInventory = Resources.Load<Inventory.Inventory>("PlayerInventory");
            ActiveEnemies = new ObservableCollection<EnemyBase>();
            if (hudCanvas != null) hudContext = hudCanvas.gameObject.GetComponent<HudContext>();
            if (levelAudioManager == null)
            {
                Debug.LogError("Field levelAudioManager in SceneContext has no reference! Will attempt to manually find it, but please fix this reference ASAP!");
                levelAudioManager = FindObjectOfType<LevelAudioManager>();
            }
            Instance = this;
        }

        private void OnDestroy()
        {
            Instance = null;
        }

        public void RaiseOnEnemyHitEvent(object sender, EnemyHitEventArgs args) =>
            OnEnemyHitEvent?.Invoke(sender, args);
    }

    #region
   // [Serializable]
   // public struct TutorialUI
   // {
     //   public TMP_Text Textwasd;
     //   public TMP_Text Bow;
     //   public TMP_Text Spirit;
      //  public TMP_Text Interact;
      //  public TMP_Text Hint1;
      //  public TMP_Text Hint2;
      //  public TMP_Text Hint3;
    //}
    #endregion
}