using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Zephrax.FNAFGame.UserInterface.Menus
{
    public class MainMenuButtons : MonoBehaviour
    {
        [SerializeField] private Text ContinueText, RetryText, SettingsText;
        [SerializeField] private GameObject ContinueButton, RetryButton, SetupButton;
        [SerializeField] private int SetupButtonNightUnlock = 5;

        private int completedNight, retryNight;
        private bool firstTime;

        // Start is called before the first frame update
        void Start()
        {
            completedNight = PlayerPrefs.GetInt("completedNight");
            retryNight = Singleton.Instance.selectedNight;
            firstTime = (PlayerPrefs.GetString("returningPlayer") == "false");

            if (completedNight > 0 && completedNight < 7)
            {
                ContinueButton.SetActive(true);
                int nextNight = Mathf.Clamp(completedNight + 1, 1, 7);
                ContinueText.text = "Start night " + nextNight;
                if (nextNight == 6) ContinueText.color = Color.yellow;
                if (nextNight == 7) ContinueText.color = Color.red;
            }
            else
            {
                ContinueButton.SetActive(false);
            }

            if (Singleton.Instance.canRetryNight && completedNight + 1 != retryNight)
            {
                RetryText.text = "Retry Night " + retryNight;
                RetryButton.SetActive(true);
            }
            else
            {
                RetryButton.SetActive(false);
            }

            if (completedNight >= SetupButtonNightUnlock)
            {
                SetupButton.SetActive(true);
            }
            else
            {
                SetupButton.SetActive(false);
            }

            SetDiscordMessage();

            //Resolution bug is fixed so no longer needed
            //if (firstTime)
            //{
            //    SettingsText.text = "> Settings <";
            //    SettingsText.color = Color.green;
            //}

        }

        public void StartNewGame()
        {
            print("Starting new game..");
            completedNight = 0;
            PlayerPrefs.SetInt("completedNight", 0);
            PlayerPrefs.Save();
            Singleton.Instance.selectedNight = 1;
            Singleton.Instance.completedNight = 0;
            Singleton.Instance.selectedMap = Singleton.Instance.availableLevels[0];
            Singleton.Instance.ChangeScene("LoadingScreen");
        }

        public void ContinueGame()
        {
            Singleton.Instance.selectedNight = Mathf.Clamp(completedNight + 1, 1, 7);
            Singleton.Instance.selectedMap = Singleton.Instance.availableLevels[0];
            print("Continuing game on night " + Singleton.Instance.selectedNight); ;
            Singleton.Instance.ChangeScene("LoadingScreen");
        }

        public void RetryGame()
        {
            print("Retrying night: " + retryNight);
            Singleton.Instance.selectedNight = retryNight;
            Singleton.Instance.ChangeScene("LoadingScreen");
        }

        private void SetDiscordMessage()
        {
            if (Singleton.Instance.discord.enabledRichPresence)
            {
                if (Singleton.Instance.completedNight > 0)
                {
                    Singleton.Instance.discord.UpdateStatus("Preparing for the nightshift..", $"Main Menu - Completed Night {Singleton.Instance.completedNight}", "mainmenu", "Main Menu");
                }
                else
                {
                    Singleton.Instance.discord.UpdateStatus("Preparing for the nightshift..", "Main Menu - Starting a new game", "mainmenu", "Main Menu");
                }
            }
        }
    }
}