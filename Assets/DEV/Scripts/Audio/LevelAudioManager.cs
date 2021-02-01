using System;
using System.Collections;
using System.Collections.Generic;
using DEV.Scripts.Enemy;
using DEV.Scripts.Extensions;
using DEV.Scripts.Managers;
using DEV.Scripts.Managers.WorldSwitching;
using DEV.Scripts.Player;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DEV.Scripts.Audio
{
    /// <summary>
    /// This class is responsible for managing the background audio in a level scene.
    /// It will alternate between normal, combat and spirit depending on current situation
    /// </summary>
    public class LevelAudioManager : MonoBehaviour
    {
        // How fast should the sound fade when transition to different audio?
        public float fadeSpeed;
        
        // The max volume that an audio can have
        [HideInInspector] public float maxAudioVolume;
        
        /**
         * All audio source components.
         * These are the sounds that LevelAudioManager will transition between
         */
        [SerializeField] private AudioSource normalAmbient;
        [SerializeField] private AudioSource spiritAmbient;
        [SerializeField] private AudioSource combatAmbient;
        [SerializeField] private AudioSource deathAmbient;
        [SerializeField] private AudioSource endgameAmbient;
        [SerializeField] private AudioSource onSpiritEnterAudioPrefab;

        // Used to prevent death sound from playing multiple times
        private bool deathSoundAlreadyPlayed;
        
        // Current active audio source
        private AudioSource currentSource;
        
        // Current active DOFade action
        private TweenerCore<float, float, FloatOptions> tweenerAction;
        
        private Settings.AudioSettingsModel audioSettings;

        private void Awake()
        {
            audioSettings = SettingsEditor.Instance.audioSettingsModel;
            maxAudioVolume = audioSettings.musicVolume * audioSettings.masterVolume;
            currentSource = normalAmbient;
            
            // Set initial volume for all sounds to zero
            foreach (var audioSource in GetComponentsInChildren<AudioSource>())
            {
                audioSource.volume = 0;
            }

            normalAmbient.volume = maxAudioVolume;
            
            // Subscribe to all relevant events
            SceneContext.Instance.worldSwitchManager.OnWorldSwitchEvent += OnWorldSwitchEvent;
            SceneContext.Instance.playerManager.OnPlayerHealthChangeEvent += OnPlayerHealthChangeEvent;
            SceneContext.Instance.OnEnemyHitEvent += OnEnemyHitEvent;
            SettingsEditor.Instance.OnSettingsSave += OnSettingsSave;
        }

        private void OnSettingsSave(object sender, EventArgs args)
        {
            tweenerAction?.Complete();
            maxAudioVolume = audioSettings.musicVolume * audioSettings.masterVolume;
            currentSource.volume = maxAudioVolume;
        }

        private void Update()
        {
            HandleCombatAmbient();
        }
        
        /// <summary>
        /// This method is subscribed to the OnPlayerHealthChangeEvent
        /// It will fade all sounds to zero and fade the combat music tm max audio
        /// It willonly do this if player actually lost health
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="args">Event arguments</param>
        private void OnPlayerHealthChangeEvent(object sender, PlayerHealthChangeEventArgs args)
        {
            if (args.IsDamage && args.NewValue > 0) OnEnemyHitEvent(sender, null);
            if (args.NewValue <= 0 && !deathSoundAlreadyPlayed)
            {
                deathSoundAlreadyPlayed = true;
                combatAmbient.DOFade(0, fadeSpeed);
                spiritAmbient.DOFade(0, fadeSpeed);
                normalAmbient.DOFade(0, fadeSpeed);
                endgameAmbient.DOFade(0, fadeSpeed);
                tweenerAction = deathAmbient.DOFade(maxAudioVolume, fadeSpeed);
                deathAmbient.Play();
                currentSource = deathAmbient;
            }
        }

        private void OnEnemyHitEvent(object sender, EnemyHitEventArgs args)
        {
            tweenerAction = combatAmbient.DOFade(maxAudioVolume, fadeSpeed).SetEase(Ease.Linear);
            spiritAmbient.DOFade(0, fadeSpeed);
            normalAmbient.DOFade(0, fadeSpeed);
            deathAmbient.DOFade(0, fadeSpeed);
            endgameAmbient.DOFade(0, fadeSpeed);
            currentSource = combatAmbient;
        }

        private void OnWorldSwitchEvent(object sender, WorldSwitchEventArgs args)
        {
            if (args.NextWorld == WorldSwitchEventArgs.WorldType.Normal)
            {
                spiritAmbient.DOFade(0, fadeSpeed);
                endgameAmbient.DOFade(0, fadeSpeed);
                tweenerAction = normalAmbient.DOFade(maxAudioVolume, fadeSpeed);
                currentSource = normalAmbient;
            }
            else
            {
                currentSource = spiritAmbient;
                tweenerAction = spiritAmbient.DOFade(maxAudioVolume, fadeSpeed);
                normalAmbient.DOFade(0, fadeSpeed);
                endgameAmbient.DOFade(0, fadeSpeed);
                Instantiate(onSpiritEnterAudioPrefab, transform);
            }
        }
        
        /// <summary>
        /// This method is called every frame and handles thee combat music (if it is playing)
        /// It will keep checking that the player is still in combat and if not, transition back to normal music
        /// </summary>
        private void HandleCombatAmbient()
        {
            if (combatAmbient.volume > 0)
            {
                int hostiles = 0;
                for (int i = 0; i < SceneContext.Instance.ActiveEnemies.Count; i++)
                { // Check how many enemies are actually hostile
                    if (SceneContext.Instance.ActiveEnemies[i].State == EnemyBase.StateEnum.Attack ||
                        SceneContext.Instance.ActiveEnemies[i].State == EnemyBase.StateEnum.Chase ||
                        SceneContext.Instance.ActiveEnemies[i].State == EnemyBase.StateEnum.Dead)
                    {
                        hostiles++;
                    }
                }
                
                // If no enemies are currently hostile, go back to normal music
                if (hostiles == 0)
                {
                    combatAmbient.DOFade(0, fadeSpeed);
                    if (SceneContext.Instance.worldSwitchManager.worldIsNormal &&
                        SceneContext.Instance.playerManager.PlayerHealth > 0)
                    {
                        tweenerAction = normalAmbient.DOFade(maxAudioVolume, fadeSpeed);
                        currentSource = normalAmbient;
                    }
                    else
                    {
                        tweenerAction = spiritAmbient.DOFade(maxAudioVolume, fadeSpeed);
                        currentSource = spiritAmbient;
                    }
                }
            }
            
        }

        public void PlayEndMusic()
        {
            combatAmbient.DOFade(0, fadeSpeed);
            spiritAmbient.DOFade(0, fadeSpeed);
            normalAmbient.DOFade(0, fadeSpeed);
            deathAmbient.DOFade(0, fadeSpeed);
            endgameAmbient.Play();
            tweenerAction = endgameAmbient.DOFade(maxAudioVolume, fadeSpeed);
            currentSource = endgameAmbient;
        }
    }
}