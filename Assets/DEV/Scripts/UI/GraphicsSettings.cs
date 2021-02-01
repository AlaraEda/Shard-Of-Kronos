using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace DEV.Scripts.UI
{
    public class GraphicsSettings : MonoBehaviour
    {
        [SerializeField] private Dropdown screenResDropdown;
        [SerializeField] private Toggle vSyncToggle;
        [SerializeField] private Toggle fullscreenToggle;
        [SerializeField] private Dropdown texQualityDropdown;
        [SerializeField] private Toggle shadowToggle;

        private bool ignoreScreenResDropdownChanged = true;

        private void Awake()
        {
            fullscreenToggle.isOn = Screen.fullScreen;

            var resOptions = Screen
                .resolutions
                .Select(x => $"{x.width}x{x.height}x{x.refreshRate}")
                .ToList();
            resOptions.Reverse();
            screenResDropdown.AddOptions(resOptions);

            screenResDropdown.value = screenResDropdown
                .options
                .FindIndex(x =>
                    x.text == $"{Screen.width}x{Screen.height}x{Application.targetFrameRate}");
        }

        public void OnScreenResChange(int index)
        {
            if (ignoreScreenResDropdownChanged)
            {
                ignoreScreenResDropdownChanged = false;
                return;
            }

            string[] res = screenResDropdown.options[index].text.Split('x');
            Screen.SetResolution(int.Parse(res[0]), int.Parse(res[1]), Screen.fullScreen, int.Parse(res[2]));
            Application.targetFrameRate = int.Parse(res[2]);
        }

        public void OnVSyncChange() => QualitySettings.vSyncCount = vSyncToggle.isOn ? 1 : 0;

        public void OnFullscreenChange() => Screen.fullScreen = fullscreenToggle.isOn;

        public void OnTextureChange(int index) => QualitySettings.masterTextureLimit = index;

        public void OnShadowChange()
        {
            var pipeline = (UniversalRenderPipelineAsset) UnityEngine.Rendering.GraphicsSettings.renderPipelineAsset;
            pipeline.shadowDistance = shadowToggle.isOn ?  50 : 0;
        }
    }
}