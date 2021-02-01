using System;
using DEV.Scripts.Input;
using UnityEngine;

namespace DEV.Scripts.Player
{
    /// <summary>
    /// Made by Okan
    /// 
    /// This script prevents the player from walking steep slopes. It is a standalone script and sends the data via IMovementModifier.cs to MovementHandler.cs
    /// The script checks in every update 2 raycast A, B. It then calculate the Angle between these 2 point and gives a signal if the player could move on this slope.
    /// </summary>
    public class PlayerSlopeJump : MonoBehaviour, IMovementModifier
    {
        public MovementHandeler movement;
        public CharacterController controller;
        public float maxSlopeThreshold = 90;
        public GameObject slopeTarget;
        public float slopeDegrees;

        private (float slope, GameObject obj) slopeInfo;

        private Vector3 movementValue;

        public Vector3 Value => movementValue;
        public string Name { get; set; }

        private void OnEnable() => movement.AddModifier(this);
        private void OnDisable() => movement.RemoveModifier(this);

        private void Awake()
        {
            Name = GetType().Name;
            movementValue = Vector3.zero;
        }

        /// <summary>
        /// This update constantly calls the CalculateSlopeAngle. The Y value in ForceReceiver.cs gets to 1 if the player jumps. Therefore the JumpForce has to be -1 to prevent the player from jumping. 
        /// </summary>
        private void Update()
        {
            float negativeForce = SettingsEditor.Instance.physicsSettingsModel.jumpForce * -1;
            slopeInfo = CalculateSlopeAngle();
            slopeTarget = slopeInfo.obj;
            slopeDegrees = slopeInfo.slope;
            movementValue.y = slopeInfo.slope > controller.slopeLimit && slopeInfo.slope < maxSlopeThreshold
                ? negativeForce //true
                : 0;            //false
        }

        /// <summary>
        /// This method calculates the degree via a cos between two raycast points.
        /// Raycast hitA is located at the feet of the player
        /// Raycast hitB is located near the middle of the player
        /// 
        /// slopeAngle = cos -1 (length AB / length AC )
        /// AB = hitB.distance - hitA.distance
        /// AC = Vector3.Distance(trianglePointA, trianglePointC)
        /// </summary>
        /// 
        /// <returns>
        /// It returns a Degree*
        /// </returns>
        private (float slope, GameObject obj) CalculateSlopeAngle()
        {
            RaycastHit hitA;
            RaycastHit hitB;

            if (!Physics.Raycast(transform.position, transform.forward, out hitA, 2)) return (0, null);
            if (!(Physics.Raycast(transform.position + Vector3.up, transform.forward, out hitB, 2) &&
                  hitB.collider == hitA.collider)) return (0, null);

            Vector3 trianglePointA = hitA.point;
            Vector3 trianglePointC = hitB.point;

            float distAB = hitB.distance - hitA.distance;
            float distAC = Vector3.Distance(trianglePointA, trianglePointC);
            return (Mathf.Acos(distAB / distAC) * Mathf.Rad2Deg, hitA.collider.gameObject);
        }
    }
}