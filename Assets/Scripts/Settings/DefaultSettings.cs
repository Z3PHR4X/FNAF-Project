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
            PlayerPrefs.SetInt("graphicsResolutionX", 1280);
            PlayerPrefs.SetInt("graphicsResolutionY", 720);
            PlayerPrefs.SetInt("graphicsRefreshrate", 60);
            PlayerPrefs.SetInt("graphicsFullScreenMode", 0);
            PlayerPrefs.SetInt("graphicsQuality", 0);
            PlayerPrefs.SetInt("graphicsVsync", 0);
            //PlayerPrefs.SetInt("completedNight", 0); 
            //PlayerPrefs.SetInt("completedLevel", 0); 
            PlayerPrefs.SetString("gameSettingsVersion", settingsVersion);
            print("Default settings set!");
        }
    }
}
