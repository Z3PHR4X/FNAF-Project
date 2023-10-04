using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerV2 : MonoBehaviour
{
    public static GameManagerV2 Instance;

    [Header("Setup")]
    [SerializeField] private GameObject startNightScreen;
    [SerializeField] private GameObject endNightScreen;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private Events.MusicBox musicBox;
    [Header("In-game values")]
    public bool hasGameStarted;
    public bool isPaused;
    public bool isGameOver;
    public bool hasPlayerWon;
    public int night;
    public int nightLength;
    public int hour;
    public float hourLength;
    public float hourTimer;
    [Header("Enable/Disable under circumstances")]
    [SerializeField] private List<GameObject> gameplayObjects;
    [SerializeField] private List<GameObject> powerObjects;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        night = Singleton.Instance.selectedNight;
        nightLength = Singleton.Instance.selectedMap.nightLength;
        hourLength = Singleton.Instance.selectedMap.hourLength;
        hour = 0;
        hasGameStarted = false;
        isPaused = false;
        isGameOver = false;
        hasPlayerWon = false;
        hourTimer = Time.time;
        //Singleton.Instance.canRetryNight = false;

        foreach (GameObject obj in gameplayObjects)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in powerObjects)
        {
            obj.SetActive(false);
        }
    }

    private void Update()
    {
        if (hasGameStarted)
        {
            if(hasPlayerWon)
            {
                PlayerWin();
            }
            else if (isGameOver) 
            { 
                GameOver();
            }
            else
            {
                if(!Player.Instance.isAlive)
                {
                    isGameOver = true;
                }
                if(!Player.Instance.powerManager.hasPower)
                {
                    PowerDown();
                }
                if (hourTimer + hourLength < Time.time)
                {
                    AdvanceNight();
                }
            }
        }
        else
        {
            StartGame();
        }
    }

    public void PreGameSequence(float length)
    {
        startNightScreen.SetActive(true);
        if (hourTimer + length < Time.time)
        {
            foreach (GameObject obj in gameplayObjects)
            {
                obj.SetActive(true);
            }
            foreach (GameObject obj in powerObjects)
            {
                obj.SetActive(true);
            }
            startNightScreen.SetActive(false);
            print("Game has started!");
            hourTimer = Time.time;
            hasGameStarted = true;
        }
    }

    public void StartGame()
    {
        PreGameSequence(4);
    }

    public void GameOver()
    {
        print("Player has lost!");
        Singleton.Instance.canRetryNight = true;
        Singleton.Instance.ChangeScene("GameOver");
    }

    public void PlayerWin()
    {
        if (!endNightScreen.activeSelf)
        {
            endNightScreen.SetActive(true);
            foreach (GameObject obj in gameplayObjects)
            {
                obj.SetActive(false);
            }
            foreach (GameObject obj in powerObjects)
            {
                obj.SetActive(false);
            }
            if (night > Singleton.Instance.completedNight)
            {
                Singleton.Instance.completedNight = Mathf.Clamp(night, 1, 8);
                PlayerPrefs.SetInt("completedNight", Singleton.Instance.completedNight);
                PlayerPrefs.Save();
            }
            Singleton.Instance.canRetryNight = false;
            print("Player has won!");
        }
    }

    public void AdvanceNight()
    {
        if(!isGameOver ||  !hasPlayerWon)
        {
            if(isNightOver(hour + 1, nightLength))
            {
                hasPlayerWon = true;
            }
            else
            {
                hour++;
                hourTimer = Time.time;
            }
        }
    }

    public void PowerDown()
    {
        if (!musicBox.isActiveAndEnabled)
        {
            foreach (GameObject obj in gameplayObjects)
            {
                obj.SetActive(false);
            }
            foreach (GameObject obj in powerObjects)
            {
                obj.SetActive(false);
            }
            musicBox.gameObject.SetActive(true);
        }
    }

    private bool isNightOver(int curTime, int endTime)
    {
        if (curTime == endTime)
        {
            return true;
        }
        else
        {
            return  false;
        }
    }
}