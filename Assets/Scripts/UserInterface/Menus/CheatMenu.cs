using UnityEngine;
using UnityEngine.UI;

public class CheatMenu : MonoBehaviour
{
    [SerializeField] private GameObject cheatIndicator;
    [SerializeField] private Text cheatStatus;
    public bool cheatsAvailable = true;
    public bool cheatsEnabled;
    public bool isIngame;
    // Update is called once per frame

    private void Start()
    {
        if (isIngame)
        {
            cheatStatus.text = "";
        }
        else
        {
            cheatStatus.text = "Press F1-F7 to set night progress";
        }
    }

    void Update()
    {

        if (cheatsAvailable){
            if (Input.GetKey(KeyCode.F12))
            {
                cheatsEnabled = true;
                cheatIndicator.SetActive(true);
            }

            MenuCheats();
            InGameCheats();
        }
        
    }

    private void InGameCheats()
    {
        if (Input.GetKey(KeyCode.F1) && isIngame && !GameManagerV2.Instance.isPaused && cheatsEnabled)
        {
            //Gamespeed to 1f
            cheatStatus.text = "Regular Game Speed";
        }
        if (Input.GetKey(KeyCode.F2) && isIngame && !GameManagerV2.Instance.isPaused && cheatsEnabled)
        {
            //Gamespeed to 2f
            cheatStatus.text = "Double Game Speed";
        }
        if (Input.GetKey(KeyCode.F3) && isIngame && !GameManagerV2.Instance.isPaused && cheatsEnabled)
        {
            cheatStatus.text = "";
        }
        if (Input.GetKey(KeyCode.F4) && isIngame && !GameManagerV2.Instance.isPaused && cheatsEnabled)
        {
            //DevMode
            //Enable dev UI showing all AI values, power values, time counters
            cheatStatus.text = "Developer Mode activated";
        }
        if (Input.GetKey(KeyCode.F5) && isIngame && !GameManagerV2.Instance.isPaused && cheatsEnabled)
        {
            cheatStatus.text = "";
        }
        if (Input.GetKey(KeyCode.F6) && isIngame && !GameManagerV2.Instance.isPaused && cheatsEnabled) {

            //Lose
            cheatStatus.text = "You lose :(";
        }
        if (Input.GetKey(KeyCode.F7) && isIngame && !GameManagerV2.Instance.isPaused && cheatsEnabled)
        {
            //Win
            cheatStatus.text = "You win :)";
        }
        if (Input.GetKey(KeyCode.F8) && isIngame && !GameManagerV2.Instance.isPaused && cheatsEnabled)
        {
            cheatStatus.text = "";
        }
        if (Input.GetKey(KeyCode.F9) && isIngame && !GameManagerV2.Instance.isPaused && cheatsEnabled)
        {
            cheatStatus.text = "";
        }
        if (Input.GetKey(KeyCode.F10) && isIngame && !GameManagerV2.Instance.isPaused && cheatsEnabled)
        {
            cheatStatus.text = "";
        }
        if (Input.GetKey(KeyCode.F11) && isIngame && !GameManagerV2.Instance.isPaused && cheatsEnabled)
        {
            cheatStatus.text = "";
        }
    }

    private void MenuCheats()
    {
        if (Input.GetKey(KeyCode.F1) && !isIngame && cheatsEnabled)
        {
            Singleton.Instance.completedNight = 1;
            PlayerPrefs.SetInt("completedNight", 1);
            cheatStatus.text = "Set completedNight to 1 | Press F11 to apply";
            PlayerPrefs.Save();
        }
        if (Input.GetKey(KeyCode.F2) && !isIngame && cheatsEnabled)
        {
            Singleton.Instance.completedNight = 2;
            PlayerPrefs.SetInt("completedNight", 2);
            cheatStatus.text = "Set completedNight to 2 | Press F11 to apply";
            PlayerPrefs.Save();
        }
        if (Input.GetKey(KeyCode.F3) && !isIngame && cheatsEnabled)
        {
            Singleton.Instance.completedNight = 3;
            PlayerPrefs.SetInt("completedNight", 3);
            cheatStatus.text = "Set completedNight to 3 | Press F11 to apply";
            PlayerPrefs.Save();
        }
        if (Input.GetKey(KeyCode.F4) && !isIngame && cheatsEnabled)
        {
            Singleton.Instance.completedNight = 4;
            PlayerPrefs.SetInt("completedNight", 4);
            cheatStatus.text = "Set completedNight to 4 | Press F11 to apply";
            PlayerPrefs.Save();
        }
        if (Input.GetKey(KeyCode.F5) && !isIngame && cheatsEnabled)
        {
            Singleton.Instance.completedNight = 5;
            PlayerPrefs.SetInt("completedNight", 5);
            cheatStatus.text = "Set completedNight to 5 | Press F11 to apply";
            PlayerPrefs.Save();
        }
        if (Input.GetKey(KeyCode.F6) && !isIngame && cheatsEnabled)
        {
            Singleton.Instance.completedNight = 6;
            PlayerPrefs.SetInt("completedNight", 6);
            cheatStatus.text = "Set completedNight to 6 | Press F11 to apply";
            PlayerPrefs.Save();
        }
        if (Input.GetKey(KeyCode.F7) && !isIngame && cheatsEnabled)
        {
            Singleton.Instance.completedNight = 7;
            PlayerPrefs.SetInt("completedNight", 7);
            cheatStatus.text = "Set completedNight to 7 | Press F11 to apply";
            PlayerPrefs.Save();
        }

        if (Input.GetKey(KeyCode.F11) && !isIngame && cheatsEnabled)
        {
            Singleton.Instance.ChangeScene("MainMenu");
        }
    }


}
