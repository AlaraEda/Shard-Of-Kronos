using System;
using DEV.Scripts.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DEV.Scripts.UI
{
    public class PauseMenu : MonoBehaviour
    {
        public int mainMenuSceneIndex;
        private Canvas pauseCanvas;
        private Canvas hudCanvas;
        private float prevTimeScale;
        private bool restoreTimeScaleOnDestroy;

        /// <summary>
        /// Use this property to control if the pause menu should be displayed or not
        /// </summary>
        public bool IsPaused
        {
            get => pauseCanvas.enabled;
            set
            {
                pauseCanvas.enabled = value;
                hudCanvas.enabled = !value;

                restoreTimeScaleOnDestroy = value;

                if (value)
                {

                    prevTimeScale = Time.timeScale;
                    Time.timeScale = 0;
                }
                else
                {
                    Time.timeScale = prevTimeScale;
                }
            }
        }

        public void Toggle() => IsPaused = !IsPaused;

        public void ExitToMainMenu() => SceneManager.LoadScene(mainMenuSceneIndex);
        



        private void Awake()
        {
            pauseCanvas = GetComponent<Canvas>();
            hudCanvas = SceneContext.Instance.hudCanvas;
            prevTimeScale = Time.timeScale;
        }
        
        // Avoid situations where scene is unloaded before timescale is restored!
        private void OnDestroy()
        {
            if (restoreTimeScaleOnDestroy) Time.timeScale = prevTimeScale;
        }
    }
}