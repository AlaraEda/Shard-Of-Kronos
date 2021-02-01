/* PlayerArrowTip.cs
This script is attached to the tip of an arrow.
This script calls the function "StayStuckInHitObject" in the corresponding PlayerArrow instance.

Made by: Mats.
*/

using UnityEngine;

namespace DEV.Scripts.BowAndArrow
{
    public class PlayerArrowTip : MonoBehaviour
    {
        // Declaration of fields
        private PlayerArrowBase playerArrowParentScript;
        private const int ObstacleLayer = 12;
        private const int GrappleLayer = 14;
        private const int EnemyLayer = 17;
        private const int IgnoreArrowLayer = 2;

        private bool hit = false;

        /// <summary>
        /// Gets the PlayerArrowBase script attached to this gameObject.
        /// </summary>
        private void Awake()
        {
            playerArrowParentScript = GetComponentInParent<PlayerArrowBase>();
        }

        /// <summary>
        /// Called when something enters the trigger of this arrow tip.
        /// It checks whether or not the arrow has a special interaction with the layer that has been hit.
        /// It also saves the location where the arrow has hit, as a value of a property located in PlayerArrowBase.cs.
        /// </summary>
        /// <param name="other">The other collider that this arrow tip collided with.</param>
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.gameObject.name);

            if (!hit)
            {
                switch (other.gameObject.tag)
                {
                    case "Enemy":
                        hit = true;
                        Debug.Log("Ene");
                        playerArrowParentScript.StayStuckInRigidBody(other.gameObject, true);
                        break;

                    case "Box":
                        hit = true;
                        Debug.Log("Box");
                        playerArrowParentScript.StayStuckInRigidBody(other.gameObject, true);
                        break;

                    //tag zoals barrel, huis, 
                    
                    
                    
                }
                
            }

           
            

            // if (other.gameObject.layer == IgnoreArrowLayer)
            // {
            //     // Plz fill me
            // }

            // else if (other.gameObject.CompareTag("Enemy"))
            // {
            //     playerArrowParentScript.StayStuckInRigidBody(other.gameObject);
            // }
            // else
            // {
            //     playerArrowParentScript.StayStuckInHitObject();
            // }

           

            // switch (other.gameObject.layer)
            // {
            //     case ObstacleLayer:
            //
            //         if (playerArrowParentScript.ArrowType == ArrowType.IceAoE)
            //         {
            //             playerArrowParentScript.GetComponent<PlayerArrowIceAoE>().StartIceAoEEffect();
            //         }
            //
            //         break;
            //     case EnemyLayer:
            //         //playerArrowParentScript.StayStuckInHitObject();
            //         break;
            //     case GrappleLayer:
            //         playerArrowParentScript.StartGrapple(other, GrappleLayer);
            //         break;
            // }
        }
    }
}