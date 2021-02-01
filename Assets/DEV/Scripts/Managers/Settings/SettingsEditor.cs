using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using DEV.Scripts;
using UnityEngine;


/// <summary>
/// This script is used for saving the settings for the game elements, such as camera settings,playerhealth, bow and arrow. 
/// This script is on a gameobject which is a prefab.
/// </summary>
public class SettingsEditor : MonoBehaviour
{
    public Settings.CameraSettingsModel cameraSettingsModel;
    public Settings.MovementSettingsModel movementSettingsModel;
    public Settings.GhostAndWorldSettingsModel ghostAndWorldSettingsModel;
    public Settings.BowAndArrowSettingsModel bowAndArrowSettingsModel;
    public Settings.PlayerHealthSettingsModel playerHealthSettingsModel;
    public Settings.AudioSettingsModel audioSettingsModel;
    public Settings.PhysicsSettingsModel physicsSettingsModel;
    public Settings.PuzzleSettings puzzleSettings;
    public Settings.InventoryAndItemSettings inventorySettings;
    public Settings.EnemySettingsModel enemySettingsModel;
    

    private static SettingsEditor classInstance;
    private SaveableSettings savedSettings;
    private string settingsFilePath;
    
    // Events
    public delegate void SaveableSettingsChangedHandler(object sender, EventArgs args);

    public event SaveableSettingsChangedHandler OnSettingsSave;

    protected virtual void Awake()
    {
        DontDestroyOnLoad(gameObject);

        // Load all persistent models
        settingsFilePath = Path.Combine(Application.persistentDataPath, "Settings.json");
        savedSettings = LoadSettings();
        //Save the settings in the file, load and read the settings.
        audioSettingsModel = savedSettings.audioSettings;
        cameraSettingsModel = savedSettings.camSettings;
    }
    /// <summary>
    /// This singleton is used for holding global variables and functions that many other classes need to access
    /// </summary>
    #region Singleton Logic

    public static SettingsEditor Instance => classInstance == null ? OnInstanceCreate() : classInstance;

    private static SettingsEditor OnInstanceCreate()
    {
        var resName = "Settings";
        var prefab = Resources.Load<GameObject>(resName);
        var gameObjectInstance = Instantiate(prefab);
        gameObjectInstance.name = resName;
        classInstance = gameObjectInstance.GetComponent<SettingsEditor>();
        return classInstance;
    }

    #endregion

    #region Save/Load

    /// <summary>
    /// Save the current setting values to file
    /// </summary>
    /// <param name="settings">(Optional) The SaveableSettings struct to save</param>
    public void SaveSettings(SaveableSettings? settings = null)
    {
        if (settings == null) settings = savedSettings;
        OnSettingsSave?.Invoke(this, new EventArgs());
        var json = JsonUtility.ToJson(settings, true);
        File.WriteAllText(settingsFilePath, json);
        //PlayerPrefs.SetString(nameof(T), json);
    }

    /// <summary>
    /// Load the settings saved in the file and convert to a class instance
    /// </summary>
    /// <returns>Instance of SaveableSetttings containing the saved values</returns>
    private SaveableSettings LoadSettings()
    {
        if (!File.Exists(settingsFilePath))
        {
            var settings = new SaveableSettings(this);
            SaveSettings(settings);
            return settings;
        }

        var json = File.ReadAllText(settingsFilePath);
        return JsonUtility.FromJson<SaveableSettings>(json);
    }

    #endregion
}

/// <summary>
/// This struct is used to represent the saved settings in Settings.json
/// </summary>
public struct SaveableSettings
{
    public Settings.AudioSettingsModel audioSettings;
    public Settings.CameraSettingsModel camSettings;

    public SaveableSettings(SettingsEditor settings)
    {
        audioSettings = settings.audioSettingsModel;
        camSettings = settings.cameraSettingsModel;
    }
}