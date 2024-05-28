using UnityEngine;

namespace Zephrax.FNAFGame.Settings
{
    public class InitializeFirstSetup : MonoBehaviour
    {
        [SerializeField] private DefaultSettings defaultSettings;
        [SerializeField] private Level defaultLevel;

        void Awake()
        {
            if (PlayerPrefs.GetString("gameSettingsVersion") != defaultSettings.settingsVersion)
            {
                print("Upgraded to new settings version: " + defaultSettings.settingsVersion);
                PlayerPrefs.SetString("hasAcknowledgedFirstTimeSetupNotice", "false");
                defaultSettings.SetDefaultSettings();
            }

            if(PlayerPrefs.GetString("lockMouseToWindow") == "true")
            {
                Cursor.lockState = CursorLockMode.Confined;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
            //Singleton.Instance.selectedMap = defaultLevel;
        }
    }
}