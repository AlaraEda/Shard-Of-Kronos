using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using DEV.Scripts;
using DEV.Scripts.Input;
using DEV.Scripts.Managers;
using DEV.Scripts.Puzzles.LightReflector;
using DG.Tweening;
using UnityEngine;
using Debug = UnityEngine.Debug;
/// <summary>
/// this script activates the checkpointslocation which is set by the player
/// </summary>
public class RespawnPoint : Interactable
{
    private RespawnManager respawnManager;
    private MeshRenderer meshRenderer;
    private Material material;

    private Vector3 respawnLocation;

   // private LightReflector lightReflector;

    protected override void Awake()
    {
        base.Awake();

      //  lightReflector = transform.GetChild(1).GetComponent<LightReflector>();
        respawnLocation = transform.GetChild(0).transform.position;
        respawnManager = SceneContext.Instance.respawnManager;

        // lightReflector.alwaysEmit = false;
    }

    public override void OnPlayerInteract()
    {
        respawnManager.SetRespawnActive(this.gameObject, respawnLocation);
       // lightReflector.gameObject.SetActive(true);
       // lightReflector.alwaysEmit = true;
        Debug.Log("Adding point");
    }
}