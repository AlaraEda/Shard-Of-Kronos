using System;
using System.Collections;
using System.Collections.Generic;
using ART.Shaders;
using DEV.Scripts.Managers;
using DEV.Scripts.Managers.WorldSwitching;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// This script handles the Spirit/Apocalpyse shader. 
///All objects that use this script can change when the player decides to enter the spirit world.
/// </summary>
public class GlobalSpiritShaderManager : MonoBehaviour
{
    private TreeChanger treeChanger;
    public TreeEnum shaderState;                            //Global

    //Tree Material
    public Material leaves_0;

    //Bridges Material
    public Material rockSpiritA;
    public Material rockSpiritB;

    //Bridges GameObject
    public GameObject normalRockParent;
    public GameObject spiritRockParent;

    //Array of normal objects & spirit objects
    [SerializeField] private Collider[] normalColArray;
    [SerializeField] private Collider[] spiritColArray;

    //Player Collider
    [SerializeField]private Collider playerCol;


    public enum TreeEnum
    {
        SINGLE,
        GLOBAL
    }

    /// <summary>
    /// Set Up bridge animations with DotTween & ignore every collision detection for every Spiritworld Object by default. 
    /// </summary>
    private void Awake()
    {
        treeChanger = GetComponent<TreeChanger>();
        playerCol = SceneContext.Instance.playerTransform.GetComponent<Collider>();
        SceneContext.Instance.worldSwitchManager.OnWorldSwitchEvent += ChangeMaterial;
        rockSpiritA.DOFloat(1, "_fadeRock", .1f);
        rockSpiritB.DOFloat(0, "_fadeRock", .1f);
        normalColArray = normalRockParent.transform.gameObject.GetComponentsInChildren<Collider>();
        spiritColArray = spiritRockParent.transform.gameObject.GetComponentsInChildren<Collider>();

        //Ignore collision detection for every spirit object
        foreach (var col in spiritColArray)
        {
            Physics.IgnoreCollision(playerCol, col, true);
        }

        //Don't ignore collision detection for every normal object
        foreach (var col in normalColArray)
        {
            Physics.IgnoreCollision(playerCol, col, false);
        }
    }

    /// <summary>
    /// Change the material from  Spirit to normal and from normal to spirit mode. 
    /// </summary>
    private void ChangeMaterial(object sender, WorldSwitchEventArgs args)
    {
        if (shaderState == TreeEnum.GLOBAL)
        {
            //Normal State
            if (args.NextWorld == WorldSwitchEventArgs.WorldType.Normal)
            {
                leaves_0.DOFloat(0.5f, "_Alpha", 2);
                rockSpiritA.DOFloat(1, "_fadeRock", 1);
                rockSpiritB.DOFloat(0, "_fadeRock", 1);
                foreach (var col in spiritColArray)
                {
                    Physics.IgnoreCollision(playerCol, col, true);
                }

                foreach (var col in normalColArray)
                {
                    Physics.IgnoreCollision(playerCol, col, false);
                }
            }

            //Spirit State
            else
            {
                leaves_0.DOFloat(0.78f, "_Alpha", 2);
                rockSpiritA.DOFloat(0, "_fadeRock", 1);
                rockSpiritB.DOFloat(1, "_fadeRock", 1);
                foreach (var col in spiritColArray)
                {
                    Physics.IgnoreCollision(playerCol, col, false);
                }

                foreach (var col in normalColArray)
                {
                    Physics.IgnoreCollision(playerCol, col, true);
                }
            }
        }
    }

    void OnApplicationQuit()
    {
        leaves_0.SetFloat("_Alpha", 0.5f);
        rockSpiritA.SetFloat("_fadeRock", .5f);
        rockSpiritB.SetFloat("_fadeRock", .5f);
    }
}