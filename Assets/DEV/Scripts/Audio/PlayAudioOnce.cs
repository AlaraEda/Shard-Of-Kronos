using System;
using System.Collections;
using UnityEngine;

namespace DEV.Scripts.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayAudioOnce : MonoBehaviour
    {
        public AudioClip audioClip;
        private AudioSource audioSource;

        private void OnEnable()
        {
            var audioSettings = SettingsEditor.Instance.audioSettingsModel;
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = audioClip;
            audioSource.loop = false;
            audioSource.volume = audioSettings.effectsVolume * audioSettings.masterVolume;
            StartCoroutine(nameof(PlayClip));
        }

        public IEnumerator PlayClip()
        {
            audioSource.Play();
            yield return new WaitForSeconds(audioClip.length + 1);
            Destroy(gameObject);
        }
    }
}