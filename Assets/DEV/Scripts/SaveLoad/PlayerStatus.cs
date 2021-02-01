using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using DEV.Scripts.Input;
using DEV.Scripts.Managers;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using DEV.Scripts.Inventory;
using DEV.Scripts.Puzzles.LightReflector;

namespace DEV.Scripts.SaveLoad
{
    /// <summary>
    /// This script contains all the different components that needs to be saved when the player activates a checkpoint
    /// </summary>
    public class PlayerStatus : MonoBehaviour
    {
        
        //Respawn
        private RespawnManager respawnManager;
        private Transform playerTransform;
        private Transform startLocation;
        private MovementHandeler movementHandeler;

        //Scripts
        private PlayerController playerController;
        private LightActivatorPillar lightActivatorPillar;

        //Settings
        private SettingsEditor settings;
        private Settings.GhostAndWorldSettingsModel ghostAndWorldSettings;
        private Settings.PlayerHealthSettingsModel playerHealthSettings;

        //Save file & Debug
        private SaveFile saveableList;

        //[ReorderableList] [SerializeField] private List<SaveFile> _CurrentSaveFile;
        //private ObjectsToSave objectsToSave;

        //Gameover canvas
        private GameObject canvas;
        private Canvas gameOverCanvas;

        public static readonly string saveFileName = "playersave.json";


        /// <summary>
        ///  This function describes a reference to other scripts, which is needed to working.   
        /// 
        /// </summary>
        public void Awake()
        {
            //Player
            movementHandeler = SceneContext.Instance.playerTransform.GetComponent<MovementHandeler>();
            playerController = SceneContext.Instance.playerManager;
            lightActivatorPillar = SceneContext.Instance.lightActivatorPillar;
         

            //transform
            playerTransform = SceneContext.Instance.playerTransform.transform;
            respawnManager = SceneContext.Instance.respawnManager;
            startLocation = SceneContext.Instance.startLocation.transform;

            
            //Settings
            var settingsEditor = SettingsEditor.Instance;
            playerHealthSettings = settingsEditor.playerHealthSettingsModel;

            //Gameover canvas
            gameOverCanvas = SceneContext.Instance.gameOverCanvas;
            canvas = gameOverCanvas.gameObject;
            canvas.SetActive(false);
        }

        /// <summary>
        ///     This funcion will be called when the deathanimation is called in anotherscript
        ///     When this function called there is no locking to the cursor 
        ///     Also the playerposition will be changed.
        /// </summary>
        public void GameOver()
        {
            Cursor.lockState = CursorLockMode.None;
            movementHandeler.SetChangingPlayerPosition(true);
            canvas.SetActive(true);

        }

     
        /// <summary>
        ///  this function loads the scene
        /// </summary>
        public void RestartGame()
        {
            canvas.SetActive(false);
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
            //Load();

           
        }
        /// <summary>
        /// this function deletes the inventoryslots 
        /// </summary>
        public void DeleteAll()
        {
          
            respawnManager.respawnPointList.Clear();
            saveableList = new SaveFile(respawnManager);

            string path = Path.Combine(Application.persistentDataPath, saveFileName); // Where to save?
            
            var json = JsonUtility.ToJson(saveableList, true);
            File.WriteAllText(path, json);
            
            Debug.Log("save" + json);
            RestartGame();
        }
        

        /// <summary>
        /// Makes a copy of inventoryList and saves it as "/player.txt" at C:\Users\...\AppData\LocalLow\DefaultCompany\Chronicle Game 3D or add  Debug.Log(path);
        /// First the data will be picked up which we want to save, after that it will be stored in a variable and that variable will be saved in the file.
        /// </summary>
        /// <param name="path">the path, name can be what ever you want, but it has to the same in load </param>
        /// <param name="json">this is .</param>
        public void Save()
        {
           
            Debug.Log("Saving...");
            
            saveableList = new SaveFile(respawnManager);
            
            string path = Path.Combine(Application.persistentDataPath, saveFileName); //Where to save?
          
            var json = JsonUtility.ToJson(saveableList, true);
            //Debug.LogWarning($"Save file JSON:\n{json}" );
            Debug.Log(json);
            
            File.WriteAllText(path, json);
            //Debug.Log("save" + json);
        }

        /// <summary>
        /// This function checks if the file exist where the data is stored in, if so, it will read and load the data, 
        /// otherwise it will save and load the previous data, this happens in the else statement
        /// </summary>
        public void Load()
        {
            //Reset all Values
            playerController.PlayerHealth = playerHealthSettings.maxPlayerHealth;
            
          
            foreach (Transform var in transform)
            {
                Destroy(var.gameObject);
            }

            //Makes a new SaveAbleList with inventory and respawnManager values
            saveableList = new SaveFile(respawnManager);

            string path = Path.Combine(Application.persistentDataPath, saveFileName); //Where to save?
            
            Debug.Log(path);

            if (File.Exists(string.Concat(path)))
            {
                var json = File.ReadAllText(path);
                var obj = JsonUtility.FromJson<SaveFile>(json);
//                Debug.Log(json);


                //var checkBool = obj.respawnPointListStruct.Where(x => x.isActivated = true).Any();
                var activeRespawnPoints = obj.respawnPointListStruct
                    .Where(x => x.isActivated && x.sceneName == SceneManager.GetActiveScene().name)
                    .ToArray();

                foreach (var activePoint in activeRespawnPoints)
                {
                    Vector3 vector3Pos = activePoint.vector3Transform;
                    vector3Pos.y += 2;

                    playerTransform.transform.position = vector3Pos;

                    Debug.Log(playerTransform.transform.position);
                    StartCoroutine(bufferLoading());
                    break;
                }

                if (activeRespawnPoints.Length == 0)
                {
 
                    playerTransform.transform.position = startLocation.position;
                    StartCoroutine(bufferLoading());
                }
            }
            else
            {
                Debug.Log("File not found!");
                Save();
                Load();
            }
        }

        /// <summary>
        /// This function is waiting 0.2f seconds for disabling the playerposition for moving
        /// </summary>
        /// <returns>returns an IEnumerator</returns>
        private IEnumerator bufferLoading()
        {
            yield return new WaitForSeconds(0.2f);
            movementHandeler.SetChangingPlayerPosition(false);
        }


        /// <summary>
        /// Save the inventory and the respawnlocations
        /// </summary>
        [Serializable]
        public struct SaveFile
        {

            public string currentScene;
            public List<RespawnObject> respawnPointListStruct;

            public SaveFile( RespawnManager respawnManager)
            {
                currentScene = SceneManager.GetActiveScene().path;
                respawnPointListStruct = respawnManager.respawnPointList;
            }
        }
        
    }
}