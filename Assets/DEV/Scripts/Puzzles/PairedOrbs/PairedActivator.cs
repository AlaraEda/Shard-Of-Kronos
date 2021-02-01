using System;
using System.Collections;
using DEV.Scripts.Input;
using UnityEngine;

namespace DEV.Scripts.Puzzles.PairedOrbs
{
    public class PairedActivator : Interactable
    {
        // The GameObject that this is paired to, must have PairedActivator script
        public PairedActivator pairedObject;
        
        // The gameobject used to represent this orb being active
        public GameObject enabledObject;
        
        // The gameobject used to represent this orb being disabled
        public GameObject disabledObject;
        
        // The max time that the orb can stay activated while the pair is inactive
        public float activationDuration = 3;
        
        // Current state of this orb
        private ActivationPhase state;

        // Events
        public delegate void OnStateSwitchHandler(object sender);
    
        public event OnStateSwitchHandler OnStateSwitch;

        public enum ActivationPhase
        {
            NotActive,
            AwaitingActivation,
            FullyActivated
        }
        
        /// <summary>
        /// Property for getting/setting current state
        /// Note that the setter is private
        /// </summary>
        public ActivationPhase State
        {
            get => state;
            private set
            {
                state = value;
                DisplayActive(value != ActivationPhase.NotActive);
                OnStateSwitch?.Invoke(this);
            }
        }

        /// <summary>
        /// Will activate the orb and run the CheckPairedActivation() routine
        /// </summary>
        public override void OnPlayerInteract()
        {
            if (State == ActivationPhase.AwaitingActivation ||
                State == ActivationPhase.FullyActivated) return;

            State = pairedObject.State == ActivationPhase.NotActive
                ? ActivationPhase.AwaitingActivation
                : ActivationPhase.FullyActivated;
            StartCoroutine(CheckPairedActivation());
        }

        protected override void Awake()
        {
            base.Awake();
            if (pairedObject == null)
            {
                Debug.LogException(new UnassignedReferenceException(
                    $"The Paired Obj value of {gameObject.name} is not assigned! Script will now disable!"));
                enabled = false;
            }

            // Debug only
            //OnStateSwitch += sender => Debug.Log($"{gameObject.name} new state: {State}");
        }
        
        /// <summary>
        /// Waits x amount of seconds (determined by activationDuration) and then disabled the orb
        /// if the paired orb is not active. Otherwise orb will remain active
        /// </summary>
        /// <returns></returns>
        private IEnumerator CheckPairedActivation()
        {
            yield return new WaitForSeconds(activationDuration);
            State = pairedObject.State == ActivationPhase.FullyActivated
                ? ActivationPhase.FullyActivated
                : ActivationPhase.NotActive;
        }
        
        /// <summary>
        /// Utility method for activating/deactivating this orb
        /// It will enable/disable the relevant game object based on argument
        /// </summary>
        /// <param name="enable">Is the orb activated?</param>
        private void DisplayActive(bool enable)
        {
            enabledObject.SetActive(enable);
            disabledObject.SetActive(!enable);
        }
    }
}