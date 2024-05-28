using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Zephrax.FNAFGame.UserInterface.Menus
{
    public class LevelSelection : MonoBehaviour
    {
        private Level selectedMap;
        [SerializeField] private Dropdown selectionDropdown;
        private List<Level> availableLevels;

        [SerializeField] private Text levelDescriptionText;
        [SerializeField] private Image levelThumbnailImage;
        [SerializeField] private AudioSource audioSource;
        private NightSelection nightSelection;

        private void Awake()
        {
            audioSource.volume = Singleton.Instance.masterVolume * Singleton.Instance.interfaceVolume;

            if (selectionDropdown == null)
            {
                selectionDropdown = GetComponent<Dropdown>();
            }

            nightSelection = FindObjectOfType<NightSelection>();

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
            int index = Singleton.Instance.availableLevels.IndexOf(selectedMap);
            selectionDropdown.value = index;
            selectionDropdown.RefreshShownValue();
            UpdateInterface();
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
            nightSelection.PopulateNightOptions();
        }

        public void UpdateInterface()
        {
            levelDescriptionText.text = selectedMap.levelDescription;
            levelThumbnailImage.sprite = selectedMap.levelThumbnail;
        }
    }
}