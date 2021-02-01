using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Outline = Packages.QuickOutline.Scripts.Outline;

namespace DEV.Scripts.Input
{
    /// <summary>
    /// This class provides base functionality for interactable objects. To make your own interactable, create a script that inherits this class
    /// Then override the OnPlayerInteract method. After that you can attach the script to any GameObject
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public abstract class Interactable : MonoBehaviour, IInteractable
    {
        [Tooltip("The interaction verb that is displayed on the UI prompt (e.g. 'pickup Key'). The full string will look like this: Press [E] to {InteractionVerb}")]
        [SerializeField] private string interactionVerb;
        [Tooltip("Should this object get highlighted when the player looks like it?")]
        [SerializeField] private bool canHighlightObject = true;
        [Tooltip("Can this object be currently intyeracted with?")]
        [SerializeField] private bool isLocked;
        
        private Outline outline;
        
        
        /// <summary>
        /// A getter for returning the full interaction verb for the UI prompt
        /// </summary>
        public virtual string InteractionVerb => string.IsNullOrEmpty(interactionVerb)
            ? $"interact with {gameObject.name}"
            : interactionVerb;

        public virtual GameObject GameObject => gameObject;
        
        /// <summary>
        /// This property controls if the object can be interacted with or not.
        /// </summary>
        public virtual bool IsLocked
        {
            get => isLocked;
            set => isLocked = value;
        }
        
        protected virtual void Awake()
        {
            if (canHighlightObject)
            {
                // Create the highlight material if allowed
                OnHighlightCreate();
            }
        }
        
        /// <summary>
        /// This method implementation is different for each interactable item and is called when the player
        /// is looking at an interactable and presses the interaction key
        /// </summary>
        /// <exception cref="NotImplementedException">Each subclass needs to override this and provide an implementation</exception>
        public virtual void OnPlayerInteract()
        {
            throw new NotImplementedException("No interaction has been implemented for " + GetType().Name);
        }

        /// <summary>
        /// Use this method to highlight the interactable object or not.
        /// This method will only work if canHighLightObject is true.
        /// </summary>
        /// <param name="enable">bool for controlling if it should be on or off</param>
        public virtual void SetHighlight(bool enable)
        {
            if (gameObject == null || canHighlightObject == false) return;
            outline.enabled = enable;
        }
        
        /// <summary>
        /// Creates the highlighting effect that will be displayed when calling SetHighlight(true).
        /// The method will only fire if canHighlightObject is true.
        /// This method can be overriden in child classes if you want to have a different implementation.
        /// </summary>
        protected virtual void OnHighlightCreate()
        {
            outline = gameObject.AddComponent<Outline>();
            outline.OutlineMode = Outline.Mode.OutlineAndSilhouette;
            outline.OutlineColor = Color.yellow;
            outline.OutlineWidth = 5f;
            outline.DisableAfterUpdate = true;
        }
    }
}