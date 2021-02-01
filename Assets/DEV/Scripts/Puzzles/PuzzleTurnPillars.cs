using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DEV.Scripts.Interaction;
using DEV.Scripts.Managers;
using DG.Tweening;
using UnityEngine;
using static Settings;

//This script is used at the start of level2. 
//The objective of the puzzle is to turn the pillars in a way that makes the light reflect through them. 
//This script sets up the Turn Pillars. 
public class PuzzleTurnPillars : MonoBehaviour
{
    //Settings
    private SettingsEditor settings;
    private PuzzleSettings puzzleSettings;
    
    // Serialized private fields
    [Header("In which rotation does every pillar need to be? Can be either 1, 2 or 3.")]
    [SerializeField] private int wantedRotationPillar1 = 2;
    [SerializeField] private int wantedRotationPillar2 = 2;
    [SerializeField] private int wantedRotationPillar3 = 3;
    
    // Pillar objects
    private List<GameObject> pillars = new List<GameObject>();
    private GameObject pillar1;
    private GameObject pillar2;
    private GameObject pillar3;
    
    // Other private vars used
    private List<int> pillarsWantedRot = new List<int>();
    private List<bool> pillarIsCorrect = new List<bool>();
    private bool cooldownStarted;
    private float curCooldown;
    private float cooldownDuration;

    
    private void Awake()
    {
        settings = SettingsEditor.Instance;
        puzzleSettings = settings.puzzleSettings;
        pillar1 = transform.GetChild(0).gameObject;
        pillar2 = transform.GetChild(1).gameObject;
        pillar3 = transform.GetChild(2).gameObject;
        pillars.Add(pillar1);
        pillars.Add(pillar2);
        pillars.Add(pillar3);
        pillarsWantedRot.Add(wantedRotationPillar1);
        pillarsWantedRot.Add(wantedRotationPillar2);
        pillarsWantedRot.Add(wantedRotationPillar3);
        pillarIsCorrect.Add(false);
        pillarIsCorrect.Add(false);
        pillarIsCorrect.Add(false);
        cooldownDuration = puzzleSettings.rotateDuration;

        PuzzlePillarInteractable script1 = pillars[0].GetComponentInChildren<PuzzlePillarInteractable>();
        script1.SetWantedRotation(wantedRotationPillar1);
        PuzzlePillarInteractable script2 = pillars[1].GetComponentInChildren<PuzzlePillarInteractable>();
        script2.SetWantedRotation(wantedRotationPillar2);
        PuzzlePillarInteractable script3 = pillars[2].GetComponentInChildren<PuzzlePillarInteractable>();
        script3.SetWantedRotation(wantedRotationPillar3);

        for (int i = 0; i < pillarsWantedRot.Count; i++)
        {
            CheckPillarRotation(i);
        }
    }

    //Update the rotation of the pillars.
    private void Update()
    {
        if (cooldownStarted)
        {
            curCooldown -= Time.deltaTime;
            if (curCooldown <= 0)
            {
                cooldownStarted = false;
            }
        }
    }

    //Check if Pillars are rotated
    private void CheckPillarRotation(int index)
    {
        var script = pillars[index].GetComponentInChildren<PuzzlePillarInteractable>();
        if (pillarsWantedRot[index] == script.GetCurrentTimesRotated())
        {
            Debug.Log(pillars[index].name + " is now in correct rotation");
            script.SetPillarIsInCorrectRotation(true);
            pillarIsCorrect[index] = true;
        }
        else
        {
            script.SetPillarIsInCorrectRotation(false);
            pillarIsCorrect[index] = false;
        }

        if (pillarIsCorrect[0] && pillarIsCorrect[1] && pillarIsCorrect[2])
        {
            Debug.Log("All pillars are correct. Puzzle solved.");
            puzzleSettings.turningPillarIsCompleted = true;
        }
    }

    //Interact with Pillars. If interacted transform the pillar rotation.
    public void InteractWithPillar(int index)
    {
        if (curCooldown <= 0)
        {
            PuzzlePillarInteractable script = pillars[index].GetComponentInChildren<PuzzlePillarInteractable>();
            Vector3 newRot = new Vector3(0,120,0);
            pillars[index].transform.DORotate(newRot, .5f, RotateMode.WorldAxisAdd);
            script.AddTimesRotated(1);
            CheckPillarRotation(index);
            curCooldown = cooldownDuration;
            cooldownStarted = true;
        }
    }
}
