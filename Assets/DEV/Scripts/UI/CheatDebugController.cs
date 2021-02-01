using System.Collections.Generic;
using UnityEngine;
using DEV.Scripts.Input;
using DEV.Scripts.Managers;
using DEV.Scripts.WorldSwitching;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/// <summary>
/// Main class providing the Debug console functionality.
/// It listens for input when console is open and tries to execute the correct commands based on user input.
/// </summary>
public class CheatDebugController : MonoBehaviour
{
    #region--trash

    bool handlescheat = false;
    bool handlescheat2 = false;
    bool handlescheat3 = false;
    bool handlescheat4 = false;
    bool handlescheat5 = false;
    bool handlescheat6 = false;
    bool handlescheat7 = false;
    private float totalTime = 1.0f;

    #endregion


    //refs to other scripts
    private PlayerController playerController;
    private InputController inputController;
    private CalculateStamina calculateStamina;
    private MovementPlayer movementPlayer;
    private SettingsEditor settings;
    private SceneContext sceneContext;

    private Settings.PlayerHealthSettingsModel playerHealthSettings;

    /*
     * Debug commands with no parameters
     */
    public static DebugCommand help;
    public static DebugCommand infinite_spirit;
    public static DebugCommand godmode;

    /*
     * Debug commands that have typed parameters
     */
    public static DebugCommand<int> setHealth;
    public static DebugCommand<int> speed;
    public static DebugCommand<int> jump;
    public static DebugCommand<int> damage;
    public static DebugCommand<int> lvl;

    // Collection of all active debug commands
    // Why the type is 'object' I do not know
    public List<object> commandList;
    
    public bool showConsole;
    [HideInInspector]public bool godModeActive;
    bool showHelp;
    Vector2 scrolling;
    string input;

    private void Awake()
    {
        // Get references to relevant scripts
        playerController = SceneContext.Instance.playerController;
        movementPlayer = SceneContext.Instance.playerTransform.GetComponent<MovementPlayer>();
        calculateStamina = SceneContext.Instance.calculateStamina;
        inputController = SceneContext.Instance.playerManager.GetComponent<InputController>();
        
        /*
         * All possible debug command instances are initialized below
         */

        help = new DebugCommand("help", "Shows all available commands", "help",
            () => { showHelp = !showHelp; });
        
        godmode = new DebugCommand("godmode", "Toggleable godmode.", "godmode",
            () =>
            {
                if (godModeActive)
                {
                    godModeActive = false;
                }
                else
                {
                    godModeActive = true;
                }
                Debug.Log(godModeActive);
            });

        infinite_spirit = new DebugCommand("infinite_spirit", "Gives unlimited stamina in the spirit world", "infinite_spirit",
            () => { calculateStamina.CheatInfiniteSpiritWorld(); });

        speed = new DebugCommand<int>("speed", "Sets the speed value specified",
            "speed <forward_speed>",
            (x) => { movementPlayer.SetCurrentForwardSpeed(x); }
        );

        damage = new DebugCommand<int>("damage", "Sets the arrow damage value specified",
            "damage <arrow_damage>",
            (x) => { SettingsEditor.Instance.bowAndArrowSettingsModel.defaultArrowDamage = x; }
        );

        jump = new DebugCommand<int>("jump", "Sets the jump value specified",
            "jump <jumping_force>",
            (x) => { SettingsEditor.Instance.physicsSettingsModel.jumpForce = x; }
        );
        setHealth = new DebugCommand<int>("set_health", "Sets the amount of health specified",
            "set_health <health_amount>",
            (x) => { playerController.PlayerHealth = x; }
        );
        lvl = new DebugCommand<int>("lvl", "Loads a level specified (by scene index)",
            "lvl <scene_index>",
            (x) => { if(x != 0){SceneManager.LoadScene(x);} }
        );
        
        // List containing all debug command instances
        commandList = new List<object>
        {
            help,
            infinite_spirit,
            speed,
            jump,
            damage,
            setHealth,
            lvl,
            godmode
        };
    }

    
    /// <summary>
    /// Enable/Disable the Debug console UI
    /// </summary>
    public void OnToggleDebug()
    {
        showConsole = !showConsole;
    }

    /// <summary>
    /// Proccess the input in the debug console by matching it to a debug command and invoking that command
    /// Any word that comes after the command id is treated as a parameter value
    /// </summary>
    private void HandleInput()
    {
        string[] props = input.Split(' ');
        for (int i = 0; i < commandList.Count; i++)
        { // Honestly who needs type safety in their code xD
            DebugCommandBase commandBase = commandList[i] as DebugCommandBase;
            if (input.Contains(commandBase.commandId))
            {
                if (commandList[i] as DebugCommand != null)
                {
                    (commandList[i] as DebugCommand).InvokeAction();
                }

                else if (commandList[i] as DebugCommand<int> != null)
                {
                    (commandList[i] as DebugCommand<int>).InvokeAction(int.Parse(props[1]));
                }
            }
        }
    }

    /// <summary>
    /// Take the input provided by the user in debug console and pass it to HandleInput()
    /// If user typed 'help', it will display the help info instead
    /// </summary>
    public void OnReturn()
    {
        if (!showConsole) return;
        HandleInput();
        if (!input.Contains("help"))
        {
            showHelp = false;
            inputController.ToggleCheatPanel();
        }

        input = "";
    }

    void Update()
    {
    }

    private void OnGUI()
    {
        if (!showConsole)
        {
            return;
        }

        float y = 0f;
        if (showHelp)
        {
            GUI.Box(new Rect(0, y, Screen.width, 100), "");
            Rect viewport = new Rect(0, 0, Screen.width - 30, 20 * commandList.Count);
            scrolling = GUI.BeginScrollView(new Rect(0, y + 5f, Screen.width, 90), scrolling, viewport);

            for (int i = 0; i < commandList.Count; i++)
            {
                DebugCommandBase command = commandList[i] as DebugCommandBase;
                string label = $"{command.commandFormat}   -   {command.commandDescription}";
                Rect labelRect = new Rect(5, 20 * i, viewport.width - 100, 20);
                GUI.Label(labelRect, label);
            }

            GUI.EndScrollView();
            y += 100;
        }

        GUI.Box(new Rect(0, y, Screen.width, 30), "");
        GUI.backgroundColor = new Color(0, 0, 0, 0);
        input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), input);

        GUI.Box(new Rect(0, y, Screen.width, 30),
            new GUIContent("Type 'help' for a description of available commands."));

        GUI.Label(new Rect(0, y, Screen.width, 30), GUI.tooltip);
    }
}