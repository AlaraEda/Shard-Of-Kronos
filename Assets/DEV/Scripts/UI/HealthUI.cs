using System;
using System.Collections;
using DEV.Scripts.Managers;
using DEV.Scripts.Player;
using UnityEngine;
using UnityEngine.UI;

namespace DEV.Scripts.UI
{
    [RequireComponent(typeof(Image))]
    public class HealthUI : MonoBehaviour
    {
        private Image fillImage;
        private Color defaultColor;

        private void Awake()
        {
            fillImage = GetComponent<Image>();
            defaultColor = fillImage.color;
            SceneContext.Instance.playerManager.OnPlayerHealthChangeEvent += OnHealthChange;
            OnHealthChange(this,
                new PlayerHealthChangeEventArgs
                    {NewValue = SettingsEditor.Instance.playerHealthSettingsModel.currentPlayerHealth});
        }

        private void OnHealthChange(object sender, PlayerHealthChangeEventArgs args)
        {
            fillImage.fillAmount = args.NewValue / SettingsEditor.Instance.playerHealthSettingsModel.maxPlayerHealth;
            StartCoroutine(nameof(HealthChangeEffect));
        }

        private IEnumerator HealthChangeEffect()
        {
            fillImage.color = Color.white;
            yield return new WaitForSeconds(0.1f);
            fillImage.color = defaultColor;
        }
    }
}