using System;
using System.IO;
using DEV.Scripts.SaveLoad;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace DEV.Scripts.UI
{
    public class MainMenu : MonoBehaviour
    {
        public int sceneIndex;
        public Button continueGameBtn;

        public LoadingScreenControl loadingScreen;
        
        public void OnPlay(){
            string path = Path.Combine(Application.persistentDataPath, PlayerStatus.saveFileName);
            File.Delete(path);
            GC.Collect();
            //SceneManager.LoadScene(sceneIndex);
        }

        public void OnPlayResume()
        {
            string path = Path.Combine(Application.persistentDataPath, PlayerStatus.saveFileName);
            var saveFile = JsonUtility.FromJson<PlayerStatus.SaveFile>(File.ReadAllText(path));
            GC.Collect();

            StartCoroutine(loadingScreen.LoadingScreen(saveFile.currentScene));
            //SceneManager.LoadScene(saveFile.currentScene);
        }   
        public void CloseApplication()
        {
            Application.Quit();
            Debug.Log("close application");
        }

        private void Awake()
        {
            if (!File.Exists(Path.Combine(Application.persistentDataPath, PlayerStatus.saveFileName)))
            {
                continueGameBtn.GetComponentInChildren<TMP_Text>().color = Color.gray;
                continueGameBtn.interactable = false;
            }
        }
    }
}