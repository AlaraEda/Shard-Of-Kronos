/*
 * GhostCheckpointUI.cs is attached to the each instance of a Checkpoint UI element.
 * This script keeps track of information about each checkpoint.
 * For example: If the checkpoint has been set, or used.
 * Also at which location the checkpoint has been set.
 */

using DEV.Scripts;
using DEV.Scripts.Managers;
using UnityEngine;
using static Settings;

public class GhostCheckpointUI : MonoBehaviour
{
    //Settings
    private SettingsEditor settings;
    private GhostAndWorldSettingsModel ghostAndWorldSettings;

    // Other private GameObjects and script instances
    public bool checkpointHasBeenSet;
    public bool checkpointHasBeenUsed;
    private Vector3 checkpointPos;
    private UnityEngine.UI.Image checkpointImage;
    private Transform objectsSpawnedByPlayer;
    private GameObject checkpointIndicator;

    private void Awake()
    {
        settings = SettingsEditor.Instance;
        ghostAndWorldSettings = settings.ghostAndWorldSettingsModel;
        objectsSpawnedByPlayer = SceneContext.Instance.objectsSpawnedByPlayer;
        
        checkpointImage = GetComponent<UnityEngine.UI.Image>();
    }

    /// <summary>
    /// Activates this checkpoint, so that the player can teleport to it. Also sets the UI color to blue.
    /// </summary>
    /// <param name="curPos">Location at which to set checkpoint, and where to instantiate the checkpoint indicator.</param>
    public void ActivateCheckpoint(Transform curPos)
    {
        Debug.Log("SceneContext.Instance.playerTransform.position) " + SceneContext.Instance.playerTransform.position);
        Debug.Log("curPos    " +  curPos.position);
        checkpointPos = curPos.position;
        checkpointHasBeenSet = true;
        checkpointImage.color = Color.blue;
        checkpointIndicator = Instantiate(ghostAndWorldSettings.checkpointLocationIndicator, objectsSpawnedByPlayer.transform);
        checkpointIndicator.transform.position = checkpointPos;
    }

    /// <summary>
    /// Sets this checkpoint to the status 'used', which means a player can't teleport to it again.
    /// It also changes the color of the UI from blue to red.
    /// </summary>
    public void SetCheckpointToUsed()
    {
        checkpointHasBeenUsed = true;
        checkpointImage.color = Color.red;
    }

    /// <summary>
    /// Getter method that returns the checkpoint indicator GameObject that is associated with this checkpoint.
    /// </summary>
    public GameObject GetCheckpointIndicator()
    {
        return checkpointIndicator;
    }

    /// <summary>
    /// Getter method that returns the position at which this checkpoint has been set.
    /// </summary>
    public Vector3 GetCheckpointPos()
    {
        return checkpointPos;
    }

}
