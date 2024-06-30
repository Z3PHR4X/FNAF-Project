using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;

namespace Zephrax.FNAFGame.Menus.VideoPlayerMenu
{

    public class VideoItem : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text title;
        public string titleText;
        public Sprite iconImg;
        public VideoClip clip;
        private VideoPlayerController playerController;

        // Start is called before the first frame update
        void Start()
        {
            playerController = FindAnyObjectByType<VideoPlayerController>();
        }

        public void SetupInstance()
        {
            icon.sprite = iconImg;
            title.text = titleText;
        }

        public void SelectVideoItem()
        {
            playerController.SwitchVideoClip(clip, titleText);
        }
    }

}