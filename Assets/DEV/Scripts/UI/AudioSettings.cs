using DEV.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace DEV.Scripts.UI
{
    public class AudioSettings : MonoBehaviour
    {
        [SerializeField] private Slider masterSlider;
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider effectSlider;

        private Settings.AudioSettingsModel audioSettings;

        private void Awake()
        {
            audioSettings = SettingsEditor.Instance.audioSettingsModel;
            masterSlider.value = audioSettings.masterVolume;
            musicSlider.value = audioSettings.musicVolume;
            effectSlider.value = audioSettings.effectsVolume;
        }

        public void OnMasterChange(float value)
        {
            audioSettings.masterVolume = value;
            SettingsEditor.Instance.SaveSettings();
        }

        public void OnMusicChange(float value)
        {
            audioSettings.musicVolume = value;
            SettingsEditor.Instance.SaveSettings();
        }

        public void OnEffectsChange(float value)
        {
            audioSettings.effectsVolume = value;
            SettingsEditor.Instance.SaveSettings();
        }
    }
}