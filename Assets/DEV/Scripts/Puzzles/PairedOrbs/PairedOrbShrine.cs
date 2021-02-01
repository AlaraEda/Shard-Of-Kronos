using System;
using System.Linq;
using DEV.Scripts.Input;
using DEV.Scripts.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DEV.Scripts.Puzzles.PairedOrbs
{
    public class PairedOrbShrine : Interactable
    {
        public OpenableDoor door;
        public GameObject pairedObjectsParent;

        private bool isAlreadyActivated;
        private PairedActivator[] allPairedObjects;

        protected override void Awake()
        {
            base.Awake();
            allPairedObjects = pairedObjectsParent.GetComponentsInChildren<PairedActivator>();
        }

        public override void OnPlayerInteract()
        {
            // Exit conditions
            
            if (allPairedObjects.Any(obj => obj.State != PairedActivator.ActivationPhase.FullyActivated))
            {
                SceneContext.Instance.hudContext.DisplayHint("In order to activate this shrine, all paired orbs must be activated!");
                return;
            }

            if (isAlreadyActivated)
            {
                SceneContext.Instance.hudContext.DisplayHint("The Shrine has already been activated!");
                door.OpenDoor(false);
                isAlreadyActivated = false;
                return;
            }
            
            // Activate the shrine since all orbs are now active
            
            SceneContext.Instance.hudContext.DisplayHint("The activation of the Shrine has opened a door!");
            isAlreadyActivated = true;
            door.OpenDoor(true);
        }
        
    }
}