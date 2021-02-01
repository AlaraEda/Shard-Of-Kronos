using System.Collections;
using System.Collections.Generic;
using DEV.Scripts.Managers;
using DEV.Scripts.Managers.WorldSwitching;
using UnityEngine;

public class DeActivateWall : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject wallCol;
    void Start()
    {
        SceneContext.Instance.worldSwitchManager.OnWorldSwitchEvent += OnWorldSwitchEvent;
    }
    
    private void OnWorldSwitchEvent(object sender, WorldSwitchEventArgs args)
    {
        if (args.NextWorld == WorldSwitchEventArgs.WorldType.Normal)
        {
            wallCol.SetActive(true);
        }
        else
        {
            wallCol.SetActive(false);
        }
    }



}
