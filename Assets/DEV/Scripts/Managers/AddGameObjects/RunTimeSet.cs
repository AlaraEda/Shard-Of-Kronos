﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Makes a list of undefined classes from where AddGameObjectToRunTime derives from.

public abstract class RunTimeSet<T> : ScriptableObject
{
    public Dictionary<String, T> itemDictionary = new Dictionary<string, T>();
    
    public void Initialize()
    {
        itemDictionary.Clear();
    }

    public T GetItemIndex(string index)
    {
        return itemDictionary[index];
    }

    public void AddToList(String name, T thingToAdd)
    {
        //prevent duplication
        if (!itemDictionary.ContainsKey(name))
        {
            itemDictionary.Add(name, thingToAdd);
        }
    }

    public void RemoveFromList(String name, T thingToAdd)
    {
        if (itemDictionary.ContainsKey(name))
        {
            itemDictionary.Remove(name);
        }
    }
}