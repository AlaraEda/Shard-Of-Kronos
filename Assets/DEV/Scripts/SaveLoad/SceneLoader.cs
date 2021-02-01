using System;
using System.Collections;
using System.Collections.Generic;
using DEV.Scripts;
using DEV.Scripts.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public int sceneNameToLoad;
    private string currentScene;
    
    private void Awake()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Gravity>())
        {
            Debug.Log("Load Next Scene");
            SceneManager.LoadScene(sceneNameToLoad); //In variabele  heb je 2 geschreven.
        }
    }
}
