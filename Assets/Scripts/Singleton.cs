using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton : MonoBehaviour
{
    public static Singleton Instance;

    public string sceneToLoad = "";
    public int selectedNight, completedNight;
    public bool overrideCompletedNight, canRetryNight;
    public List<Level> availableLevels = new List<Level>();
    public Level selectedMap;
    public float masterVolume, musicVolume, sfxVolume, voiceVolume, interfaceVolume, mouseSensitivity;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        if (selectedMap == null)
        {
            selectedMap = availableLevels[0];
        }
        print("Singleton successfully initialized!");

        if (overrideCompletedNight)
        {
            PlayerPrefs.SetInt("completedNight", completedNight);
        }

        completedNight = PlayerPrefs.GetInt("completedNight");
        //Audio
        masterVolume = PlayerPrefs.GetFloat("audioMasterVolume");
        musicVolume = PlayerPrefs.GetFloat("audioMusicVolume");
        sfxVolume = PlayerPrefs.GetFloat("audioSfxVolume");
        voiceVolume = PlayerPrefs.GetFloat("audioVoiceVolume");
        interfaceVolume = PlayerPrefs.GetFloat("audioInterfaceVolume");
        //Controls
        mouseSensitivity = PlayerPrefs.GetFloat("mouseSensitivity");

        print("Settings loaded from save to Singleton instance");
    }

    //Simple function to change scenes, no loading screens
    public void ChangeScene(string sceneName)
    {
        print("Changing scene to:" + sceneName);
        SceneManager.LoadScene(sceneName);        
    }

    //Exits the application.. 
    public void ExitApplication()
    {
        print("Exitting application..");
        Application.Quit();
    }
}
