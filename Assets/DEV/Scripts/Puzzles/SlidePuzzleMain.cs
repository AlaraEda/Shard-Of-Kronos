using System.Collections;
using System.Collections.Generic;
using ART.Shaders;
using DEV.Scripts.Managers;
using DEV.Scripts.Managers.WorldSwitching;
using DG.Tweening;
using UnityEngine;

public class SlidePuzzleMain : MonoBehaviour
{
    
    public Material wallMatA;
    public Material wallMatB;
    
    public GameObject normalWallParent;
    public GameObject spiritWallParent;

    private Collider playerCol;
    [SerializeField] private Collider[] normalColArray;
    [SerializeField] private Collider[] spiritColArray;
    public bool puzzleIsCompleted;

    private void Awake()
    {
        playerCol = SceneContext.Instance.playerTransform.GetComponent<Collider>();
        SceneContext.Instance.worldSwitchManager.OnWorldSwitchEvent += ChangeMaterial;
        wallMatA.DOFloat(1, "_fadeRock", .1f);
        wallMatB.DOFloat(0, "_fadeRock", .1f);
        normalColArray = normalWallParent.transform.gameObject.GetComponentsInChildren<Collider>();
        spiritColArray = spiritWallParent.transform.gameObject.GetComponentsInChildren<Collider>();
        foreach (var col in spiritColArray)
        {
            Physics.IgnoreCollision(playerCol, col, true);
            col.enabled = false;
        }

        foreach (var col in normalColArray)
        {
            Physics.IgnoreCollision(playerCol, col, false);
            col.enabled = true;
        }
    }

    private void ChangeMaterial(object sender, WorldSwitchEventArgs args)
    {
        if (args.NextWorld == WorldSwitchEventArgs.WorldType.Normal)
        {
            wallMatA.DOFloat(1, "_fadeRock", 1);
            wallMatB.DOFloat(0, "_fadeRock", 1);
            foreach (var col in spiritColArray)
            {
                Physics.IgnoreCollision(playerCol, col, true);
                col.enabled = false;
            }

            foreach (var col in normalColArray)
            {
                Physics.IgnoreCollision(playerCol, col, false);
                col.enabled = true;
            }
        }

        //Spirit State
        else
        {
            wallMatA.DOFloat(0, "_fadeRock", 1);
            wallMatB.DOFloat(1, "_fadeRock", 1);
            foreach (var col in spiritColArray)
            {
                Physics.IgnoreCollision(playerCol, col, false);
                col.enabled = true;
            }

            foreach (var col in normalColArray)
            {
                Physics.IgnoreCollision(playerCol, col, true);
                col.enabled = false;
            }
        }
    }
}
