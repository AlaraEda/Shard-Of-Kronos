using System;
using System.Collections;
using System.Collections.Generic;
using DEV.Scripts.Managers;
using DEV.Scripts.Managers.WorldSwitching;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// 2 different mats
/// Collision check
/// check what state player is in
/// layer check
/// </summary>
public class DeathSwap : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    private Color normal;
    private Color spirit;

    private BoxCollider boxCollider;

    private PlayerController playerController;

    private bool canDoDamge;


    [SerializeField] private float count;

    private float rate;

    [SerializeField] private float ghostSpawnCooldown;
    [SerializeField] private float lastGhostSpawnCooldown;
    private bool ghostSpawnCooldownStarted = false;

    private bool spiritWorld;


    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
        playerController = SceneContext.Instance.playerController;

        SceneContext.Instance.worldSwitchManager.OnWorldSwitchEvent += OnWorldSwitchEvent;

        normal = Color.red;
        spirit = Color.white;
    }

    private void OnWorldSwitchEvent(object sender, WorldSwitchEventArgs args)
    {
        if (args.NextWorld == WorldSwitchEventArgs.WorldType.Normal)
        {
            meshRenderer.material.DOColor(normal, "_FlipColor", 1);
            boxCollider.enabled = true;
            //playerController.PlayerHealth-=100;

        }
        //spirit
        else
        {
            meshRenderer.material.DOColor(spirit, "_FlipColor", 1);
            
            boxCollider.enabled = false;
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter Collider");
        canDoDamge = true;
        
        spiritWorld = true;
        ghostSpawnCooldownStarted = true;
    }

    private void OnTriggerExit(Collider other)
    {
       canDoDamge = false;
        spiritWorld = false;
        ghostSpawnCooldownStarted = false;
    }

    private void Update()
    {
        ghostSpawnCooldown += Time.deltaTime;

        if (spiritWorld && ghostSpawnCooldownStarted &&
            (ghostSpawnCooldown > lastGhostSpawnCooldown))
        {
            ghostSpawnCooldown = 0;
            playerController.PlayerHealth -= 100;
        }
        
       
    }
}