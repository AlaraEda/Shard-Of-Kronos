using System;
using System.Collections;
using System.Collections.Generic;
using DEV.Scripts;
using DEV.Scripts.Enemy;
using DEV.Scripts.Managers;
using NaughtyAttributes;
using UnityEngine;

public class OpenDoorOnEnemyDefeat : MonoBehaviour
{
    public OpenableDoor door;
    [TextArea] public string hintText = "All enemies have been killed, a door has opened.";
    public bool hasPillars = false;
    [ShowIf("hasPillars")] public List<BeamPillarPuzzle> pillars;
    private int aliveEnemies;
    private bool doorHasBeenOpened;


    private void Awake()
    {
        foreach (Transform child in transform)
        {
            aliveEnemies++;
        }
    }

    private void Update()
    {
        int alivePillars = 0;
        if (hasPillars)
        {
            foreach (var pillar in pillars)
            {
                if (!pillar.pillarIsDead) alivePillars++;
            }
        }

        if (alivePillars == 0 && aliveEnemies == 0 && doorHasBeenOpened == false)
        {
            doorHasBeenOpened = true;
            OpenDoor();
        }
    }

    private void OnTransformChildrenChanged()
    {
        aliveEnemies--;
    }


    private void OpenDoor()
    {
        SceneContext.Instance.hudContext.DisplayHint(hintText);
        door.OpenDoor(true);
    }
}