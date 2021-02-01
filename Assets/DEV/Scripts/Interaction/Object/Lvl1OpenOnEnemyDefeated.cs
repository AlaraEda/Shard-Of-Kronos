using System;
using System.Collections;
using System.Collections.Generic;
using DEV.Scripts.EnemyPillar;
using DEV.Scripts.Managers;
using UnityEngine;

public class Lvl1OpenOnEnemyDefeated : MonoBehaviour
{
    public GameObject sceneLoader;
    public GameObject doorLeftClosed;
    public GameObject doorLeftOpen;
    public GameObject doorRightClosed;
    public GameObject doorRightOpen;
    public GameObject doorVisual;
    public BeamPillarPuzzle[] pillars;
    private bool alreadyOpened;

    private void Awake()
    {
        doorVisual.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!alreadyOpened)
        {
            int alivePillars = 0;
            foreach (var pillar in pillars)
            {
                if (!pillar.pillarIsDead) alivePillars++;
            }

            if (alivePillars == 0)
            {
                SceneContext.Instance.hudContext.DisplayHint("A portal to the next level has opened!");
                alreadyOpened = true;
                doorLeftClosed.SetActive(false);
                doorLeftOpen.SetActive(true);
                doorRightClosed.SetActive(false);
                doorRightOpen.SetActive(true);
                doorVisual.SetActive(true);
                sceneLoader.SetActive(true);
                //sceneLoader.LoadingScreenControl.LoadScreenExample(2);
                //sceneLoader.GetComponent<LoadingScreenControlLvl2>().LoadScreenExample(2);
                
                //sceneLoader.GetComponent<LoadingScreenControlLvl2>();
            }
        }
    }
}
