/* WorldSwitchManager.cs
This global script manages the world in which the player currently resides.
It also activates fade in/out effects when switching worlds
*/

using DEV.Scripts.BowAndArrow;
using DEV.Scripts.Managers;
using DEV.Scripts.Managers.WorldSwitching;
using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace DEV.Scripts.WorldSwitching
{
    public class WorldSwitchManager : MonoBehaviour
    {
        //Set GameObjectFinder

        private CalculateStamina calculateStamina;
        //disable enemy in normalworld
        //private GameObject enemyAI;

        // Global settings
        private SettingsEditor settings;
        private Settings.GhostAndWorldSettingsModel ghostAndWorldSettings;

        //Fade DOTWeen Values
        [SerializeField] private float fadeInValue = 0.5f;
        [SerializeField] private float animDuration = 5.0f;
        [SerializeField] private Ease animEase;
        private MeshRenderer materialFade;

        // Prefabs
        [SerializeField] private GameObject shockwavePrefab;
        [SerializeField] private GameObject spiritWorldParticleEffect;

        // The Gameobjects & components that this script can access.
        private GameObject playerObject;
        private GameObject normalWorldObjectParent;

        private GameObject spiritWorldObjectParent;

        //private PlayerController playerController;
        private Camera mainCamera;

        // Used private vars
        private const int LayerNormalWorld = 8;
        private const int LayerSpiritWorld = 9;
        private const int PlayerLayer = 10;
        private GameObject playerSpiritWorldEffect;
        private float fovTransitionSpeed = 100;
        private float initialFov;
        private float ghostTargetFov;
        private Sequence fadeSeq;

        // Public vars
        [HideInInspector] public bool worldIsNormal = true;

        // Usefull for quickly getting the current world layer
        // NOTE: The value of LayerMask.GetMask("PersonA") is NOT the same as LayerNormalWorld !!
        public int CurrentLayer => worldIsNormal ? LayerMask.GetMask("PersonA") : LayerMask.GetMask("PersonB");

        public delegate void OnWorldSwitchHandler(object sender, WorldSwitchEventArgs args);

        public event OnWorldSwitchHandler OnWorldSwitchEvent;


        private ColorAdjustments colorAdjustments;
        [BoxGroup("Post Processing")] public Volume volume;

        [BoxGroup("Post Processing")] public Color normalWorldColor;
        [BoxGroup("Post Processing")] public Color spiritWorldColor;

        [BoxGroup("Spirit Shader value's")] public float decayTime;
        [BoxGroup("Spirit Shader value's")] public float revertTime;

        public float timeLerp = 0;
        private bool DoLerp;
        private float endLerp;


        //LIST OF COLLISIONS 


        //Is used before the application starts. and used even if its not enabled. 
        private void Awake()
        {
            playerObject = SceneContext.Instance.playerTransform.gameObject;
            calculateStamina = SceneContext.Instance.calculateStamina;
            settings = SettingsEditor.Instance;
            ghostAndWorldSettings = settings.ghostAndWorldSettingsModel;
            normalWorldObjectParent = transform.GetChild(0).gameObject; //IcoSfere 0
            spiritWorldObjectParent = transform.GetChild(1).gameObject; //Icosfere 1
            mainCamera = SceneContext.Instance.mainCamera;
            initialFov = mainCamera.fieldOfView;
            ghostTargetFov = initialFov + 35;
            materialFade = mainCamera.GetComponentInChildren<MeshRenderer>();
            

            if (volume.profile.TryGet<ColorAdjustments>(out colorAdjustments))
            {
                colorAdjustments.colorFilter.value = normalWorldColor;
            }
        }

        //Start wont be called if the script is not enabled. 
        private void Start()
        {
            fadeSeq = DOTween.Sequence();
            fadeSeq.SetAutoKill(false);
            fadeSeq.PrependInterval(0).SetAutoKill(false);
            SetGameObjectLayers();
            //EnterNormalWorld();
            worldIsNormal = true;

            mainCamera.cullingMask = ~(1 << LayerSpiritWorld);
        }

        /// <summary>
        /// This function is called on awake to set the correct layer for each object
        /// that is a child of the normalWorldObjectParent or the spiritWorldObjectParent.
        /// </summary>
        private void SetGameObjectLayers()
        {
            //Elke kind van Normal World transformeren
            foreach (Transform child in normalWorldObjectParent.transform)
            {
                child.gameObject.layer = LayerNormalWorld; //Pas layer van het object aan.
            }

            //Elke kind van Spirit world transformeren
            foreach (Transform child in spiritWorldObjectParent.transform)
            {
                child.gameObject.layer = LayerSpiritWorld; //Pas layer van het object aan. 
            }
        }


        private void Update()
        {
            // Debug.Log("spirittime   " + ghostAndWorldSettings.spiritWorldTime);

            if (worldIsNormal && mainCamera.fieldOfView > initialFov)
            {
                mainCamera.fieldOfView -= fovTransitionSpeed * Time.deltaTime;
            }

            if (!worldIsNormal && mainCamera.fieldOfView < ghostTargetFov)
            {
                mainCamera.fieldOfView += fovTransitionSpeed * Time.deltaTime;
            }

            if (DoLerp)
            {
                DOTween.To(() => timeLerp, x => timeLerp = x, endLerp, 5);

                if (volume.profile.TryGet<ColorAdjustments>(out colorAdjustments))
                {
                    colorAdjustments.colorFilter.value =
                        Color.Lerp(normalWorldColor, spiritWorldColor, timeLerp);
                        //Debug.Log("asd");
                }

                if (timeLerp == 1)
                {
                    DoLerp = false;
                    timeLerp = 0;
                }
            }
            
        }

        /// <summary>
        /// Switches the player to the normal world.
        /// </summary>
        public void EnterNormalWorld()
        {
            OnWorldSwitchEvent?.Invoke(this,
                new WorldSwitchEventArgs {NextWorld = WorldSwitchEventArgs.WorldType.Normal});

            //Post Processing

            endLerp = 0;
            DoLerp = true;

            if (volume.profile.TryGet<ColorAdjustments>(out colorAdjustments))
            {
                colorAdjustments.colorFilter.value = normalWorldColor;
            }

            worldIsNormal = true;


            //FadeOUT                     0.0f            5.0f
            DOTween.Complete(materialFade);
            fadeSeq.Append(materialFade.material.DOFade(0, ghostAndWorldSettings.spiritWorldTime).SetEase(animEase));

            // Enables all layers in culling mask except for layerSpiritWorld
            mainCamera.cullingMask = ~(1 << LayerSpiritWorld);

            Physics.IgnoreLayerCollision(PlayerLayer, LayerSpiritWorld, true);
            Physics.IgnoreLayerCollision(PlayerLayer, LayerNormalWorld, false);

            //Arrows

            PlayerArrowBase[] arrows = GameObject.FindObjectsOfType<PlayerArrowBase>();

            foreach (var arrow in arrows)
            {
                arrow.SwitchArrowsToNormalWorld();
            }
        }

        /// <summary>
        /// Switches the player to the spirit world.
        /// </summary>
        public void EnterSpiritWorld()
        {
            Debug.Log("Spirit");
            OnWorldSwitchEvent?.Invoke(this,
                new WorldSwitchEventArgs {NextWorld = WorldSwitchEventArgs.WorldType.Spirit});

            //Post Processing
            endLerp = 1;
            DoLerp = true;

            //set spirittimer to 0;
            calculateStamina.spiritTimer = 0.0f;
            worldIsNormal = false;

            //Set playerTransform to last position
            var playerPos = playerObject.transform.position;
            DOTween.Complete(materialFade);

            // Enables all layers in culling mask except for layerNormalWorld
            mainCamera.cullingMask = ~(1 << LayerNormalWorld);

            //playerSpiritWorldEffect.SetActive(true);
            Physics.IgnoreLayerCollision(PlayerLayer, LayerSpiritWorld, false);
            Physics.IgnoreLayerCollision(PlayerLayer, LayerNormalWorld, true);
            Instantiate(shockwavePrefab, playerPos, Quaternion.identity, playerObject.transform);
            Instantiate(spiritWorldParticleEffect, playerPos, Quaternion.identity, playerObject.transform);

            //Arrows
            PlayerArrowBase[] arrows = GameObject.FindObjectsOfType<PlayerArrowBase>();
            foreach (var arrow in arrows)
            {
                arrow.SwitchArrowsToSpiritWorld();
            }
        }
    }
}