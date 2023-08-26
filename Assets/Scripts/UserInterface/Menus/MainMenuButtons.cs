using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private Text ContinueText;
    [SerializeField] private GameObject ContinueButton, SetupButton;
    [SerializeField] private bool ContinueAvailable, SetupAvailable;

    private int completedNight;

    // Start is called before the first frame update
    void Start()
    {
        completedNight = PlayerPrefs.GetInt("completedNight");

        if(completedNight > 0) {
            ContinueAvailable = true;
            ContinueButton.SetActive(true);
            ContinueText.text = "Night " + Mathf.Clamp(completedNight + 1, 1, 6);
        }
        else
        {
            ContinueButton.SetActive(false);
        }

        if (completedNight > 5)
        {
            SetupAvailable = true;
            SetupButton.SetActive(true);
        }
        else
        {
            SetupButton.SetActive(false);
        }

    }

    public void StartNewGame()
    {
        print("Starting new game..");
        completedNight = 0;
        PlayerPrefs.SetInt("completedNight", 0);
        Singleton.Instance.selectedNight = 1;
        Singleton.Instance.ChangeScene("LoadingScreen");
    }

    public void ContinueGame()
    {
        Singleton.Instance.selectedNight = Mathf.Clamp(completedNight + 1, 1, 6);
        print("Continuing game on night " + Singleton.Instance.selectedNight); ;
        Singleton.Instance.ChangeScene("LoadingScreen");
    }
}
