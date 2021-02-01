using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DEV.Scripts.Input;
using DG.Tweening;

public class DoorInteract : Interactable
{
    private int keysDoor;
    private string stringText = "You still need to collect 2 keys";
  
    public override void OnPlayerInteract()
    {
        if (keysDoor == 2)
        {
            transform.DOLocalMove(new Vector3(0, 0, -5), 2f);
        }
        
    }

    public void KeyCollect( )
    {
        keysDoor++;
        switch (keysDoor)
        {
            case 0:
                stringText = "You still need to collect 2 keys";
                break;

            case 1:
                stringText = "You still need to collect 1 keys";
                break;

            case 2:
                stringText = "You got all the keys! Press \"E\" to open the door";
                break;
        }
    }
    

    public override string InteractionVerb => stringText;
}

// transform.DOLocalMove(new Vector3(0, 0, -5), 2f);