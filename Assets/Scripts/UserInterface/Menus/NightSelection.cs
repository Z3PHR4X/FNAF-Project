using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface.Menus
{
    public class NightSelection : MonoBehaviour
    {
        private int selectedNight, completedNight;
        private Dropdown selectionDropdown;
        private Level selectedLevel;

        private void Awake()
        {
            selectionDropdown = GetComponent<Dropdown>();
        }

        // Start is called before the first frame update      
        void Start()
        {
            completedNight = PlayerPrefs.GetInt("completedNight");
            selectedLevel = Singleton.Instance.selectedMap;
            selectedNight = Singleton.Instance.selectedNight;
            if (Singleton.Instance.discord.enabledRichPresence)
            {
                Singleton.Instance.discord.UpdateStatus($"Setting up a game..", $"Main Menu - Completed Night {completedNight}", "mainmenu", "Main Menu");
            }
                PopulateNightOptions();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetSelectedNight()
        {
            selectedNight = selectionDropdown.value + 1;
            Singleton.Instance.selectedNight = selectedNight;
            print("Selected night: " + selectedNight);
        }

        public void PopulateNightOptions()
        {
            List<string> _availableNights = new List<string>();
            selectedLevel = Singleton.Instance.selectedMap;
            selectionDropdown.ClearOptions();
            if (completedNight < selectedLevel.numberOfNights)
            {
                for (int x = 1; x <= completedNight; x++)
                {
                    _availableNights.Add("Night " + x);
                }
            }
            else
            {
                for (int x = 1; x <= selectedLevel.numberOfNights; x++)
                {
                    _availableNights.Add("Night " + x);
                }
                if (selectedLevel.supportsCustomNight)
                {
                    _availableNights.Add("Night " + (selectedLevel.numberOfNights + 1) + " (Custom Night)");
                }
            }
            selectionDropdown.AddOptions(_availableNights);
            selectionDropdown.value = selectedNight - 1;

            selectionDropdown.RefreshShownValue();
        }
    }
}