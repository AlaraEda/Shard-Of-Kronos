/* PlayerArrow.cs
This script is attached to the PlayerArrow.prefab
It handles the collision of and arrow with an object.
It also handles which arrow type has been fired (unused currently) and stores handy information, like which enemy it hit.
It also generates an impulse source, to add a camera shake effect when an arrow has been fired.

Made by: Mats.
*/

using System.Collections.Generic;
using Cinemachine;
using DEV.Scripts.Enemy;
using DEV.Scripts.Managers;
using DEV.Scripts.WorldSwitching;
using DG.Tweening;
using UnityEngine;

namespace DEV.Scripts.BowAndArrow
{
    public enum ArrowType
    {
        IceAoE,
        ImplodingArrow,
        Default
    }

    public class PlayerArrowBase : MonoBehaviour
    {
        // Declaration of properies 
        private SettingsEditor settings;
        public Settings.BowAndArrowSettingsModel BowAndArrowSettings { get; set; }
        public ArrowType ArrowType { get; set; }
        public ItemType ItemType { get; set; }
        public GameObject ArrowGfxParent { get; set; }
        public GameObject ArrowGfxNormal { get; set; }
        public WorldSwitchManager WorldSwitchManager { get; set; }
        public GameObject PlayerBowParent { get; set; }
        public PlayerBow PlayerBow { get; set; }
        public Rigidbody ArrowRb { get; set; }
        public Collider ArrowCo { get; set; }
        public bool ArrowCanDoDamage { get; set; }
        public List<GameObject> EnemiesThatHaveBeenHit { get; set; }
        public Vector3 ArrowHitLocation { get; set; }
        public float ArrowDamage { get; set; }

        private bool nearBox;

        private CinemachineImpulseSource impulseSource;

        /// <summary>
        /// Sets the values to corresponding properties.
        /// </summary>
        public virtual void Awake()
        {
            ArrowType = ArrowType.Default;
            ItemType = ItemType.Arrow;
            ArrowRb = GetComponentInChildren<Rigidbody>();
            ArrowCo = GetComponent<Collider>();
            ArrowGfxParent = gameObject.transform.GetChild(0).gameObject;
            ArrowGfxNormal = ArrowGfxParent.transform.GetChild(0).gameObject;
            WorldSwitchManager = SceneContext.Instance.worldSwitchManager;
            PlayerBowParent = SceneContext.Instance.playerBowParent;
            PlayerBow = PlayerBowParent.GetComponent<PlayerBow>();
            settings = SettingsEditor.Instance;
            BowAndArrowSettings = settings.bowAndArrowSettingsModel;
            ArrowCanDoDamage = true;
            EnemiesThatHaveBeenHit = new List<GameObject>();
            ArrowDamage = BowAndArrowSettings.defaultArrowDamage;
            impulseSource = GetComponent<CinemachineImpulseSource>();

            // Sets correct arrow visuals when the arrow is instantiated
            if (WorldSwitchManager.worldIsNormal)
            {
                SwitchArrowsToNormalWorld();
            }
            else
            {
                SwitchArrowsToSpiritWorld();
            }
        }

        /// <summary>
        /// Switches the arrow graphics to the normal world.
        /// FIXME: Add shader effect, switching world does nothing now (since there is no second art)
        /// </summary>
        public void SwitchArrowsToNormalWorld()
        {
            ArrowGfxNormal.SetActive(true);
        }

        /// <summary>
        /// Switches the arrow graphics to the spirit world.
        /// FIXME: Add shader effect, switching world does nothing now (since there is no second art)
        /// </summary>
        public void SwitchArrowsToSpiritWorld()
        {
            ArrowGfxNormal.SetActive(true);
        }

        /// <summary>
        /// This method freezes the constraints of the arrows rigidbody, so that it is stuck in the wall.
        /// It also passes the object it hit with and the arrow tip that hit the object to its parent "PlayerArrow.cs"
        /// </summary>
        public virtual void StayStuckInHitObject()
        {
            ArrowRb.constraints = RigidbodyConstraints.FreezeAll;
            ArrowCanDoDamage = false;
        }

        /// <summary>
        /// This method lets the arrow move with the rigidbody it collided with, so that it moves with that rigidbody.
        /// </summary>
        /// <param name="other">The other game object to stay stuck in.</param>
        public virtual void StayStuckInRigidBody(GameObject other, bool destroy)
        {
            var arrowGameObject = other;
            var boolDestroy = destroy;

            ArrowCo.enabled = false;
            ArrowRb.isKinematic = true;
            ArrowRb.transform.parent = arrowGameObject.gameObject.transform;

            if (boolDestroy)
            {
                Debug.Log("Destroy");
                Destroy(ArrowRb.gameObject);
            }
        }

        /// <summary>
        /// Method to actually transpose the arrow when it has been fired. Currently done by casting a ray from the middle of the camera frustum (forward).
        /// The location of this raycast is then used to retrieve a distance of how far the arrow should fly. The needed time to fly is then calculated with the arrow speed from the settings.
        ///
        /// If the second raycast has no distance, a force is applied to the rigidbody to somewhat shoot in towards that location.
        ///
        /// It also generates an impulse to shake the camera when an arrow has been shot.
        /// </summary>
        /// <param name="arrow">Arrow object on which to apply the force / transpose action.</param>
        public void AddForceToArrow(GameObject arrow, bool isNearBox)
        {
            var cam = SceneContext.Instance.mainCamera;
            Rigidbody arrowRb = arrow.GetComponent<Rigidbody>();
            Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0f));
            Physics.Raycast(cam.transform.position, ray.direction, out var hit);
            var time = hit.distance / BowAndArrowSettings.arrowSpeed;
            if (hit.distance > 0.01)
            {
                if (!PlayerBow.isNearBoxCol)
                {
                    arrow.transform.DOMove(ray.GetPoint(hit.distance), time  + (time * 1.5f));
                }
                else
                {
                    arrow.transform.DOMove(ray.GetPoint(hit.distance), time + (time * 1.5f));
                }


                arrow.transform.LookAt(hit.point);
            }
            else
            {
                arrowRb.AddForce(ray.direction.normalized * BowAndArrowSettings.arrowSpeed, ForceMode.Impulse);
            }

            impulseSource.GenerateImpulse(cam.transform.forward);
        }

        /// <summary>
        /// Called when an arrow is shot at a Unity Layer that accepts grappling. 
        /// </summary>
        /// <param name="otherObject">The other object that the arrow has been shot at.</param>
        /// <param name="grappleLayer">The layer integer to know at which layer has been shot.</param>
        public void StartGrapple(Collider otherObject, int grappleLayer)
        {
            ArrowRb.constraints = RigidbodyConstraints.FreezeAll;
            PlayerBow.EngageArrowGrapple(transform);
        }
    }
}