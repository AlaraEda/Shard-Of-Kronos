﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddGameObjectToRunTime : MonoBehaviour
{
    public GameObjectRunTime gameObjectRuntime;
    [SerializeField] private String nameObject;
    [SerializeField] private bool dontDestroyOnDisable;

    private void OnEnable()
    {
       
        gameObjectRuntime.AddToList(nameObject, this.gameObject);
    }

    private void OnDisable()
    {
        if (dontDestroyOnDisable == false)
        {
            gameObjectRuntime.RemoveFromList(nameObject, this.gameObject);
        }
        else
        {
          //  Debug.Log("dont destroy this gameobject" + nameObject);
        }
    }
}