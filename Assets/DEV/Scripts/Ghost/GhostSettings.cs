using System;
using System.Collections;
using System.Collections.Generic;
using DEV.Scripts.Managers;
using DEV.Scripts.Managers.WorldSwitching;
using UnityEngine;
/// <summary>
/// This Class spawns the ghost copy's you see when switching to spirit mode. When the OnWorldSwitchEvent returns "SpiritWorld" it instantiate the ghost objects objects
/// </summary>
public class GhostSettings : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMeshRenderer;

    //copy gameobject
    public GameObject copy;

    [SerializeField] private float waitTime = 1;
    [SerializeField] private float deleteTime = 1;

    public Transform spawn;

    private bool spiritWorld;

    private Transform transformC;

    [SerializeField] private float ghostSpawnCooldown;
    [SerializeField] private float lastGhostSpawnCooldown;
    private bool ghostSpawnCooldownStarted = false;

    public void Start()
    {
        SceneContext.Instance.worldSwitchManager.OnWorldSwitchEvent += OnWorldSwitchEvent;
        transformC = SceneContext.Instance.playerTransform.transform;
    }

    private void OnWorldSwitchEvent(object sender, WorldSwitchEventArgs args)
    {
        if (args.NextWorld == WorldSwitchEventArgs.WorldType.Normal)
        {
            spiritWorld = false;
            ghostSpawnCooldownStarted = false;
        }
        else
        {
            spiritWorld = true;
            ghostSpawnCooldownStarted = true;
            
        }
    }

    /// <summary>
    /// When the OnWorldSwitchEvent is not NextWorld, the spiritWorld get turns true, and starts the countdown for the next ghost copy.
    /// </summary>
    private void FixedUpdate()
    {
        ghostSpawnCooldown += Time.deltaTime;

        if (spiritWorld && SceneContext.Instance.hasMoved && ghostSpawnCooldownStarted &&
            (ghostSpawnCooldown > lastGhostSpawnCooldown))
        {
            var instance = Instantiate(copy, transformC.position,
                transformC.rotation, spawn);

            ghostSpawnCooldown = 0;
        }
    }
}