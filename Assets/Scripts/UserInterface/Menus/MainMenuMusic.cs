using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuMusic : MonoBehaviour
{
    public AudioSource menuMusicSource;
    [SerializeField] private AudioClip defaultMusic, variance1Music, variance2Music, completedMusic;
    [SerializeField] private Toggle enableCompletedMusic;
    public bool overrideActive;
    public int musicSelection;
    private float originalVolume;

    // Start is called before the first frame update
    void Start()
    {
        originalVolume = 0.25f;
        if(PlayerPrefs.GetInt("enableSpecialMusic") == 1)
        {
            enableCompletedMusic.isOn = true;
        }
        else
        {
            enableCompletedMusic.isOn = false;
        }
        overrideActive = PlayerPrefs.GetInt("menuMusicOverride") == 1;
        musicSelection = PlayerPrefs.GetInt("menuMusicSelection");

        SetMusic();
    }

    public void SetupMusicSetting(bool newOverride, int newSong)
    {
        overrideActive = newOverride;
        musicSelection = newSong;
        SetMusic();
    }

    public void SetMusic()
    {
        menuMusicSource.volume = Singleton.Instance.musicVolume * Singleton.Instance.masterVolume * originalVolume;
        if (overrideActive)
        {
            switch (musicSelection)
            {
                case 0:
                    menuMusicSource.clip = defaultMusic;
                    menuMusicSource.Play();
                    break;
                case 1:
                    menuMusicSource.clip = variance1Music;
                    menuMusicSource.Play();
                    break;
                case 2:
                    menuMusicSource.clip = variance2Music;
                    menuMusicSource.Play();
                    break;
                case 3:
                    menuMusicSource.clip = completedMusic;
                    menuMusicSource.Play();
                    break;
            }

        }
        else
        {
            if (Singleton.Instance.completedNight >= 7 && enableCompletedMusic.isOn)
            {
                menuMusicSource.clip = completedMusic;
                menuMusicSource.Play();
            }
            else if (Singleton.Instance.completedNight >= 5 && enableCompletedMusic.isOn)
            {
                menuMusicSource.clip = variance2Music;
                menuMusicSource.Play();
            }
            else if (Singleton.Instance.completedNight >= 3 && enableCompletedMusic.isOn)
            {
                menuMusicSource.clip = variance1Music;
                menuMusicSource.Play();
            }
            else
            {
                menuMusicSource.clip = defaultMusic;
                menuMusicSource.Play();
            }
            if (enableCompletedMusic.isOn)
            {
                PlayerPrefs.SetInt("enableSpecialMusic", 1);
            }
            else
            {
                PlayerPrefs.SetInt("enableSpecialMusic", 0);
            }
        }
        
    }
}
