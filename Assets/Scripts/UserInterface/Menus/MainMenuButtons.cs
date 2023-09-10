using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private Text ContinueText, RetryText;
    [SerializeField] private GameObject ContinueButton, RetryButton, SetupButton;
    [SerializeField] private int SetupButtonNightUnlock = 5;
    
    private int completedNight, retryNight;

    // Start is called before the first frame update
    void Start()
    {
        completedNight = PlayerPrefs.GetInt("completedNight");
        retryNight = Singleton.Instance.selectedNight;

        if(completedNight > 0 && completedNight < 7) {
            ContinueButton.SetActive(true);
            int nextNight = Mathf.Clamp(completedNight + 1, 1, 7);
            ContinueText.text = "Start night " + nextNight;
            if (nextNight == 6) ContinueText.color = Color.yellow;
            if (nextNight == 7) ContinueText.color = Color.red;
        }
        else
        {
            ContinueButton.SetActive(false);
        }

        if (Singleton.Instance.canRetryNight && completedNight+1 != retryNight)
        {
            RetryText.text = "Retry Night " + retryNight;
            RetryButton.SetActive(true);
        }
        else
        {
            RetryButton.SetActive(false);
        }

        if (completedNight >= SetupButtonNightUnlock)
        {
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
        Singleton.Instance.completedNight = 0;
        Singleton.Instance.ChangeScene("LoadingScreen");
    }

    public void ContinueGame()
    {
        Singleton.Instance.selectedNight = Mathf.Clamp(completedNight + 1, 1, 7);
        print("Continuing game on night " + Singleton.Instance.selectedNight); ;
        Singleton.Instance.ChangeScene("LoadingScreen");
    }

    public void RetryGame()
    {
        print("Retrying night: " + retryNight);
        Singleton.Instance.selectedNight = retryNight;
        Singleton.Instance.ChangeScene("LoadingScreen");
    }
}
