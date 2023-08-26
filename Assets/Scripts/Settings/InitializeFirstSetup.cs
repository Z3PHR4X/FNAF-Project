using UnityEngine;

namespace Settings
{
    public class InitializeFirstSetup : MonoBehaviour
    {
        [SerializeField] private DefaultSettings defaultSettings;
        [SerializeField] private Level defaultLevel;

        void Start()
        {
            if (PlayerPrefs.GetString("gameSettingsVersion") != defaultSettings.settingsVersion)
            {
                Debug.Log("Upgraded to new settings from version " + defaultSettings.settingsVersion);
                defaultSettings.SetDefaultSettings();
            }

            Singleton.Instance.selectedMap = defaultLevel;
        }
    }
}