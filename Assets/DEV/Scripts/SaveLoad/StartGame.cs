using DEV.Scripts.Managers;
using UnityEngine;

namespace DEV.Scripts.SaveLoad
{
    /// <summary>
    /// In this script you define what you want to load first at the start of the game. This script is fired last 
    /// 
    /// </summary>

    public class StartGame : MonoBehaviour
    {
        private PlayerStatus playerStatus;
        private bool gameIsLoaded;

        private void Start()
        {
            playerStatus = SceneContext.Instance.playerStatus;
            playerStatus.Load();
        }

        public void SaveGame()
        {
            //playerStatus.Save();
        }   
        public void LoadGame()
        {
            playerStatus.Load();
        }
    }
}