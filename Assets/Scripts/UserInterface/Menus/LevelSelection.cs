using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface.Menus
{
    public class LevelSelection : MonoBehaviour
    {
        private Level selectedMap;
        [SerializeField] private Dropdown selectionDropdown;
        private List<Level> availableLevels;

        [SerializeField] private Text levelDescriptionText;
        [SerializeField] private Image levelThumbnailImage;
        [SerializeField] private AudioSource audioSource;

        private void Awake()
        {
            audioSource.volume = Singleton.Instance.masterVolume * Singleton.Instance.interfaceVolume;

            if (selectionDropdown == null)
            {
                selectionDropdown = GetComponent<Dropdown>();
            }

            availableLevels = Singleton.Instance.availableLevels;
            List<string> _availableLevelOptions = new List<string>();

            selectionDropdown.ClearOptions();
            foreach(Level level in availableLevels)
            {
                _availableLevelOptions.Add(level.levelName);
            }
            selectionDropdown.AddOptions(_availableLevelOptions);
            selectionDropdown.RefreshShownValue();

        }

        // Start is called before the first frame update      
        void Start()
        {
            selectedMap  = Singleton.Instance.selectedMap;
            UpdateInterface();
            selectionDropdown.RefreshShownValue();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetSelectedMap()
        {
            int selectedMapInt = selectionDropdown.value;
            selectedMap = availableLevels[selectedMapInt];
            UpdateInterface();
            audioSource.clip = selectedMap.selectionSound;
            audioSource.Play();
            Singleton.Instance.selectedMap = selectedMap;
            print("Selected map: " + selectedMap.levelName);
        }

        public void UpdateInterface()
        {
            levelDescriptionText.text = selectedMap.levelDescription;
            levelThumbnailImage.sprite = selectedMap.levelThumbnail;
        }
    }
}