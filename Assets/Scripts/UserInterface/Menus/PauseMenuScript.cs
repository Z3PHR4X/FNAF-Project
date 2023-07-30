using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] private PlayerScript player;
    public GameObject pauseMenu;
    public Camera camera;
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
        SceneManager.LoadScene(0);
    }

    public void TogglePause()
    {
        if (paused)
        {
            Time.timeScale = 1.0f;
            pauseMenu.SetActive(false);
            //camera.audio.play();
            paused = false;
        }
        else
        {
            Time.timeScale = 0.0f;
            pauseMenu.SetActive(true);
            //camera.audio.pause();
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