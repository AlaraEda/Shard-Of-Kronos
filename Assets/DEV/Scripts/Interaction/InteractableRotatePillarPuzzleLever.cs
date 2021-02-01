using System.Collections;
using DEV.Scripts.Input;
using DEV.Scripts.Managers;
using UnityEngine;
using DEV.Scripts.Inventory;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

namespace DEV.Scripts.Interaction
{
    public class InteractableRotatePillarPuzzleLever : Interactable
    {
        public GameObject doorToOpen;
        public GameObject sceneLoader;
        public GameObject respawnPointAfterCompletion;
        public GameObject leverRotateable;
        
        private Inventory.Inventory inventory;
        private SettingsEditor settingsEditor;
        private Settings.PuzzleSettings puzzleSettings;

        private RespawnManager respawnManager;
        private TweenerCore<Quaternion, Vector3, QuaternionOptions> leverAction;

        protected override void Awake()
        {
            base.Awake();
            // leverAction = leverRotateable.transform.DORotate(new Vector3(0, 0, -90), 0);
            // leverAction.Complete();
            inventory = Resources.Load<Inventory.Inventory>("PlayerInventory");
            settingsEditor = SettingsEditor.Instance;
            puzzleSettings = settingsEditor.puzzleSettings;
            respawnManager = SceneContext.Instance.respawnManager;
            sceneLoader.SetActive(false);
        }

        public override void OnPlayerInteract()
        {
            if (leverAction != null)
            {
                if (leverAction.IsComplete()) return;
            }
            Debug.Log("DORotate finished!");
            leverAction = leverRotateable.transform.DORotate(new Vector3(0, 0, -40), 0.5f);
            leverAction.OnComplete(() =>
            {
                leverAction = leverRotateable.transform.DORotate(new Vector3(0, 0, -90), 0.5f);
            });
            
            if (puzzleSettings.turningPillarIsCompleted)
            {
                doorToOpen.transform
                    .DOMove(doorToOpen.transform.position - Vector3.up*5, 2)
                    .SetEase(Ease.Linear)
                    .OnComplete(() => doorToOpen.SetActive(false));
                sceneLoader.SetActive(true);
                respawnManager.SetRespawnActive(respawnPointAfterCompletion.gameObject, respawnPointAfterCompletion.transform.position);
            }

            
        }
    }
}