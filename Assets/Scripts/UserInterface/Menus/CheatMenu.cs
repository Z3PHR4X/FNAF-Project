using UnityEngine;
using UnityEngine.UI;

namespace Zephrax.FNAFGame.UserInterface.Menus
{
    public class CheatMenu : MonoBehaviour
    {
        [SerializeField] private GameObject cheatIndicator;
        [SerializeField] private Text cheatStatus;
        public bool cheatsAvailable = true;
        public bool cheatsEnabled;
        // Update is called once per frame

        private void Start()
        {
            cheatStatus.text = "Press F1-F7 to set night progress";
        }

        void Update()
        {

            if (cheatsAvailable)
            {
                if (Input.GetKey(KeyCode.F12))
                {
                    cheatsEnabled = true;
                    cheatIndicator.SetActive(true);
                }

                MenuCheats();
            }

        }

        private void MenuCheats()
        {
            if (Input.GetKey(KeyCode.F1) && cheatsEnabled)
            {
                Singleton.Instance.completedNight = 1;
                PlayerPrefs.SetInt("completedNight", 1);
                cheatStatus.text = "Set completedNight to 1 | Press F11 to apply";
                PlayerPrefs.Save();
            }
            if (Input.GetKey(KeyCode.F2) && cheatsEnabled)
            {
                Singleton.Instance.completedNight = 2;
                PlayerPrefs.SetInt("completedNight", 2);
                cheatStatus.text = "Set completedNight to 2 | Press F11 to apply";
                PlayerPrefs.Save();
            }
            if (Input.GetKey(KeyCode.F3) && cheatsEnabled)
            {
                Singleton.Instance.completedNight = 3;
                PlayerPrefs.SetInt("completedNight", 3);
                cheatStatus.text = "Set completedNight to 3 | Press F11 to apply";
                PlayerPrefs.Save();
            }
            if (Input.GetKey(KeyCode.F4) && cheatsEnabled)
            {
                Singleton.Instance.completedNight = 4;
                PlayerPrefs.SetInt("completedNight", 4);
                cheatStatus.text = "Set completedNight to 4 | Press F11 to apply";
                PlayerPrefs.Save();
            }
            if (Input.GetKey(KeyCode.F5) && cheatsEnabled)
            {
                Singleton.Instance.completedNight = 5;
                PlayerPrefs.SetInt("completedNight", 5);
                cheatStatus.text = "Set completedNight to 5 | Press F11 to apply";
                PlayerPrefs.Save();
            }
            if (Input.GetKey(KeyCode.F6) && cheatsEnabled)
            {
                Singleton.Instance.completedNight = 6;
                PlayerPrefs.SetInt("completedNight", 6);
                cheatStatus.text = "Set completedNight to 6 | Press F11 to apply";
                PlayerPrefs.Save();
            }
            if (Input.GetKey(KeyCode.F7) && cheatsEnabled)
            {
                Singleton.Instance.completedNight = 7;
                PlayerPrefs.SetInt("completedNight", 7);
                cheatStatus.text = "Set completedNight to 7 | Press F11 to apply";
                PlayerPrefs.Save();
            }

            if (Input.GetKey(KeyCode.F11) && cheatsEnabled)
            {
                Singleton.Instance.ChangeScene("MainMenu");
            }
        }


    }
}