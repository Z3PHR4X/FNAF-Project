using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatMenu : MonoBehaviour
{
    [SerializeField] private GameObject cheatIndicator;
    [SerializeField] private Text cheatStatus;
    public bool cheatsEnabled;
    public bool isIngame;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F12))
        {
            cheatsEnabled = true;
            cheatIndicator.SetActive(true);
        }

        if (Input.GetKey(KeyCode.F1) && !isIngame && cheatsEnabled)
        {
            Singleton.Instance.completedNight = 1;
            PlayerPrefs.SetInt("completedNight", 1);
            cheatStatus.text = "Set completedNight to 1 / Press F11 to apply";
        }
        if (Input.GetKey(KeyCode.F2) && !isIngame && cheatsEnabled)
        {
            Singleton.Instance.completedNight = 2;
            PlayerPrefs.SetInt("completedNight", 2);
            cheatStatus.text = "Set completedNight to 2 / Press F11 to apply";
        }
        if (Input.GetKey(KeyCode.F3) && !isIngame && cheatsEnabled)
        {
            Singleton.Instance.completedNight = 3;
            PlayerPrefs.SetInt("completedNight", 3);
            cheatStatus.text = "Set completedNight to 3 / Press F11 to apply";
        }
        if (Input.GetKey(KeyCode.F4) && !isIngame && cheatsEnabled)
        {
            Singleton.Instance.completedNight = 4;
            PlayerPrefs.SetInt("completedNight", 4);
            cheatStatus.text = "Set completedNight to 4 / Press F11 to apply";
        }
        if (Input.GetKey(KeyCode.F5) && !isIngame && cheatsEnabled)
        {
            Singleton.Instance.completedNight = 5;
            PlayerPrefs.SetInt("completedNight", 5);
            cheatStatus.text = "Set completedNight to 5 / Press F11 to apply";
        }
        if (Input.GetKey(KeyCode.F6) && !isIngame && cheatsEnabled)
        {
            Singleton.Instance.completedNight = 6;
            PlayerPrefs.SetInt("completedNight", 6);
            cheatStatus.text = "Set completedNight to 6 / Press F11 to apply";
        }
        if (Input.GetKey(KeyCode.F7) && !isIngame && cheatsEnabled)
        {
            Singleton.Instance.completedNight = 7;
            PlayerPrefs.SetInt("completedNight", 7);
            cheatStatus.text = "Set completedNight to 7 / Press F11 to apply";
        }
        if (Input.GetKey(KeyCode.F11) && !isIngame && cheatsEnabled)
        {
            Singleton.Instance.ChangeScene("MainMenu");
        }
    }


}
