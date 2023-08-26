using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Settings
{
    public class UpdateSettings : MonoBehaviour
    {

        //Controls
        public void UpdateMouseSensitivty(float newValue)
        {
            PlayerPrefs.SetFloat("mouseSensitivity", newValue);
        }

        //Audio
        public void UpdateMasterVolume(float newValue)
        {
            PlayerPrefs.SetFloat("audioMasterVolume", newValue);
        }
        public void UpdateMusicVolume(float newValue)
        {
            PlayerPrefs.SetFloat("audioMusicVolume", newValue);
        }
        public void UpdateSfxVolume(float newValue)
        {
            PlayerPrefs.SetFloat("audioSfxVolume", newValue);
        }
        public void UpdateInterfaceVolume(float newValue)
        {
            PlayerPrefs.SetFloat("audioInterfaceVolume", newValue);
        }
        public void UpdateVoiceVolume(float newValue)
        {
            PlayerPrefs.SetFloat("audioVoiceVolume", newValue);
        }

        //Graphics
        public void UpdateGraphicsResolution(int resX, int resY)
        {
            PlayerPrefs.SetInt("graphicsResolutionX", resX);
            PlayerPrefs.SetInt("graphicsResolutionY", resY);
        }

        public void UpdateGraphicsRefreshRate(int newValue)
        {
            PlayerPrefs.SetInt("graphicsRefreshRate", newValue);
        }
    }
}
