using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CometSpawner : MonoBehaviour
{
    private GameObjectRunTime playerSet;
    private CometScript cometScript;

    private void Awake()
    {
        playerSet = Resources.Load<GameObjectRunTime>("ObjectRunTime");
        cometScript = playerSet.GetItemIndex("Comet").GetComponent<CometScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        cometScript.TriggerComet();
     
    }
}
