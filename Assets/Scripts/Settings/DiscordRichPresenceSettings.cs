using UnityEngine;
using UnityEngine.UI;

public class DiscordRichPresenceSettings : MonoBehaviour
{
    [SerializeField] private Toggle richPresenceToggle;
    private bool rpEnabled, initialized;

    // Start is called before the first frame update
    void Start()
    {
        rpEnabled = PlayerPrefs.GetInt("discordRichPresence") == 1;
        richPresenceToggle.isOn = rpEnabled;
        initialized = true;
    }

    public void SetDiscordSettings()
    {
        if (initialized)
        {
            int state = 0;
            rpEnabled = richPresenceToggle.isOn;
            if (rpEnabled) state = 1;
            PlayerPrefs.SetInt("discordRichPresence", state);
            Singleton.Instance.discord.enabledRichPresence = rpEnabled;
            print($"Discord Rich Presence enabled: {rpEnabled}");
        }
    }
}
