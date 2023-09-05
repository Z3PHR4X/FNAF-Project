using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] private PlayerScript player;
    public GameObject pauseMenu;
    private bool paused;

    
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
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
            pauseMenu.SetActive(false);
            paused = false;
        }
        else
        {
            Time.timeScale = 0.0f;
            pauseMenu.SetActive(true);
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