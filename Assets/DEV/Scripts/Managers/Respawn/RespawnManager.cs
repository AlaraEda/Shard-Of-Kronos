using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DEV.Scripts.Managers;
using DEV.Scripts.Puzzles.LightReflector;
using DEV.Scripts.SaveLoad;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script handles the checkpoint system.
/// </summary>
namespace DEV.Scripts
{
    /// <summary>
    /// This struct is used in the awake function. The struct will be saved in a variable and added to a list.
    /// </summary>
    [Serializable]
    public class RespawnObject
    {
        public string sceneName;
        public int index; //add later
        public GameObject respawnObject;
        public bool isActivated;
        public Vector3 vector3Transform;
    }


    public class RespawnManager : MonoBehaviour
    {
        public List<RespawnObject> respawnPointList; //List of all respawnPoints

        private PlayerStatus playerStatus;
        private StartGame startGame;
        
      // private LightReflector lightReflector;

        private void Awake()
        {
            foreach (Transform var in this.transform)
            {

                var obj = new RespawnObject
                {
                    sceneName = SceneManager.GetActiveScene().name,
                    index = var.GetSiblingIndex(),
                    vector3Transform = var.transform.GetChild(0).transform.position,
                    respawnObject = var.gameObject,
                    isActivated = false
                };
                respawnPointList.Add(obj);
            }
        }
        /// <summary>
        ///  This function is used for saving the checkpoints.
        /// </summary>
        /// <param name="gamePoint"></param>
        /// <param name="respawnLocation"></param>
        public void SetRespawnActive(GameObject gamePoint, Vector3 respawnLocation)
        {
            foreach (var var in respawnPointList)
            {
               // lightReflector = var.respawnObject.gameObject.transform.GetChild(1).GetComponent<LightReflector>();
               // lightReflector.alwaysEmit = false;
            }

            for (int i = 0; i < respawnPointList.Count; i++)
            {
                if (gamePoint.transform.GetSiblingIndex() == respawnPointList[i].index)
                {
                    respawnPointList[i].isActivated = true;
                }
                else
                {
                    respawnPointList[i].isActivated = false;
                }
            }

            playerStatus = SceneContext.Instance.playerStatus;
            playerStatus.Save();

        }
    }
}