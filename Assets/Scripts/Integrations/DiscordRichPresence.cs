using Discord;
using System;
using UnityEngine;

namespace Zephrax.FNAFGame.Integrations
{
    public class DiscordRichPresence : MonoBehaviour
    {
        [Header("Application Setup")]
        public bool enabledRichPresence;
        [SerializeField] private long applicationID;
        [Header("Activity Settings")]
        public string details;
        public string state;
        public string largeImage;
        public string largeText;
        public string smallImage;
        public string smallText;
        [Header("Party Settings")]
        public Privacy partyPrivacy;
        public int partySize;
        public int partyMax;

        public Discord.Discord discord;

        private long startTime;
        private bool isDisabled;

        public enum Privacy
        {
            Private,
            Public
        }

        private void Awake()
        {   
            enabledRichPresence = PlayerPrefs.GetInt("discordRichPresence") == 1;
            Debug.Log($"Discord Rich Presence enabled in save: {enabledRichPresence}");
            if (enabledRichPresence)
            {
                EnableRichPresence();
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            
            
        }

        // Update is called once per frame
        void LateUpdate()
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
            else
            {
                if (discord != null && !isDisabled)
                {
                    DisableRichPresence();
                    isDisabled = true;
                    Debug.Log("Discord RP is disabled!");
                }
            }
        }

        public void EnableRichPresence()
        {
            try
            {
                discord = new Discord.Discord(applicationID, (UInt64)CreateFlags.NoRequireDiscord);
            }
            catch
            {
                Debug.LogWarning("Discord Rich Presence could not create Discord instance");
                enabledRichPresence = false;
            }

            startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            UpdateStatus(details, state, largeImage, largeText);
        }

        public void DisableRichPresence()
        {
            if (discord != null)
            {
                var activityManager = discord.GetActivityManager();
                activityManager.ClearActivity((result) =>
                {
                    if (result == Result.Ok)
                    {
                        Debug.Log("Success!");
                    }
                    else
                    {
                        Debug.Log("Failed");
                    }
                });
            }
        }

        //Includes partySize
        public void UpdateStatus(string details, string state, string largeImage, string largeText, string smallImage, string smallText, int partySize, int partyMax)
        {
            if (enabledRichPresence)
            {
                // Update Status every frame
                try
                {
                    var activityManager = discord.GetActivityManager();
                    var activity = new Activity
                    {
                        Details = details,
                        State = state,
                        Assets =
                    {
                        LargeImage = largeImage,
                        LargeText = largeText,
                        SmallImage = smallImage,
                        SmallText = smallText
                    },
                        Party =
                    {
                        Size =
                        {
                            CurrentSize = partySize,
                            MaxSize = partyMax
                        }
                    }
                        ,
                        Timestamps =
                {
                    Start = startTime,
                }
                    };

                    activityManager.UpdateActivity(activity, (res) =>
                    {
                        if (res != Result.Ok) Debug.LogWarning("Failed connecting to Discord!");
                    });
                }
                catch
                {
                    // If updating the status fails, Destroy the GameObject
                    //Destroy(gameObject); disabled as it will destroy the singleton
                    Debug.Log("Tried to Update Status for Discord RP, but failed!");
                    enabledRichPresence = false;
                }
            }
        }

        //Standard 
        public void UpdateStatus( string details, string state, string largeImage, string largeText, string smallImage, string smallText)
        {
            if (enabledRichPresence)
            {
                // Update Status every frame
                try
                {
                    var activityManager = discord.GetActivityManager();
                    var activity = new Activity
                    {
                        Details = details,
                        State = state,
                        Assets =
                {
                    LargeImage = largeImage,
                    LargeText = largeText,
                    SmallImage = smallImage,
                    SmallText = smallText
                },
                        Timestamps =
                {
                    Start = startTime,
                }
                    };

                    activityManager.UpdateActivity(activity, (res) =>
                    {
                        if (res != Result.Ok) Debug.LogWarning("Failed connecting to Discord!");
                    });
                }
                catch
                {
                    // If updating the status fails, Destroy the GameObject
                    //Destroy(gameObject); disabled as it will destroy the singleton
                    Debug.Log("Tried to Update Status for Discord RP, but failed!");
                    enabledRichPresence = false;
                }
            }
        }

        //No Small Image
        public void UpdateStatus(string details, string state, string largeImage, string largeText)
        {
            if (enabledRichPresence)
            {
                // Update Status every frame
                try
                {
                    var activityManager = discord.GetActivityManager();
                    var activity = new Activity
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
                        if (res != Result.Ok) Debug.LogWarning("Failed connecting to Discord!");
                    });
                }
                catch
                {
                    // If updating the status fails, Destroy the GameObject
                    //Destroy(gameObject); disabled as it will destroy the singleton
                    Debug.Log("Tried to Update Status for Discord RP, but failed!");
                    enabledRichPresence = false;
                }
            }
        }
    }
}