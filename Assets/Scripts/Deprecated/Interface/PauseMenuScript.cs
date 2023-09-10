using System.Collections.Generic;
using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{
    public bool isDeprecated = true;
    private Canvas pauseMenu;
    private GameManager gameManager;
    [SerializeField] private GameObject mainButtons;
    [SerializeField] private List<GameObject> pauseMenuItems = new List<GameObject>();
    private bool paused;

    private void Awake()
    {
        pauseMenu = GetComponent<Canvas>();
        gameManager = FindObjectOfType<GameManager>();
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
            gameManager.isGamePaused = false;
            paused = false;
        }
        else
        {
            gameManager.isGamePaused = true;
            Time.timeScale = 0.0f;
            mainButtons.SetActive(true);
            pauseMenu.enabled = true;
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