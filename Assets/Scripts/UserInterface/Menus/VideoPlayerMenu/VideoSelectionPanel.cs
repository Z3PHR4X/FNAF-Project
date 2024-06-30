using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace Zephrax.FNAFGame.Menus.VideoPlayerMenu
{
    public class VideoSelectionPanel : MonoBehaviour
    {
        public GameObject videoItemPrefab;
        public Transform itemHolderTransform;
        public List<VideoItemContent> videoItems;
        public List<VideoItem> videoItemInstances;

        private void Start()
        {
            PopulatePanels();
        }

        public void PopulatePanels()
        {
            for (int x = videoItemInstances.Count -1; x >= 0; x--)
            {
                VideoItem panel = videoItemInstances[x];
                videoItemInstances.Remove(panel);
                GameObject obj = panel.gameObject;
                print("destroying " + obj);
                Object.Destroy(obj);
            }


            foreach (VideoItemContent videoItem in videoItems)
            {
                GameObject instance = Instantiate(videoItemPrefab, itemHolderTransform);
                VideoItem vidInst = instance.GetComponent<VideoItem>();
                vidInst.titleText = videoItem.title;
                vidInst.iconImg = videoItem.icon;
                vidInst.clip = videoItem.clip;
                vidInst.SetupInstance();

                videoItemInstances.Add(instance.GetComponent<VideoItem>());
            }
        }

    }

    [System.Serializable]
    public class VideoItemContent
    {
        public string title;
        public Sprite icon;
        public VideoClip clip;
    }
}