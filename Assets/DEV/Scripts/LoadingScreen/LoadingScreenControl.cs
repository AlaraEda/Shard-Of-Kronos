using System.Collections;
using System.Collections.Generic;
using System.IO;
using DEV.Scripts.Managers;
using DEV.Scripts.SaveLoad;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreenControl : MonoBehaviour
{
    public GameObject loadingScreenObj;
    public Slider slider;
    //public int LVL;
    //public PlayerStatus playerStatus;

    AsyncOperation async;
    public static readonly string saveFileName = "playersave.json";

    public void LoadScreenExample(int LVL)
    {
        string path = Path.Combine(Application.persistentDataPath, saveFileName); //Where to save?
            
        Debug.Log(path);

        if (File.Exists(string.Concat(path)))
        {
            File.Delete(string.Concat(path));
        }
        else
        {
           
        }
        StartCoroutine(LoadingScreen(LVL));
    }



    public IEnumerator LoadingScreen(int lvl){
        Debug.Log("load Screen");
        loadingScreenObj.SetActive(true);
        async = SceneManager.LoadSceneAsync(lvl);     //load scene nummer 0 to background.
        async.allowSceneActivation = false;

        while (async.isDone == false){
        
            slider.value = async.progress;         //Move slider based on loading progress. 
        
            //If level is fully loaded
            if(async.progress == 0.9f){      
        
                slider.value = 1f;                  //Slider full
                async.allowSceneActivation = true;  //Allow to switch to new level.
                //AsyncOperation.isDone = True, dus nu ga je uit de while-loop. 
        
            }
            
            //yield return null;
            yield return new WaitForSeconds(7);
        }
    }

    public IEnumerator LoadingScreen(string lvl){
        Debug.Log("load Screen");
        loadingScreenObj.SetActive(true);
        async = SceneManager.LoadSceneAsync(lvl);     //load scene nummer 0 to background.
        async.allowSceneActivation = false;

        while (async.isDone == false){
        
            slider.value = async.progress;         //Move slider based on loading progress. 
        
            //If level is fully loaded
            if(async.progress == 0.9f){      
        
                slider.value = 1f;                  //Slider full
                async.allowSceneActivation = true;  //Allow to switch to new level.
                //AsyncOperation.isDone = True, dus nu ga je uit de while-loop. 
        
            }
            
            //yield return null;
            yield return new WaitForSeconds(7);
        }
    }
}
