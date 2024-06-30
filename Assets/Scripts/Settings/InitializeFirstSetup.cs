using UnityEngine;

namespace Zephrax.FNAFGame.Settings
{
    public class InitializeFirstSetup : MonoBehaviour
    {
        [SerializeField] private DefaultSettings defaultSettings;
        [SerializeField] private Level defaultLevel;

        void Start()
        {
            if (PlayerPrefs.GetString("gameSettingsVersion") != defaultSettings.GetLatestSettingsVersion())
            {
                print("Upgraded to new settings version: " + defaultSettings.settingsVersion);
                PlayerPrefs.SetString("hasAcknowledgedFirstTimeSetupNotice", "false");
                defaultSettings.SetDefaultSettings();
            }

            if(PlayerPrefs.GetInt("lockMouseToWindow") == 1)
            {
                Cursor.lockState = CursorLockMode.Confined;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
            //Singleton.Instance.selectedMap = defaultLevel;
            Singleton.Instance.discord.UpdateStatus(Application.version, "Starting game..", "mainmenu", $"Completed night: {Singleton.Instance.completedNight}");
        }
    }
}