using System.Collections;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DEV.Scripts
{
    /// <summary>
    /// Generic class for opening and closing doors with OpenDoor() method
    /// </summary>
    public class OpenableDoor : MonoBehaviour
    {
        public GameObject leftDoor;
        public GameObject rightDoor;
        
        // Active Tween rotations
        private TweenerCore<Quaternion, Vector3, QuaternionOptions> leftDoorAction;
        private TweenerCore<Quaternion, Vector3, QuaternionOptions> rightDoorAction;
        
        /// <summary>
        /// Public method for other scripts to use in order to open the door
        /// </summary>
        /// <param name="open"></param>
        public void OpenDoor(bool open) => StartCoroutine(DoorSequence(open));
        
        /// <summary>
        /// Rotates the door in an open or close position
        /// </summary>
        /// <param name="open">True to open the door, false to close it</param>
        /// <returns>IEnumerator for StartCoroutine()</returns>
        private IEnumerator DoorSequence(bool open)
        {   
            // Wait for any existing door animation to complete
            yield return WaitForExistingAnimation();
            
            float targetRotY = open ? 90 : -90;
            leftDoorAction = leftDoor.transform.DORotate(new Vector3(0,targetRotY * -1,0), 1.5f, RotateMode.LocalAxisAdd);
            rightDoorAction = rightDoor.transform.DORotate(new Vector3(0,targetRotY,0), 1.5f, RotateMode.LocalAxisAdd);
        }
        
        /// <summary>
        /// Checks if any of the doors have an active DoRotate action
        /// and if so, return a yield instruction that waits until completion.
        /// </summary>
        /// <returns>IEnumerator that can be yielded in a Coroutine</returns>
        private IEnumerator WaitForExistingAnimation()
        {
            yield return leftDoorAction?.WaitForCompletion();
            yield return rightDoorAction?.WaitForCompletion();
        }
    }
}