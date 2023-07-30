using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Audio
{
    public class MenuSoundPlayer : MonoBehaviour
    {
        [Header("Scene objects")]
        [SerializeField] private AudioSource audioSource;

        [Header("Audio clips")]
        [SerializeField] private AudioClip confirmSound, cancelSound, hoverSound;
        [SerializeField] private float confirmVolume = 1f, cancelVolume = 1f, hoverVolume = 1f;

        private float masterVolume, musicVolume, sfxVolume, voiceVolume, interfaceVolume;

        private void Awake()
        {
            masterVolume = PlayerPrefs.GetFloat("audioMasterVolume");
            musicVolume = PlayerPrefs.GetFloat("audioMusicVolume");
            sfxVolume = PlayerPrefs.GetFloat("audioSfxVolume");
            interfaceVolume = PlayerPrefs.GetFloat("audioInterfaceVolume");
            voiceVolume = PlayerPrefs.GetFloat("audioVoiceVolume");
        }
        public void PlayConfirmSound() 
        {
            audioSource.clip = confirmSound;
            audioSource.volume = masterVolume * interfaceVolume * confirmVolume;
            audioSource.Play();
        }

        public void PlayCancelSound()
        {
            audioSource.clip = cancelSound;
            audioSource.volume = masterVolume * interfaceVolume * cancelVolume;
            audioSource.Play();
        }

        public void PlayHoverSound()
        {
            audioSource.clip = hoverSound;
            audioSource.volume = masterVolume * interfaceVolume * hoverVolume;
            audioSource.Play();
        }
    }
}

