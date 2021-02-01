using UnityEngine;

namespace DEV.Scripts.Input
{
    /// <summary>
    /// This is the main interface for interactable objects
    /// In order for the PlayerManager to be able to interact with something,
    /// the target object needs to contain a script that implements this interface.
    /// </summary>
    public interface IInteractable
    {
        /// <summary>
        /// The interaction verb that should be displayed in the UI prompt.
        /// The format of the interaction UI prompt is: Press [E] to {InteractionVerb}
        /// where InteractionVerb is replaced by this property value
        /// </summary>
        string InteractionVerb { get; }

        /// <summary>
        /// This property points to the underlying GameObject that the implementing script is attached to
        /// </summary>
        GameObject GameObject { get; }

        /// <summary>
        /// This property can be used to determine if the object is currently interactable or not
        /// </summary>
        bool IsLocked { get; set; }

        /// <summary>
        /// This method contains the main behavior for when the player interacts with the object
        /// </summary>
        void OnPlayerInteract();

        /// <summary>
        /// This method is used by the PlayerManager to control the highlight effect
        /// </summary>
        /// <param name="enable">Should the effect be enabled?</param>
        void SetHighlight(bool enable);
    }
}