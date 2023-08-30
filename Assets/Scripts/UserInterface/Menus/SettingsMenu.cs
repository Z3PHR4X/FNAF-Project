using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface.Menus
{
    public class SettingsMenu : MonoBehaviour
    {
        [Header ("Script Dependencies")] 
        [SerializeField] private Settings.DefaultSettings defaultSettings;
        [SerializeField] private Settings.UpdateSettings updateSettings;

        [Header("UI Elements")]
        [SerializeField] private Slider sensitivitySlider, masterVolumeSlider, musicVolumeSlider, sfxVolumeSlider, interfaceVolumeSlider, voiceVolumeSlider;
        [SerializeField] private Dropdown resolutionDropdown;
        [SerializeField] private Text sensitivityText;

        private float mouseSensitivity;

        // Start is called before the first frame update
        void Start()
        {
            mouseSensitivity = PlayerPrefs.GetFloat("mouseSensitivity");
            UpdateMenu();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateMenu();
        }

        void UpdateMenu()
        {

        }

        public void resetSettings()
        {
            defaultSettings.SetDefaultSettings();
            UpdateMenu();
        }

        public void UpdateMouseSensitivty()
        {
            mouseSensitivity = sensitivitySlider.value;
            //updateSettings.UpdateMouseSensitivty(mouseSensitivity);
            sensitivityText.text = Convert.ToString(Math.Round(mouseSensitivity, 2));
            sensitivitySlider.value = mouseSensitivity;
        }
    }

}