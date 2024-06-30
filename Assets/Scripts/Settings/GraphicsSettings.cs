using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Zephrax.FNAFGame.Settings
{
    public class GraphicsSettings : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown resolutionDropdown, refreshDropdown, screenModeDropdown, qualityDropdown;
        [SerializeField] private Toggle vsyncToggle;
        [SerializeField] private RevertScreen revertScreen;
        [SerializeField] private bool hasInterface, applyFromSave;
        private Resolution[] resolutions;
        private List<Resolution> filteredResolutions;
        private FullScreenMode[] screenModes;
        private List<int> refreshRates;
        private List<string> qualityOptions;
        private Resolution currentResolution;
        private FullScreenMode currentScreenMode;
        private int curResolutionIndex, curRefreshIndex, curScreenModeIndex, curQualityIndex, currentRefresh, targetFps;
        private int vsync;
        private bool settingsLoaded;

        private void Start()
        {
            settingsLoaded = false;
            resolutions = Screen.resolutions;
            currentResolution = Screen.currentResolution;
            currentRefresh = Screen.currentResolution.refreshRate;
            currentScreenMode = Screen.fullScreenMode;
            vsync = QualitySettings.vSyncCount;

            if (hasInterface)
            {
                PopulateRefreshSettings();
                PopulateResolutionSettings(currentRefresh);
                PopulateScreenModeSettings();
                PopulateVsyncSetting();
                PopulateQualitySettings();
            }
            if (applyFromSave)
            {
                LoadGraphicsSettingsFromSave();
            }
            settingsLoaded = true;
        }

        private void PopulateVsyncSetting()
        {
            vsyncToggle.isOn = (vsync == 1);
        }

        private void PopulateRefreshSettings()
        {
            refreshRates = new List<int>();
            refreshDropdown.ClearOptions();

            foreach (var res in resolutions)
            {
                if (!refreshRates.Contains(res.refreshRate))
                {
                    refreshRates.Add(res.refreshRate);
                }
            }

            List<string> refOptions = new List<string>();
            for (int i = 0; i < refreshRates.Count; i++)
            {
                refOptions.Add(refreshRates[i].ToString() + "Hz");
                if (refreshRates[i] == Screen.currentResolution.refreshRate)
                {
                    curRefreshIndex = i;
                }
            }

            refreshDropdown.AddOptions(refOptions);
            refreshDropdown.value = curRefreshIndex;
            refreshDropdown.RefreshShownValue();
        }

        private void PopulateResolutionSettings(int targetRefresh)
        {
            filteredResolutions = new List<Resolution>();
            resolutionDropdown.ClearOptions();

            foreach (var res in resolutions)
            {
                if (targetRefresh != 0)
                {
                    if (res.refreshRate == targetRefresh)
                    {
                        filteredResolutions.Add(res);
                    }
                }
                else
                {
                    filteredResolutions.Add(res);
                }
            }

            List<string> resOptions = new List<string>();
            for (int i = 0; i < filteredResolutions.Count; i++)
            {
                string option;
                if (targetRefresh == 0)
                {
                    option = filteredResolutions[i].width + "x" + filteredResolutions[i].height + " @" + filteredResolutions[i].refreshRate + "Hz";
                }
                else
                {
                    option = filteredResolutions[i].width + "x" + filteredResolutions[i].height;
                }
                resOptions.Add(option);
                if (filteredResolutions[i].width == Screen.width && filteredResolutions[i].height == Screen.height && filteredResolutions[i].refreshRate == Screen.currentResolution.refreshRate)
                {
                    curResolutionIndex = i;
                }
            }

            resolutionDropdown.AddOptions(resOptions);
            resolutionDropdown.value = curResolutionIndex;
            resolutionDropdown.RefreshShownValue();
        }

        private void PopulateScreenModeSettings()
        {
            screenModeDropdown.ClearOptions();

            if (SystemInfo.operatingSystemFamily == OperatingSystemFamily.Windows)
            {
                screenModes = new FullScreenMode[] { FullScreenMode.Windowed, FullScreenMode.ExclusiveFullScreen, FullScreenMode.FullScreenWindow };
            }
            else if ((SystemInfo.operatingSystemFamily == OperatingSystemFamily.MacOSX))
            {
                screenModes = new FullScreenMode[] { FullScreenMode.Windowed, FullScreenMode.FullScreenWindow, FullScreenMode.MaximizedWindow };
            }
            else
            {
                screenModes = new FullScreenMode[] { FullScreenMode.Windowed, FullScreenMode.FullScreenWindow };
            }

            List<string> screenModeOptions = new List<string>();
            for (int i = 0; i < screenModes.Length; i++)
            {
                screenModeOptions.Add(screenModes[i].ToString());
                if (screenModes[i] == Screen.fullScreenMode)
                {
                    curScreenModeIndex = i;
                }
            }

            screenModeDropdown.AddOptions(screenModeOptions);
            screenModeDropdown.value = curScreenModeIndex;
            screenModeDropdown.RefreshShownValue();
        }
        private void PopulateQualitySettings()
        {
            qualityOptions = new List<string>();
            qualityDropdown.ClearOptions();

            string[] temp = QualitySettings.names;
            for (int i = 0; i < temp.Length; i++)
            {
                qualityOptions.Add(temp[i]);
                if (i == QualitySettings.GetQualityLevel())
                {
                    curQualityIndex = i;
                }
            }

            qualityDropdown.AddOptions(qualityOptions);
            qualityDropdown.value = curQualityIndex;
            qualityDropdown.RefreshShownValue();
        }

        public void UpdateVsync()
        {
            if (vsyncToggle.isOn)
            {
                vsync = 1;
                targetFps = currentRefresh;
            }
            else
            {
                vsync = 0;
                targetFps = -1;
            }
        }

        public void UpdateScreenResolution()
        {
            if (settingsLoaded)
            {
                currentResolution = filteredResolutions[resolutionDropdown.value];
                print("Selected " + currentResolution);
            }
        }

        public void UpdateRefreshRate()
        {
            if (settingsLoaded)
            {
                currentRefresh = refreshRates[refreshDropdown.value];
                print("Selected " + currentRefresh);
                PopulateResolutionSettings(currentRefresh);
            }
        }

        public void UpdateScreenMode()
        {
            if (settingsLoaded)
            {
                curScreenModeIndex = screenModeDropdown.value;
                currentScreenMode = screenModes[curScreenModeIndex];
                print("Selected " + currentScreenMode);
            }
        }

        public void UpdateQuality()
        {
            if (settingsLoaded)
            {
                curQualityIndex = qualityDropdown.value;
                print("Selected " + curQualityIndex);
            }
        }

        public void ApplyGraphicsSettings()
        {
            Screen.SetResolution(currentResolution.width, currentResolution.height, currentScreenMode, currentRefresh);
            QualitySettings.SetQualityLevel(curQualityIndex);
            QualitySettings.vSyncCount = vsync;
            Application.targetFrameRate = currentRefresh;
            print("Applied new graphics settings.");
        }

        public void SaveGraphicsSettings()
        {
            PlayerPrefs.SetInt("graphicsResolutionX", currentResolution.width);
            PlayerPrefs.SetInt("graphicsResolutionY", currentResolution.height);
            PlayerPrefs.SetInt("graphicsRefreshrate", currentResolution.refreshRate);
            PlayerPrefs.SetInt("graphicsFullScreenMode", curScreenModeIndex);
            PlayerPrefs.SetInt("graphicsQuality", curQualityIndex);
            PlayerPrefs.SetInt("graphicsVsync", vsync);
            PlayerPrefs.Save();
            print("Saved graphics settings to save.");
        }

        public void LoadGraphicsSettingsFromSave()
        {
            FullScreenMode[] screenModes;
            if (SystemInfo.operatingSystemFamily == OperatingSystemFamily.Windows)
            {
                screenModes = new FullScreenMode[] { FullScreenMode.Windowed, FullScreenMode.ExclusiveFullScreen, FullScreenMode.FullScreenWindow };
            }
            else if ((SystemInfo.operatingSystemFamily == OperatingSystemFamily.MacOSX))
            {
                screenModes = new FullScreenMode[] { FullScreenMode.Windowed, FullScreenMode.FullScreenWindow, FullScreenMode.MaximizedWindow };
            }
            else
            {
                screenModes = new FullScreenMode[] { FullScreenMode.Windowed, FullScreenMode.FullScreenWindow };
            }

            int x = PlayerPrefs.GetInt("graphicsResolutionX");
            int y = PlayerPrefs.GetInt("graphicsResolutionY");
            print($"Resolution: {x}x{y}");
            int refresh = PlayerPrefs.GetInt("graphicsRefreshrate");
            print($"Refresh rate: {refresh}Hz");
            FullScreenMode screenMode = screenModes[PlayerPrefs.GetInt("graphicsFullScreenMode")];
            print($"Screen mode: {screenMode}");
            int quality = PlayerPrefs.GetInt("graphicsQuality");
            print($"Graphics quality: {quality}");
            int v = PlayerPrefs.GetInt("graphicsVsync");
            print($"VSync: {v}");

            Screen.SetResolution(x, y, screenMode, refresh);
            QualitySettings.SetQualityLevel(quality);
            QualitySettings.vSyncCount = v;
            print("Succesfully loaded graphics settings from save");
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

        public void RevertSettingsDialog(int length)
        {
            StartCoroutine(WaitToRevert(length));
        }

        public void StopRevertSettingsDialog()
        {
            StopAllCoroutines();
        }

        IEnumerator WaitToRevert(int duration)
        {
            revertScreen.waitTime = duration;
            for (int i = 0; i < duration; i++)
            {
                yield return new WaitForSeconds(1);
                revertScreen.waitTime--;
            }
            LoadGraphicsSettingsFromSave();
            revertScreen.gameObject.SetActive(false);
        }
    }
}

