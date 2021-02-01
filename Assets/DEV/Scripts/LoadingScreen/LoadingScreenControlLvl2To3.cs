using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DEV.Scripts.Managers;
using DEV.Scripts.Input;

public class LoadingScreenControlLvl2To3 : MonoBehaviour
{
    public GameObject loadingScreenObj;
    public Slider slider;
    //private int lvl = 2;

    //Alles dat op false moet wanneer de loading screen verschijnt
    public GameObject GeneralUIPauseMenu;
    public GameObject GeneralUIHudCanvas;

    // PlayerInput Setup *This is the global PlayerInputAction!*
    private PlayerInputActions playerInputActions;

    [SerializeField] private MovementHandeler movementHandler;
    
    public Transform enemy;
    

    AsyncOperation async;

    private IEnumerator LoadingScreen(int lvl){
        loadingScreenObj.SetActive(true);

        //Lopen Uitzetten
        movementHandler = SceneContext.Instance.playerTransform.gameObject.GetComponent<MovementHandeler>();
        movementHandler.changingPlayerPosition = true;
        SceneContext.Instance.playerController.inputController.OnDisable();

        //Set General UI to false so that you won't see it when the screen is loading
        GeneralUIPauseMenu.SetActive(false);
        GeneralUIHudCanvas.SetActive(false);

        async = SceneManager.LoadSceneAsync(lvl);     //load scene nummer 0 to background.
        async.allowSceneActivation = false;

        while (async.isDone == false){

            slider.value = async.progress;         //Move slider based on loading progress. 

            //If level is fully loaded
            if(async.progress >= 0.9f){      

                slider.value = 1f;                  //Slider full
                async.allowSceneActivation = true;  //Allow to switch to new level.
                //AsyncOperation.isDone = True, dus nu ga je uit de while-loop. 

            }
            
            //yield return null;
            yield return new WaitForSeconds(7);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Gravity>())
        {
            Debug.Log("Load Next Scene");
            StartCoroutine(LoadingScreen(3));

            foreach (Transform VARIABLE in enemy)
            {
                Destroy(VARIABLE.gameObject);
            }
            
        }
    }
}
