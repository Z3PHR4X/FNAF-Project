using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton : MonoBehaviour
{
    public static Singleton Instance;

    public string sceneToLoad = "";
    public int selectedNight, completedNight;
    public bool overrideCompletedNight, canRetryNight, debugMode;
    public string deathReason;
    public List<Level> availableLevels = new List<Level>();
    public Level selectedMap;
    public float masterVolume, musicVolume, sfxVolume, voiceVolume, interfaceVolume, mouseSensitivity;
    private Animator transitionAnimator;

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
        transitionAnimator = GetComponent<Animator>();
        print("Singleton successfully initialized!");

        if (overrideCompletedNight)
        {
            PlayerPrefs.SetInt("completedNight", completedNight);
            PlayerPrefs.Save();
        }
        debugMode = false;
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

    public void SetDeathMessage(string characterName, string doorName)
    {
        string door;
        switch(doorName){
            case "LeftDoor":
                door = "left door";
                break;
            case "VentDoor":
                door = "vent";
                break;
            case "RightDoor":
                door = "right door";
                break;
            default:
                door = "unknown";
                break;
        }
        deathReason = $"{characterName} attacked you from the {door}..";
    }

    public void SetDeathMessage(string message)
    {
        deathReason = message;
    }

    //Simple function to change scenes, no loading screens
    public void ChangeScene(string sceneName)
    {
        print("Changing scene to: " + sceneName);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(sceneName);        
    }

    public void ChangeScene(string sceneName, bool fade)
    {
        transitionAnimator = GameObject.FindWithTag("SceneTransition").GetComponent<Animator>();
        float transitionTime = 1f;
        print($"Changing scene to: {sceneName} with fade of {transitionTime}s");
        Time.timeScale = 1.0f;
        StartCoroutine(LoadSceneFade(sceneName, transitionTime));
    }

    //Exits the application.. 
    public void ExitApplication()
    {
        print("Exitting application..");
        Application.Quit();
        //StartCoroutine(ExitSequence(1f));
    }

    IEnumerator LoadSceneFade(string sceneName, float transitionTime)
    {
        transitionAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(transitionTime);

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        yield return new WaitForEndOfFrame();

    }

    IEnumerator ExitSequence(float transitionTime)
    {
        transitionAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(transitionTime);
        Application.Quit();
    }
}
