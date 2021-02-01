using System.Collections;
using System.Collections.Generic;
using DEV.Scripts.Enemy;
using DEV.Scripts.EnemyPillar;
using DEV.Scripts.Managers;
using UnityEngine;

public class Lvl2OpenOnEnemyDefeated : MonoBehaviour
{
    public GameObject sceneLoader;
    public GameObject doorLeftClosed;
    public GameObject doorLeftOpen;
    public GameObject doorRightClosed;
    public GameObject doorRightOpen;
    public GameObject enemyParent;
    private bool alreadyOpened;
    

    // Update is called once per frame
    void Update()
    {
        if (!alreadyOpened)
        {
            if (enemyParent.transform.childCount <= 10)
            {
                SceneContext.Instance.hudContext.DisplayHint("A portal to the next level has opened!");
                alreadyOpened = true;
                doorLeftClosed.SetActive(false);
                doorLeftOpen.SetActive(true);
                doorRightClosed.SetActive(false);
                doorRightOpen.SetActive(true);
                sceneLoader.SetActive(true);
                //sceneLoader.GetComponent<LoadingScreenControlLvl2To3>();
                //Debug.Log("Ben ik er?");
            }

        }
    }
}
