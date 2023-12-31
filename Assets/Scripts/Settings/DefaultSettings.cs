using UnityEngine;

namespace Settings
{
    public class DefaultSettings : MonoBehaviour
    {
        public float mouseSensitivity = 1f, masterVolume = 1f, musicVolume = 0.7f, sfxVolume = 0.9f, interfaceVolume = 0.9f, voiceVolume = 0.95f;
        public string settingsVersion = "Version1";
        public bool addApplicationVersion = false;
        public bool enableDiscordRichPresence = true;
        public bool unlockTesterAward = false;

        private void Awake()
        {
            if (addApplicationVersion) {
                settingsVersion = Application.version + settingsVersion;
            }
        }

        public void SetDefaultSettings()
        {
            PlayerPrefs.SetInt("lockMouseToWindow", 1);
            PlayerPrefs.SetFloat("mouseSensitivity", mouseSensitivity);
            PlayerPrefs.SetFloat("audioMasterVolume", masterVolume);
            PlayerPrefs.SetFloat("audioMusicVolume", musicVolume);
            PlayerPrefs.SetFloat("audioSfxVolume", sfxVolume);
            PlayerPrefs.SetFloat("audioInterfaceVolume", interfaceVolume);
            PlayerPrefs.SetFloat("audioVoiceVolume", voiceVolume);
            PlayerPrefs.SetInt("graphicsResolutionX", Screen.currentResolution.width);
            PlayerPrefs.SetInt("graphicsResolutionY", Screen.currentResolution.height);
            PlayerPrefs.SetInt("graphicsRefreshrate", Screen.currentResolution.refreshRate);
            PlayerPrefs.SetInt("graphicsFullScreenMode", 1);
            PlayerPrefs.SetInt("graphicsQuality", 5);
            PlayerPrefs.SetInt("graphicsVsync", 0);
            //PlayerPrefs.SetInt("completedNight", 0);
            PlayerPrefs.SetInt("enableSpecialMusic", 1);
            PlayerPrefs.SetInt("menuMusicOverride", 0);
            PlayerPrefs.SetInt("menuMusicSelection", 0);
            PlayerPrefs.SetString("returningPlayer", "false");
            PlayerPrefs.SetInt("discordRichPresence", 0);
            PlayerPrefs.SetString("gameSettingsVersion", settingsVersion);

            if(enableDiscordRichPresence)
            {
                PlayerPrefs.SetInt("discordRichPresence", 1);
            }

            if (unlockTesterAward)
            {
                PlayerPrefs.SetInt("TesterAwardUnlocked", 1);
            }
            PlayerPrefs.Save();
            print("Default settings set!");
        }
    }
}
