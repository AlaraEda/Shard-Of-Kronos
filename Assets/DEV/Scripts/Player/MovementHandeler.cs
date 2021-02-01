using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using System;
using System.Collections;
using DEV.Scripts;
using DEV.Scripts.BowAndArrow;
using DEV.Scripts.Input;
using DEV.Scripts.Managers;
using DEV.Scripts.WorldSwitching;
using DG.Tweening;

namespace DEV.Scripts.Input
{
    /// <summary>
    /// This class gets all the data from IMovementModifier.cs and sends the collected data to the CharacterController.
    /// -------------
    /// Move() receives all the data from MovementPlayer.cs, ForceReceiver.cs, Gravity.cs and PlayerSlopejump.cs. 
    /// </summary>
    public class MovementHandeler : MonoBehaviour
    {
        //changingPlayerPosition op true zeetten en met new Transform de playerposition aangevenen daarna weer op false zetten
        public Vector3 TotalPlayerMovement { get; private set; }
        private CharacterController characterController;
        private Transform playerTransform;
        public bool changingPlayerPosition = false;
        public bool disableWasdMovement;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            changingPlayerPosition = true;
            playerTransform = SceneContext.Instance.playerTransform;
        }

        //This list contains all the data that is being added from the other Value scripts. 
        private readonly List<IMovementModifier> modifiers = new List<IMovementModifier>();

        private void FixedUpdate() => Move();

        public void AddModifier(IMovementModifier modifier) => modifiers.Add(modifier);
        public void RemoveModifier(IMovementModifier modifier) => modifiers.Remove(modifier);

        /// <summary>
        /// This Method receives all the different data:
        /// MovementPlayer.cs - > The X and Z value.
        /// ForceReceiver.cs - > The -Y value. 
        /// PlayerSlopejump.cs - > The -Y value. 
        /// Gravity.cs - > The +Y value.
        /// The different data all derives from the List<IMovementModifier> modifiers
        /// </summary>
        private void Move()
        {
            if (changingPlayerPosition == false)
            {
                Vector3 movement = Vector3.zero;
                foreach (IMovementModifier modifier in modifiers)
                {
                    movement += modifier.Value;
                    if (modifier.Name == "DEV.Scripts.Input.MovementPlayer" && disableWasdMovement)
                    {
                        movement -= modifier.Value;
                    }
                }

                characterController.Move(movement * Time.fixedDeltaTime);
            }
        }


        /// <summary>
        /// A simple method that (un)locks the stream of data that is getting imported in Move(). If you want to teleport the player, this method has to be called so it wont give new data and therefore disrupting the teleport vector.
        /// </summary>
        /// <param name="set"></param>
        public void SetChangingPlayerPosition(bool set)
        {
            changingPlayerPosition = set;
        }

        public List<IMovementModifier> GetMovementModifiers()
        {
            return modifiers;
        }
    }
}