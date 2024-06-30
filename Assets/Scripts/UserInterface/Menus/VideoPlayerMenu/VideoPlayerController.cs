using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Zephrax.FNAFGame.Menus.VideoPlayerMenu
{
    public class VideoPlayerController : MonoBehaviour
    {
        [SerializeField] private VideoClip defaultVid;
        [SerializeField] private string defaultVidName;
        [SerializeField] private TMP_Text videoTitle;
        [SerializeField] private Slider progressSlider;
        [SerializeField] private Slider volumeSlider;
        [SerializeField] private AudioSource playerAudioSource;
        public VideoPlayer player;
        private string nowPlayingTitle;

        private void Awake()
        {
            volumeSlider.value = 0.3f;
        }

        // Start is called before the first frame update
        void Start()
        {
            Singleton.Instance.discord.UpdateStatus("Accessing the video archive", $"Now playing: Bite_of_87.mp4", "videoarchive", "I always come back..");
            SwitchVideoClip(defaultVid, defaultVidName);
            StartVideoPlayback();
        }

        // Update is called once per frame
        void Update()
        {
            if (player.isPlaying)
            {
                progressSlider.value = (float)(player.time / player.length);
            }
        }

        public void UpdateVideoVolume()
        {
            playerAudioSource.volume = volumeSlider.value;
        }

        public void ChangeVideoPlaybackTime()
        {
            if (progressSlider.IsInteractable())
            {
                float sliderval = progressSlider.value;
                player.time = (float)(player.length * sliderval);
            }

        }

        public void SwitchVideoClip(VideoClip newVideoClip, string newVideoTitle)
        {
            progressSlider.interactable = false;
            player.Stop();
            nowPlayingTitle = newVideoTitle;
            videoTitle.text = nowPlayingTitle;
            player.clip = newVideoClip;
            player.Play();
        }

        public void StopVideoPlayback()
        {
            player.time = 0;
            player.Stop();
            progressSlider.interactable = true;
        }

        public void PauseVideoPlayback()
        {
            player.Pause();
            progressSlider.interactable = true;
        }

        public void StartVideoPlayback()
        {
            player.Play();
            progressSlider.interactable = false;
        }
    }

}