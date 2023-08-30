using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private Slider masterSlider, musicSlider, voiceSlider, sfxSlider, interfaceSlider;
    [SerializeField] private Text masterText, musicText, voiceText, sfxText, interfaceText;
    private float master, music, voice, sfx, interfac;

    // Start is called before the first frame update
    void Start()
    {
        LoadSettings();   
    }

    public void LoadSettings()
    {
        master = PlayerPrefs.GetFloat("audioMasterVolume");
        masterSlider.value = master;
        masterText.text = master.ToString();
        music = PlayerPrefs.GetFloat("audioMusicVolume");
        musicSlider.value = music;
        musicText.text = music.ToString();
        voice = PlayerPrefs.GetFloat("audioVoiceVolume");
        voiceSlider.value = voice;
        voiceText.text = voice.ToString();
        sfx = PlayerPrefs.GetFloat("audioSfxVolume");
        sfxSlider.value = sfx;
        sfxText.text = sfx.ToString();
        interfac = PlayerPrefs.GetFloat("audioInterfaceVolume");
        interfaceSlider.value = interfac;
        interfaceText.text = interfac.ToString();
    }

    public void SetMasterVolume()
    {
        master = Mathf.Round(masterSlider.value * 100.0f) * 0.01f;
        masterText.text = master.ToString();
        Singleton.Instance.masterVolume = master;
    }
    public void SetMusicVolume()
    {
        music = Mathf.Round(musicSlider.value * 100.0f) * 0.01f;
        musicText.text = music.ToString();
        Singleton.Instance.musicVolume = music;
    }
    public void SetVoiceVolume()
    {
        voice = Mathf.Round(voiceSlider.value * 100.0f) * 0.01f;
        voiceText.text = voice.ToString();
        Singleton.Instance.voiceVolume = voice;
    }
    public void SetSfxVolume()
    {
        sfx = Mathf.Round(sfxSlider.value * 100.0f) * 0.01f;
        sfxText.text = sfx.ToString();
        Singleton.Instance.sfxVolume = sfx;
    }
    public void SetInterfaceVolume()
    {
        interfac = Mathf.Round(interfaceSlider.value * 100.0f) * 0.01f;
        interfaceText.text = interfac.ToString();
        Singleton.Instance.interfaceVolume = interfac;
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("audioMasterVolume", master);
        PlayerPrefs.SetFloat("audioMusicVolume", music);
        PlayerPrefs.SetFloat("audioVoiceVolume", voice);
        PlayerPrefs.SetFloat("audioSfxVolume",sfx);
        PlayerPrefs.SetFloat("audioInterfaceVolume", interfac);
    }

    public void Cancel()
    {
        master = PlayerPrefs.GetFloat("audioMasterVolume");
        Singleton.Instance.masterVolume = master;
        music = PlayerPrefs.GetFloat("audioMusicVolume");
        Singleton.Instance.musicVolume = music;
        voice = PlayerPrefs.GetFloat("audioVoiceVolume");
        Singleton.Instance.voiceVolume = voice;
        sfx = PlayerPrefs.GetFloat("audioSfxVolume");
        Singleton.Instance.sfxVolume = sfx;
        interfac = PlayerPrefs.GetFloat("audioInterfaceVolume");
        Singleton.Instance.interfaceVolume = interfac;
    }
}
