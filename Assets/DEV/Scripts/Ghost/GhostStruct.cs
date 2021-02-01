/*
 * GhostStruct.cs is attached to the Player Manager prefab. It handles activation of the ghost that follows the player.
 * It also handles checkpoint adding / player teleportation to checkpoints.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using DEV.Scripts;
using DEV.Scripts.Input;
using DEV.Scripts.Managers;
using DEV.Scripts.WorldSwitching;
using DG.Tweening;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static Settings;


/// <summary>
/// This struct contains the position and rotation of the player
/// </summary>
[Serializable]
public struct GhostTransform
{
    public Vector3 position;
    public Quaternion rotation;

    public GhostTransform(Transform transform)
    {
        position = transform.position;
        rotation = transform.rotation;
    }
}

public class GhostStruct : MonoBehaviour
{
    //Settings
    private SettingsEditor settings;
    private MovementSettingsModel moveSettings;
    private GhostAndWorldSettingsModel ghostAndWorldSettings;

    // Other Script instances
    private Gravity gravity;
    private MovementPlayer movementPlayer;
    private MovementHandeler movementHandeler;
    private WorldSwitchManager worldSwitchManager;
    private GhostCheckpointParent ghostCheckpointParent;

    // Public vars, read by other scripts
    [HideInInspector] public bool recording;
    [HideInInspector] public bool playing;
    [HideInInspector] public bool bufferDone = true;

    // private UI vars
    private Canvas hudCanvas;
    private TMP_Text bufferText;
    private Slider sliderTimeLeft;
    private Slider staminaPlayer;

    // spirit world private vars
    private float gravitySpirit;
    private float movementSpeedSpirit;

    // normal world private vars
    private float gravityNormal;
    private float movementSpeedNormal;

    // Other private objects and vars
    private TrailRenderer trailRenderer;
    private Transform playerTransform;
    private Transform ghostTransform;
    [HideInInspector] public List<GhostTransform> recordedGhostTransform;
    private IEnumerator stopGhost;
    private Sequence sliderSeq;
    private Tween sliderTween;
    private bool playerCanAddCheckpoint;
    private bool breakMoveGhostLoop;

    //Stamina system variables
    //private float maxStamina=100;
    //private float currentStamina = 100;

    private SkinnedMeshRenderer skinnedMeshRenderer;

    private void Awake()
    {
        var player = SceneContext.Instance.playerTransform.gameObject;
        settings = SettingsEditor.Instance;
        gravity = player.GetComponent<Gravity>();
        worldSwitchManager = SceneContext.Instance.worldSwitchManager;
        ghostCheckpointParent = SceneContext.Instance.ghostCheckpointParent;
        moveSettings = settings.movementSettingsModel;
        ghostAndWorldSettings = settings.ghostAndWorldSettingsModel;
        trailRenderer = player.GetComponent<TrailRenderer>();
        movementPlayer = player.GetComponent<MovementPlayer>();
        movementHandeler = SceneContext.Instance.playerManager.movementHandeler;
        gravityNormal = ghostAndWorldSettings.gravityNormal;
        movementSpeedNormal = moveSettings.forwardSpeed;
        gravitySpirit = ghostAndWorldSettings.gravitySpirit;
        movementSpeedSpirit = ghostAndWorldSettings.forwardSpeedSpirit;
        playerTransform = SceneContext.Instance.playerTransform;
        ghostTransform = SceneContext.Instance.playerManager.ghostTransform;
        // First do all the setup, then deactivate ghost and trail renderer
        trailRenderer.enabled = false;
        ghostTransform.gameObject.SetActive(false);
    }

    private void Start()
    {
        // Setup of UI elements
        sliderSeq = DOTween.Sequence();
        sliderSeq.SetAutoKill(false);
        sliderSeq.PrependInterval(0).SetAutoKill(false);
        stopGhost = StopGhost();
        hudCanvas = SceneContext.Instance.hudCanvas;
        if (hudCanvas == null)
            throw new NullReferenceException("Cant find HudCanvas Tag!");

        bufferText = SceneContext.Instance.bufferTxt;
        if (bufferText == null)
            throw new NullReferenceException("Cant find bufferText");
        sliderTimeLeft = SceneContext.Instance.sliderTimeLeft;
        staminaPlayer = SceneContext.Instance.sliderStamina;
        //Buffer Text
        //bufferText.text = "Switchable";
        //bufferText.color = Color.green;
        // currentStamina = maxStamina;
    }

    private void Update()
    {
        //Warning! You also record when standing still! Need to fix this!
        // This starts initial recording of player movement.
        if (recording)
        {
            var newGhostTransform = new GhostTransform(playerTransform);
            recordedGhostTransform.Add(newGhostTransform);
        }

        // To start the movement of the ghost, is called when enough locations have been recorded (based on ghostDelaySpawn).
        if (playing)
        {
            Play();
        }
    }
   

    /// <summary>
    /// Starts coroutine to begin moving the ghost.
    /// </summary>
    private void Play()
    {
        StartCoroutine(StartGhost());
        playing = false;
    }

    /// <summary>
    /// Sets player stats (like gravity & movement speed) to spirit world stats.
    /// </summary>
    private void DoWorldEffects()
    {
        gravity.SetGravityMagnitude(gravitySpirit);
        movementPlayer.SetCurrentForwardSpeed(movementSpeedSpirit);
    }

    /// <summary>
    /// Sets player stats (like gravity & movement speed) back to normal world stats.
    /// </summary>
    private void RevertDoWorldEffects()
    {
        gravity.SetGravityMagnitude(gravityNormal);
        movementPlayer.SetCurrentForwardSpeed(movementSpeedNormal);
    }

    /// <summary>
    /// Starts activating ghost. When the yield return wait (ghostDelaySpawn) is finished, the ghost will start moving.
    /// Also enables UI for checkpoints and activates the ghost gameobject. 
    /// </summary>
    private IEnumerator StartGhost()
    {
        trailRenderer.enabled = true;
        trailRenderer.DOTime(ghostAndWorldSettings.ghostDelaySpawn + 0.1f, 0); //Sets Trail
        DoWorldEffects(); //Sets Spirit Effects

        //bufferText.text = "Not Switchable!"; //Sets buffer text
        // bufferText.color = Color.red;
        // bufferDone = false;

        yield return new WaitForSeconds(ghostAndWorldSettings.ghostDelaySpawn); //Waits for ghost spawn delay time
        // StartCoroutine(StopGhost()); //Starts ghost following
        playerCanAddCheckpoint = true;
        ghostCheckpointParent.gameObject.SetActive(true);
        bufferDone = true; //can press c again
        // bufferText.text = "Switchable";
        // bufferText.color = Color.green;

        //ghostTransform.gameObject.SetActive(true);

        StartCoroutine(MoveGhost());
    }

    /// <summary>
    /// Coroutine to start moving the ghost. Can be stopped anytime by setting breakMoveGhostLoop to true.
    /// </summary>
    private IEnumerator MoveGhost()
    {
        //Moves for each list location
        for (int i = 0; i < recordedGhostTransform.Count; i++)
        {
            if (breakMoveGhostLoop == false)
            {
                ghostTransform.position = recordedGhostTransform[i].position;
                ghostTransform.rotation = recordedGhostTransform[i].rotation;
                yield return new WaitForSeconds(0.001f * Time.deltaTime);
            }
            else
            {
                breakMoveGhostLoop = false; //sets it to false as we already know to break the loop
                break; //breaks the loop
            }
        }
    }

    /// <summary>
    /// Calls all necessary functions to prematurely exit the whole ghost function, and return back to normal world. 
    /// </summary>
    public void StopGhostFunction()
    {
        //DOTween.Complete(sliderTimeLeft); //Complete the slider to 0, makes the wait for completion 0 sec
        //DOTween.Complete(staminaPlayer);
        playerCanAddCheckpoint = false;
        ghostCheckpointParent.RemoveAllCheckpointIndicators();
        ghostCheckpointParent.gameObject.SetActive(false);
    }

    /// <summary>
    /// Handles stopping all the ghost functionality.
    /// Returns player back to normal world and remove all objects based around the checkpoint system.
    /// </summary>
    public IEnumerator StopGhost()
    {
        RevertDoWorldEffects();
        recording = false;
        recordedGhostTransform.Clear();
        var pos = ghostTransform.position;
        ghostTransform.gameObject.SetActive(false);
        trailRenderer.DOTime(0f, 0f);
        var activeCheckpointCount = 0;
        foreach (var checkpoint in ghostCheckpointParent.GetCheckpointUIDictionary())
        {

            var script = checkpoint.Value.GetComponent<GhostCheckpointUI>();
            if (script.checkpointHasBeenSet)
            {
                activeCheckpointCount++;
            }
        }
        if (activeCheckpointCount > 0)
        {
            GoBackOneCheckpoint();
        }
        
        playerCanAddCheckpoint = false;
        trailRenderer.enabled = false;
        StopCoroutine(stopGhost);
        yield return null; 
    }

    public void DoStopGhost()
    {
        StartCoroutine(StopGhost());
    }

    /// <summary>
    /// Activated from InputController. Adds a checkpoint on the current player location. Also adds in UI elements.
    /// </summary>
    public void AddCheckpoint()
    {
        if (playerCanAddCheckpoint)
        {
            // Get dict with all currently placed checkpoints
            Dictionary<string, GameObject> checkpoints = ghostCheckpointParent.GetCheckpointUIDictionary();
            for (int i = 0; i < ghostAndWorldSettings.maxCheckpoints; i++)
            {
                // Retrieve GameObject (so script and UI), linked to the retrieved checkpoint. 
                checkpoints.TryGetValue("checkpoint_" + i, out GameObject tempGameObject);
                GhostCheckpointUI script = tempGameObject.GetComponent<GhostCheckpointUI>();

                // If the currently checked checkpoint has already been set, try next one.
                if (script.checkpointHasBeenSet)
                {
                    //Debug.Log("Checkpoint " + i + " already has been set. Trying next one.");
                }
                else
                {
                    // If the ghost has to wait at last placed checkpoint, teleport the ghost to last checkpoint.
                    // Also deactivates all previous checkpoints and activates newest checkpoint.
                    if (ghostAndWorldSettings.ghostWaitsAtLastCheckpoint)
                    {
                        if (!recording)
                        {
                            ghostCheckpointParent.SetCheckpointsToUsedByIndex(i);
                        }

                        recording = false;
                        breakMoveGhostLoop = true;

                        script.ActivateCheckpoint(SceneContext.Instance.playerTransform);

                        //SceneContext.Instance.skinnedMeshRendererCopy =  SceneContext.Instance.skinnedMeshRenderer;

                        ghostTransform.position = SceneContext.Instance.playerTransform.position;


                        trailRenderer.DOTime(0f, 0f);
                        trailRenderer.DOTime(ghostAndWorldSettings.ghostDelaySpawn + 0.1f, 0);
                    }
                    // else, just activate the newest checkpoint.
                    else
                    {
                        script.ActivateCheckpoint(SceneContext.Instance.playerTransform);
                    }

                    break;
                }
            }
        }
    }

    /// <summary>
    /// Teleports player back to the last activated checkpoint.
    /// </summary>
    public void GoBackOneCheckpoint()
    {
        if (playerCanAddCheckpoint)
        {
            // Get dict with all currently placed checkpoints
            Dictionary<string, GameObject> checkpoints = ghostCheckpointParent.GetCheckpointUIDictionary();
            var upperVal = ghostAndWorldSettings.maxCheckpoints - 1;
            for (int i = upperVal; i >= 0; i--)
            {
                // Retrieve GameObject (so script and UI), linked to the retrieved checkpoint. 
                checkpoints.TryGetValue("checkpoint_" + i, out GameObject tempGameObject);
                GhostCheckpointUI script = tempGameObject.GetComponent<GhostCheckpointUI>();

                // If the currently checked checkpoint has already been set, and player has not teleported to it yet.
                if (script.checkpointHasBeenSet && !script.checkpointHasBeenUsed)
                {
                    // If the ghost has to wait at last placed checkpoint, teleport player to that point
                    // Also disables complete ghost functionality (after teleport) if current checkpoint is also the last checkpoint. 
                    if (ghostAndWorldSettings.ghostWaitsAtLastCheckpoint)
                    {
                        Debug.Log("TP player back");
                        StartCoroutine(TeleportBackOneCheckpoint(ghostTransform.localPosition));
                        if (i == upperVal)
                        {
                            StopGhostFunction();
                        }
                    }
                    // If the ghost does not have to wait at last checkpoint, just teleport player to last checkpoint.
                    else
                    {
                        StartCoroutine(TeleportBackOneCheckpoint(script.GetCheckpointPos()));
                    }

                    // Whatever happens above, always set current checkpoint to used, and remove indicator GameObject (visible in game)
                    // This also sets the UI display to used. 
                    script.SetCheckpointToUsed();
                    Destroy(script.GetCheckpointIndicator());
                    break;
                }
            }
        }
    }

    /// <summary>
    /// Handles teleporting player to a specified location. It disables the movementPlayer to overcome a bug.
    /// </summary>
    /// <param name="localPos">Vector3 position to teleport the player to.</param>
    private IEnumerator TeleportBackOneCheckpoint(Vector3 localPos)
    {
        movementHandeler.SetChangingPlayerPosition(true);
        Tween tp = playerTransform.DOMove(localPos, 0.1f);
        yield return tp.WaitForCompletion();
        movementHandeler.SetChangingPlayerPosition(false);
    }
}