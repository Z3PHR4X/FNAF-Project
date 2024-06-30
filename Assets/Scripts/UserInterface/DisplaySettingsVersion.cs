using TMPro;
using UnityEngine;

namespace Zephrax.FNAFGame.UserInterface
{
    public class DisplaySettingsVersion : MonoBehaviour
    {
        [SerializeField] private TMP_Text settingsVersionText;

        // Start is called before the first frame update
        void Start()
        {
            if (settingsVersionText != null)
            {
                settingsVersionText.text = $"Settings-version: {PlayerPrefs.GetString("gameSettingsVersion")}";

            }
        }
    }
}