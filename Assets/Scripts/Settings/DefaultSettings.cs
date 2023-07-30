using UnityEngine;

namespace Settings
{
    public class DefaultSettings : MonoBehaviour
    {
        public float mouseSensitivity = 1f, masterVolume = 1f, musicVolume = 0.6f, sfxVolume = 0.8f, interfaceVolume = 0.8f, voiceVolume = 0.85f;
        public string settingsVersion = "Version1";
        public bool addApplicationVersion = false;

        private void Awake()
        {
            if (addApplicationVersion) {
                settingsVersion = Application.version + settingsVersion;
            }
        }

        public void SetDefaultSettings()
        {
            PlayerPrefs.SetFloat("mouseSensitivity", mouseSensitivity);
            PlayerPrefs.SetFloat("audioMasterVolume", masterVolume);
            PlayerPrefs.SetFloat("audioMusicVolume", musicVolume);
            PlayerPrefs.SetFloat("audioSfxVolume", sfxVolume);
            PlayerPrefs.SetFloat("audioInterfaceVolume", interfaceVolume);
            PlayerPrefs.SetFloat("audioVoiceVolume", voiceVolume);
            PlayerPrefs.SetInt("selectedNight", 0); //To be removed
            PlayerPrefs.SetInt("selectedLevel", 0); //To be removed
            PlayerPrefs.SetString("gameSettingsVersion", settingsVersion);
        }
    }
}
