using Discord;
using UnityEngine;

public class DiscordRichPresence : MonoBehaviour
{
    [SerializeField] private long applicationID;
    public string details, state, largeImage, largeText;
    public Discord.Discord discord;
    public bool enabledRichPresence;

    private long startTime;


    // Start is called before the first frame update
    void Start()
    {
        discord = new Discord.Discord(applicationID, (System.UInt64)Discord.CreateFlags.NoRequireDiscord);
        startTime = System.DateTimeOffset.Now.ToUnixTimeMilliseconds();
        enabledRichPresence = PlayerPrefs.GetInt("discordRichPresence") == 1;
        
        UpdateStatus(state, details, largeImage, largeText);
    }

    // Update is called once per frame
    void Update()
    {
        if (enabledRichPresence)
        {
            try
            {
                discord.RunCallbacks();
            }
            catch
            {
                Debug.Log("Tried to Update Status for Discord RP, but failed!");
                enabledRichPresence = false;
            }
        }
    }

    public void UpdateStatus(string state, string details, string largeImage, string largeText)
    {
        // Update Status every frame
        try
        {
            var activityManager = discord.GetActivityManager();
            var activity = new Discord.Activity
            {
                Details = details,
                State = state,
                Assets =
                {
                    LargeImage = largeImage,
                    LargeText = largeText
                },
                Timestamps =
                {
                    Start = startTime,
                }
            };

            activityManager.UpdateActivity(activity, (res) =>
            {
                if (res != Discord.Result.Ok) Debug.LogWarning("Failed connecting to Discord!");
            });
        }
        catch
        {
            // If updating the status fails, Destroy the GameObject
            //Destroy(gameObject);
            Debug.Log("Tried to Update Status for Discord RP, but failed!");
            enabledRichPresence = false;
        }
    }
}
