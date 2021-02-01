using System;
using DEV.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

namespace DEV.Scripts.UI
{
    public class ControlsSettings : MonoBehaviour
    {
        [SerializeField] private Slider xSensitivitySlider;
        [SerializeField] private Toggle invertXToggle;
        [SerializeField] private Slider ySensitivitySlider;
        [SerializeField] private Toggle invertYToggle;
        [SerializeField] private Slider aimDivideXSlider;
        [SerializeField] private Slider aimDivideYSlider;

        [SerializeField] private TMP_Text textXSensitivitySlider;
        [SerializeField] private TMP_Text textYSensitivitySlider;
        [SerializeField] private TMP_Text textAimDivideXSlider;
        [SerializeField] private TMP_Text textAimDivideYSlider;


        private Settings.CameraSettingsModel cameraSettings;
        private CameraSettingsManager cameraSettingsManager;

        private void Awake()
        {
         
        }

        private void Start()
        {
            cameraSettings = SettingsEditor.Instance.cameraSettingsModel;
            cameraSettingsManager = SceneContext.Instance.mainCamera.GetComponentInParent<CameraSettingsManager>();
            xSensitivitySlider.value = cameraSettings.sensitivityX;
            ySensitivitySlider.value = cameraSettings.sensitivityY;
            invertXToggle.isOn = cameraSettings.invertedX;
            invertYToggle.isOn = cameraSettings.invertedY;
            aimDivideXSlider.value = cameraSettings.aimCamXDivider;
            aimDivideYSlider.value = cameraSettings.aimCamYDivider;
            
            
            textXSensitivitySlider.text = cameraSettings.sensitivityX.ToString("0");
            textYSensitivitySlider.text = cameraSettings.sensitivityY.ToString("0.0");

            textAimDivideXSlider.text = cameraSettings.aimCamXDivider.ToString("0");
            textAimDivideYSlider.text = cameraSettings.aimCamYDivider.ToString("0");
        }

        public void OnSensXChange(float value)
        {
            cameraSettings.sensitivityX = value;
            textXSensitivitySlider.text = cameraSettings.sensitivityX.ToString("0.0");
            SettingsEditor.Instance.SaveSettings();
            cameraSettingsManager.UpdateSettingsToComponents();
            //Debug.Log("Saved value is: " + cameraSettings.sensitivityX);
        }

        public void OnInvertXChange(bool value)
        {
            cameraSettings.invertedX = value;

            SettingsEditor.Instance.SaveSettings();
            cameraSettingsManager.UpdateSettingsToComponents();
            //Debug.Log("Saved value is: " + cameraSettings.invertedX);
        }

        public void OnSensYChange(float value)
        {
            cameraSettings.sensitivityY = value;
            textYSensitivitySlider.text = cameraSettings.sensitivityY.ToString("0.0");
            SettingsEditor.Instance.SaveSettings();
            cameraSettingsManager.UpdateSettingsToComponents();
            //Debug.Log("Saved value is: " + cameraSettings.sensitivityY);
        }

        public void OnInvertYChange(bool value)
        {
            cameraSettings.invertedY = value;
            SettingsEditor.Instance.SaveSettings();
            cameraSettingsManager.UpdateSettingsToComponents();
            //Debug.Log("Saved value is: " + cameraSettings.invertedY);
        }

        public void OnSensAimXChange(float value)
        {
            cameraSettings.aimCamXDivider = value;
            textAimDivideXSlider.text = cameraSettings.aimCamXDivider.ToString();
            SettingsEditor.Instance.SaveSettings();
            cameraSettingsManager.UpdateSettingsToComponents();
            //Debug.Log("Saved value is: " + cameraSettings.aimCamDivider);
        }

        public void OnSensAimYChange(float value)
        {
            cameraSettings.aimCamYDivider = value;
            textAimDivideYSlider.text = cameraSettings.aimCamYDivider.ToString();
            SettingsEditor.Instance.SaveSettings();
            cameraSettingsManager.UpdateSettingsToComponents();
            //Debug.Log("Saved value is: " + cameraSettings.aimCamDivider);
        }
    }
}