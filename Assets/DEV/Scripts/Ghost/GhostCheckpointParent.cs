 /*
 * GhostCheckpointParent.cs is attached to the GhostCheckpointParent.prefab, which is found in the HudCanvas.prefab.
 * This script keeps track of all the currently placed and used checkpoints.
 * It also handles updating the UI and removing/adding elements.
 */
using System.Collections.Generic;
using DEV.Scripts;
using DEV.Scripts.Managers;
using DEV.Scripts.WorldSwitching;
using UnityEngine;
using static Settings;

public class GhostCheckpointParent : MonoBehaviour
{
    //Settings
    private SettingsEditor settings;
    private GhostAndWorldSettingsModel ghostAndWorldSettings;
    private WorldSwitchManager worldSwitchManager;
    
    // Dictionary that keeps track of the checkpoint UI GameObjects and scripts (by string, based on hierarchy names + index i).
    private Dictionary<string, GameObject> checkpointUIObjects = new Dictionary<string, GameObject>();
    
    private void Awake()
    {
        // Assigning values to vars
        settings = SettingsEditor.Instance;
        ghostAndWorldSettings = settings.ghostAndWorldSettingsModel;
        worldSwitchManager = SceneContext.Instance.worldSwitchManager;

        // Instantiate new batch of UI elements.
        
            InstantiateNewUIPrefabs();
        
        
        // First do all init, then deactivate the gameobject.
        gameObject.SetActive(false);
    }
    private void Update()
    {
       if (worldSwitchManager.worldIsNormal == true)
        {
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Instantiate a new set of UI elements, and adds them to the Dictionary.
    /// </summary>
    private void InstantiateNewUIPrefabs()
    {
        for (int i = 0; i < ghostAndWorldSettings.maxCheckpoints; i++)
        {
            
            
                Vector3 pos = new Vector3(0 + (50 * i), 0, 0);
                GameObject uiCheckpointObject = Instantiate(ghostAndWorldSettings.checkpointUIPrefab, transform);
                uiCheckpointObject.GetComponent<RectTransform>().anchoredPosition = pos;
                uiCheckpointObject.name = "checkpoint_" + i;
                checkpointUIObjects.Add("checkpoint_" + i, uiCheckpointObject);
            
        }
    }

    /// <summary>
    /// Removes all GameObjects in game, that indicate a checkpoint.
    /// Also removes UI GameObjects, clears the dictionary and generates new set of UI GameObjects.
    /// This function is used after leaving the spirit world, and entering again. 
    /// </summary>
    public void RemoveAllCheckpointIndicators()
    {
        for (int i = 0; i < ghostAndWorldSettings.maxCheckpoints; i++)
        {
            checkpointUIObjects.TryGetValue("checkpoint_" + i, out GameObject tempGameObject);
            GhostCheckpointUI script = tempGameObject.GetComponent<GhostCheckpointUI>();
            Destroy(script.GetCheckpointIndicator());
            Destroy(tempGameObject);
        }
        checkpointUIObjects.Clear();
     
            InstantiateNewUIPrefabs();
          
        
        
    }

    /// <summary>
    /// Sets all the checkpoints (that are lower than index) to 'used' status, based on the parameter index.
    /// Also removes indicator GameObject from scene.
    /// </summary>
    /// <param name="index">Every checkpoint will get set to used if it is lower than given index.</param>
    public void SetCheckpointsToUsedByIndex(int index)
    {
        for (int i = 0; i < index; i++)
        {
            checkpointUIObjects.TryGetValue("checkpoint_" + i, out GameObject tempGameObject);
            GhostCheckpointUI script = tempGameObject.GetComponent<GhostCheckpointUI>();
            script.SetCheckpointToUsed();
            Destroy(script.GetCheckpointIndicator());
            
        }
    }
    
    /// <summary>
    /// Getter method that returns current dictionary filled with checkpoint information.
    /// </summary>
    public Dictionary<string, GameObject> GetCheckpointUIDictionary()
    {
        return checkpointUIObjects;
    }
}