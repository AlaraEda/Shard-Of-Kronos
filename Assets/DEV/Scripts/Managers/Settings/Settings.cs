using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a general script where the properties can be easy adjusted for each part. 
/// For example, the properties for the playerhealth and bowarrow.
/// This script is attached to a gameobject.
/// </summary>
public static class Settings 
{

    #region - Player -
    
    [Serializable]
    public class CameraSettingsModel
    {
        [Header("Camera Settings")] 
        public float sensitivityX = 150;
        public bool invertedX = false;
        public float sensitivityY = 2;
        public bool invertedY = true;

        public float aimCamXDivider = 4;
        public float aimCamYDivider = 8;

    }
    
    [Serializable]
    public class MovementSettingsModel
    {
        [Header("Speed")] 
        public float forwardSpeed;

        public float characterRotationSmoothDamp = 0.6f;
    }
    

    
    [Serializable]
    public class BowAndArrowSettingsModel
    {
        [Header("Bow")] 
        public float chargeRate = 80;
        public float chargeMax = 100;
        public Vector3 cameraOffsetWhenDrawingBow;

        [Header("Arrows")] 
        public int startAmountArrows = 5;
        public int maxAmountArrows = 8;
        public float arrowSpeed = 100.0f;
        public float defaultArrowDamage = 5.0f;
        public GameObject defaultArrowPrefab;
        public GameObject iceAoEPrefab;
        public GameObject arrowSpawnParentPrefab;

        [Header("Special Arrow Properties")] 
        [Tooltip("The time that the Ice Area of Effect arrow is active.")]public float iceAoEArrowActiveTime = 5.0f;
        [Tooltip("The factor to divide the enemy forward and angular speeds with.")]public int iceAoEArrowSpeedDivision = 5;
        
        [Header("Grappling")] 
        public float grappleSpeed = 8.0f;
        [Tooltip("The amount to divide the gravity with, when the player is reeling in a grappling hook line.")] public float grappleGravityModifier = 2;
        [Tooltip("The distance the player needs to be from the grapple point to be able to do an extra jump.")] public float extraJumpDistance = .5f;
        

    }
    
    [Serializable]
    public class InventoryAndItemSettings
    {
        [Header("Inventory")] 
        public int maxItemStackSize = 64;

    }

    [Serializable]
    public class PlayerHealthSettingsModel
    {
        public float maxPlayerHealth = 100;
        public float currentPlayerHealth;

        public PlayerHealthSettingsModel()
        {
            currentPlayerHealth = maxPlayerHealth;
        }
    }

    [Serializable]
    public class AudioSettingsModel
    {
        public float masterVolume = 1;
        public float musicVolume = 1;
        public float effectsVolume = 1;
    }
    
    [Serializable]
    public class EnemySettingsModel
    {
        public GameObject enemyShootingProjectilePrefab;

    }

    #endregion

    #region--islandcordinates

  
    #endregion

    #region GhostSettings

    [Serializable]
    public class GhostAndWorldSettingsModel
    {
        [Header("Ghost Settings")] 
        public bool ghostWaitsAtLastCheckpoint = true;
        public int maxCheckpoints = 3;
        public float ghostDelaySpawn = 2;
        public GameObject checkpointUIPrefab;
        public GameObject checkpointLocationIndicator;
        
        [Header("World Switching Settings")] 
        public float spiritWorldTime=5;
        public float decreasingRunStamina = 10;
        public float decreasingAttackStamina = 10;
        public float decreasingJumpStamina = 10;
        public float gravityNormal = -9.81f;
        public float gravitySpirit = -5;
        public float forwardSpeedSpirit = 15;
        public float ghostBuffer = 1.5f;
    }

    #endregion


    #region Physics
    [Serializable]
        public class PhysicsSettingsModel
        {
            [Header("Jumping")]
            public float jumpForce = 5;     //jumpForce of player
            public float groundedPullMagnitude = 5;  //When you go down a slide, this float sets you down a frame, so that you dont "teleport" down words.
            
            [Header("Gravity")] 
            public float mass = 1f;     //How much mass does the player have
            public float drag = 5f;    //How much dampening for the player
        }
        #endregion

        #region Puzzles
        [Serializable]
        public class PuzzleSettings
        {
            [Header("Turning Pillars Puzzle")] 
            public bool lightBeamPillarIsCompleted = false;
            public bool turningPillarIsCompleted = false;

            public float rotateDuration = 0.5f;
        }
        

        #endregion
}
