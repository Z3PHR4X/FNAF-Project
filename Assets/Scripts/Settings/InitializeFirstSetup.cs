using UnityEngine;

namespace Settings
{
    public class InitializeFirstSetup : MonoBehaviour
    {
        [SerializeField] private DefaultSettings defaultSettings;

        void Start()
        {
            if (PlayerPrefs.GetString("gameSettingsVersion") != defaultSettings.settingsVersion)
            {
                Debug.Log("Upgraded to new settings from version " + defaultSettings.settingsVersion);
                defaultSettings.SetDefaultSettings();
            }

            PlayerPrefs.SetInt("selectedLevel", 1);
        }
    }
}