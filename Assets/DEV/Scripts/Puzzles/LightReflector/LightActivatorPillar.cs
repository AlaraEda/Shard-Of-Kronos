using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using DEV.Scripts.Managers;
using UnityEngine;

namespace DEV.Scripts.Puzzles.LightReflector
{
    /// <summary>
    /// This class implements the ILightReflector interface and is meant for the pillar at the light puzzle
    /// that is supposed to receive the light ray and spawn the items needed to complete the puzzle
    /// </summary>
    public class LightActivatorPillar : MonoBehaviour, ILightReflector
    {
        // Game object representing the active state when light is received
        public GameObject activeState;
        // Game object representing state when no light is received
        public GameObject disabledState;
        // The puzzle item that needs to spawn when pillar receives correct light
        public GameObject hookItem;
        // Prefab representing the enemy to instantiate when puzzle completes
        public GameObject enemiesToInstantiate;
        // Used by respawn manager
        public GameObject repsawnPointAfterCompletion;
        
        // All light sources that are hitting this object. This is a list with events
        private ObservableCollection<ILightReflector> sources;
        private bool hintAlreadyDisplayed;
        private RespawnManager respawnManager;

        public IList<ILightReflector> LightSources => sources;
        public ILightReflector LightTarget { get; set; }
        public GameObject EmitterGameObject => gameObject;

        private void Awake()
        {
            sources = new ObservableCollection<ILightReflector>();
            sources.CollectionChanged += SourcesOnCollectionChanged;
            respawnManager = SceneContext.Instance.respawnManager;
        }
        
        /// <summary>
        /// Called when the LightSources has been modified. It will check if a tower is casting light to this and complete the puzzle
        /// Not that it will not complete the puzzle if player is in spirit world!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SourcesOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Debug.Log(nameof(SourcesOnCollectionChanged));
            var isActive = sources.Count > 0;
            activeState.SetActive(isActive);
            disabledState.SetActive(!isActive);
            if (isActive && !hintAlreadyDisplayed && SceneContext.Instance.worldSwitchManager.worldIsNormal)
            {
                SceneContext.Instance.doorClearance.SetDoor();
                hintAlreadyDisplayed = true;
                hookItem.SetActive(true);
                SceneContext
                    .Instance
                    .hudContext
                    .DisplayHint("You have completed the light pillar puzzle! You may proceed to the next area now.");
                SettingsEditor.Instance.puzzleSettings.lightBeamPillarIsCompleted = true;
                respawnManager.SetRespawnActive(repsawnPointAfterCompletion.gameObject, repsawnPointAfterCompletion.transform.position);
                
                for (int i = 1; i < 4; i++)
                {
                    var pillarPos = transform.position;
                    var pos = new Vector3(pillarPos.x - (i*3), pillarPos.y, pillarPos.z + (i*3));
                    //Instantiate(enemiesToInstantiate, pos, Quaternion.identity, SceneContext.Instance.enemySpawnParent.transform);
                }
                
            }
        }
    }
}