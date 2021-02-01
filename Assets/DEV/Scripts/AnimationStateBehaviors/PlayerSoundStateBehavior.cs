using DEV.Scripts.Extensions;
using DEV.Scripts.Managers;
using UnityEngine;

namespace DEV.Scripts.AnimationStateBehaviors
{
    /// <summary>
    /// This is a small class used to play certain SFX when entering/exiting certain animation states.
    /// Since this class extends StateMachineBehaviour, it can be added as a state behavior to an animation state
    /// </summary>
    public class PlayerSoundStateBehavior : StateMachineBehaviour
    {
        /// <summary>
        /// Called when the Animator enters a state that has this script attached to it.
        /// </summary>
        /// <param name="animator">The Animator component</param>
        /// <param name="stateInfo">Info about the current animation state</param>
        /// <param name="layerIndex">N/A</param>
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (stateInfo.IsName("Shoot"))
            {
                // Instantiate a sound prefab
                Instantiate(
                    SceneContext.Instance.playerController.playerBowFireSounds.RandomElement(),
                    SceneContext.Instance.playerController.transform
                );
            }

            if (stateInfo.IsName("DrawArrow"))
            {
                Instantiate(
                    SceneContext.Instance.playerController.playerBowChargeSound,
                    SceneContext.Instance.playerController.transform
                );
            }

            if (stateInfo.IsName("Run_Blend_Forward_Left_Right"))
            {
                // Continue playing the running sound audio source
                SceneContext.Instance.playerController.playerRunningSound.Play();
            }
        }
        
        /// <summary>
        /// Called when the Animator exits a state that has this script attached to it.
        /// </summary>
        /// <param name="animator">The Animator component</param>
        /// <param name="stateInfo">Info about the current animation state</param>
        /// <param name="layerIndex">N/A</param>
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (stateInfo.IsName("Run_Blend_Forward_Left_Right"))
            {
                // Stop the audio source force running sound
                SceneContext.Instance.playerController.playerRunningSound.Stop();
            }
        }
    }
}