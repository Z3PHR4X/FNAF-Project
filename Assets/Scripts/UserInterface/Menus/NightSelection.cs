using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface.Menus
{
    public class NightSelection : MonoBehaviour
    {
        private int selectedNight, completedNight;
        private Dropdown selectionDropdown;

        private void Awake()
        {
            selectionDropdown = GetComponent<Dropdown>();
        }

        // Start is called before the first frame update      
        void Start()
        {
            completedNight = PlayerPrefs.GetInt("completedNight");
            selectedNight = Singleton.Instance.selectedNight;
            List<string> _availableNights = new List<string>();

            selectionDropdown.ClearOptions();
            for (int x = 1; x <= completedNight; x++)
            {
                _availableNights.Add("Night " + x);
            }
            selectionDropdown.AddOptions(_availableNights);
            selectionDropdown.value = completedNight;
            selectedNight = completedNight;
            selectionDropdown.RefreshShownValue();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetSelectedNight()
        {
            selectedNight = selectionDropdown.value + 1;
            Singleton.Instance.selectedNight = selectedNight;
            print("Selected night: " + selectedNight);
        }
    }
}