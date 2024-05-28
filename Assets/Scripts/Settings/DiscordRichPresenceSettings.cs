using UnityEngine;
using UnityEngine.UI;
using Zephrax.FNAFGame;

namespace Zephrax.FNAFGame.Settings {
public class DiscordRichPresenceSettings : MonoBehaviour
{
     [SerializeField] private Toggle discordRPToggle;
        [SerializeField] private bool hasInterface;
        private bool settingsLoaded, discordRPEnabled;

        private void Start()
        {
            settingsLoaded = false;

            LoadDiscordSettings();
        }

        private void UpdateInterfaceValues()
        {
            discordRPToggle.isOn = discordRPEnabled;
        }

        public void ToggleDiscordRP()
        {
            if(settingsLoaded)
            {
                discordRPEnabled = discordRPToggle.isOn;
                if (discordRPEnabled)
                {
                    Singleton.Instance.discord.enabledRichPresence = true;
                    Singleton.Instance.discord.EnableRichPresence();
                }
                else
                {
                    Singleton.Instance.discord.enabledRichPresence = false;
                    Singleton.Instance.discord.DisableRichPresence();
                }
            }
        }

        public void DisableDiscordRP()
        {
            Singleton.Instance.discord.enabledRichPresence = false;
            Singleton.Instance.discord.DisableRichPresence();
            PlayerPrefs.SetInt("discordRichPresence", 0);
            PlayerPrefs.Save();
        }

        public void EnableDiscordRP()
        {
            Singleton.Instance.discord.enabledRichPresence = true;
            Singleton.Instance.discord.EnableRichPresence();
            PlayerPrefs.SetInt("discordRichPresence", 1);
            PlayerPrefs.Save();
        }

        public void SaveDiscordSettings()
        {
            if (discordRPEnabled)
            {
                PlayerPrefs.SetInt("discordRichPresence", 1);
            }
            else
            {
                PlayerPrefs.SetInt("discordRichPresence", 0);
            }
            PlayerPrefs.Save();
            print("Saved game settings to save.");
        }

        public void LoadDiscordSettings()
        {
            discordRPEnabled = (PlayerPrefs.GetInt("discordRichPresence")==1);

            if (hasInterface)
            {
                UpdateInterfaceValues();
            }

            settingsLoaded = true;
            print("Succesfully loaded game settings from save");
        }

        public void ToggleMenu(bool setActive)
        {
            if (setActive)
            {
                CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
                canvasGroup.alpha = 1;
                canvasGroup.blocksRaycasts = true;
                canvasGroup.interactable = true;
            }
            else
            {
                CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
                canvasGroup.alpha = 0;
                canvasGroup.blocksRaycasts = false;
                canvasGroup.interactable = false;
            }
        }

    }

}