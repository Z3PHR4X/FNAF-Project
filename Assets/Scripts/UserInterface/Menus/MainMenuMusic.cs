using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuMusic : MonoBehaviour
{
    public AudioSource menuMusicSource;
    [SerializeField] private AudioClip defaultMusic, varianceMusic, completedMusic;
    [SerializeField] private Toggle enableCompletedMusic;
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
        //SetupMusicSetting();
        SetMusic();
    }

    private void SetupMusicSetting()
    {
        
    }

    public void SetMusic()
    {
        menuMusicSource.volume = Singleton.Instance.musicVolume * Singleton.Instance.masterVolume * originalVolume;
        if (Singleton.Instance.completedNight>=7 && enableCompletedMusic.isOn)
        {
            menuMusicSource.clip = completedMusic;
            menuMusicSource.Play();
        }
        else if (Singleton.Instance.completedNight >= 5 && enableCompletedMusic.isOn)
        {
            menuMusicSource.clip = varianceMusic;
            menuMusicSource.Play();
        }
        else
        {
            menuMusicSource.clip = defaultMusic;
            menuMusicSource.Play();
        }
        if(enableCompletedMusic.isOn)
        {
            PlayerPrefs.SetInt("enableSpecialMusic", 1);
        }
        else
        {
            PlayerPrefs.SetInt("enableSpecialMusic", 0);
        }
        
    }
}
