using System;
using System.Collections;
using System.Collections.Generic;
using DEV.Scripts.EnemyPillar;
using DEV.Scripts.Managers;
using UnityEngine;

public class Lvl3OpenOnSlidePuzzleComplete : MonoBehaviour
{
    public GameObject doorLeftClosed;
    public GameObject doorLeftOpen;
    public GameObject doorRightClosed;
    public GameObject doorRightOpen;
    public GameObject collToDisable;
    public GameObject collToDisable2;

    public void OpenDoorsOnCompletion()
    {
        SceneContext.Instance.hudContext.DisplayHint("The slide puzzle has been completed. A gate has opened.");
        doorLeftClosed.SetActive(false);
        doorLeftOpen.SetActive(true);
        doorRightClosed.SetActive(false);
        doorRightOpen.SetActive(true);
        collToDisable.SetActive(false);
        collToDisable2.SetActive(false);
    }

    

}
