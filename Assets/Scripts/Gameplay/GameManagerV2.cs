using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerV2 : MonoBehaviour
{
    public static GameManagerV2 Instance;

    public GameObject startNightScreen, endNightScreen, gameOverScreen;
    public bool hasGameStarted, isPaused, isGameOver, hasPlayerWon;
    public int night, nightLength, nightTime;
    public float hourLength, hourTimer;

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
        nightTime = 0;
        hasGameStarted = false;
        isPaused = false;
        isGameOver = false;
        hasPlayerWon = false;

        PreGameSequence();
    }

    private void Update()
    {
        if (hasGameStarted && !isGameOver && !hasPlayerWon)
        {
            if (hourTimer + hourLength > Time.time)
            {
                AdvanceNight();
            }
        }
    }

    public void PreGameSequence()
    {
        //show start night screen

    }

    public void StartGame()
    {
        //
    }

    public void GameOver()
    {

    }

    public void PlayerWin()
    {

    }

    public void AdvanceNight()
    {
        if(!isGameOver ||  !hasPlayerWon)
        {
            if(isNightOver(nightTime + 1, nightLength))
            {
                hasPlayerWon = true;
            }
            else
            {
                nightTime++;
                hourTimer = Time.time;
            }
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