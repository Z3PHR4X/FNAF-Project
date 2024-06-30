using UnityEngine;

namespace Zephrax.FNAFGame.Settings
{
    public class DefaultSettings : MonoBehaviour
    {
        [Header("Control settings")]
        public float mouseSensitivity = 1f;
        public bool lockCursor = false;
        [Header("Audio settings")]
        public bool musicOverride = false;
        [Range(0,1)] public float masterVolume = 1f, musicVolume = 0.7f, sfxVolume = 0.9f, interfaceVolume = 0.9f, voiceVolume = 0.95f;
        [Header("Discord settings")]
        public bool enableDiscordRichPresence = true;
        [Header("Rewards")]
        public bool unlockTesterAward = false;
        [Header("Game version")]
        public string settingsVersion = "Version1";
        [SerializeField] private string baseSettingsVersion;
        public bool addApplicationVersion = false;
        public bool addBuildDate = false;

        public void SetDefaultSettings()
        { 
            if (lockCursor)
            {
                PlayerPrefs.SetInt("lockMouseToWindow", 1);
            }
            else {
                PlayerPrefs.SetInt("lockMouseToWindow", 0);
            }

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

            if(enableDiscordRichPresence)
            {
                PlayerPrefs.SetInt("discordRichPresence", 1);
            }

            if (unlockTesterAward)
            {
                PlayerPrefs.SetInt("TesterAwardUnlocked", 1);
            }

            settingsVersion = baseSettingsVersion;
            if (addApplicationVersion)
            {
                settingsVersion += $"A{Application.version.ToString()}";
            }
            if (addBuildDate)
            {
                settingsVersion += $"B{BuildInfo.BUILD_TIME}";
            }
            PlayerPrefs.SetString("gameSettingsVersion", settingsVersion);

            PlayerPrefs.Save();
            print($"Default settings set! New settings-version: {settingsVersion}");

        }

        public string GetLatestSettingsVersion()
        {
            string lSettingsVersion = baseSettingsVersion;
            if (addApplicationVersion)
            {
                lSettingsVersion += $"A{Application.version.ToString()}";
            }
            if (addBuildDate)
            {
                lSettingsVersion += $"B{BuildInfo.BUILD_TIME}";
            }
            settingsVersion = lSettingsVersion;

            return lSettingsVersion;
        }
    }
}
