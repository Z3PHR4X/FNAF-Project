using System.Collections.Generic;
using UnityEngine;
using Zephrax.FNAFGame.Gameplay;

namespace Zephrax.FNAFGame.UserInterface.Menus
{
    public class PauseMenuScript : MonoBehaviour
    {
        private Canvas pauseMenu;
        [SerializeField] private GameObject mainButtons;
        [SerializeField] private List<GameObject> pauseMenuItems = new List<GameObject>();
        private bool paused;

        private void Awake()
        {
            pauseMenu = GetComponent<Canvas>();
        }

        // Start is called before the first frame update
        void Start()
        {
            pauseMenu.enabled = false;
            mainButtons.SetActive(true);
            foreach (GameObject item in pauseMenuItems)
            {
                item.SetActive(false);
            }

            paused = false;
        }

        // Update is called once per frame
        void Update()
        {
            HandleInput();
        }

        public void ReturnToMenu()
        {
            print("Returning to menu");
            Singleton.Instance.ChangeScene("MainMenu");
        }

        public void TogglePause()
        {
            if (paused)
            {
                Time.timeScale = 1.0f;
                foreach (GameObject item in pauseMenuItems)
                {
                    item.SetActive(false);
                }

                pauseMenu.enabled = false;
                mainButtons.SetActive(false);
                GameManagerV2.Instance.isPaused = false;
                paused = false;
            }
            else
            {
                Time.timeScale = 0.0f;
                mainButtons.SetActive(true);
                pauseMenu.enabled = true;
                GameManagerV2.Instance.isPaused = true;
                paused = true;
            }
        }

        void HandleInput()
        {
            //pause game upon pressing escape
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }
        }
    }
}