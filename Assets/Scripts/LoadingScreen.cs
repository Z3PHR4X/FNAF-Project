using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private float delay = 0f;
    [SerializeField] private bool isLoadingScreen = true;
    [SerializeField] private bool waitForKeypress = true;
    [SerializeField] private Image backgroundImage, progressBar;
    [SerializeField] private Text loadingText, progressText, mapText, mapDescriptionText;
    [SerializeField] private AudioSource loadingMusic;

    public string _sceneToLoad;

    private void Awake()
    {
        loadingMusic.volume = (PlayerPrefs.GetFloat("audioMusicVolume") * PlayerPrefs.GetFloat("audioMasterVolume"));
    }

    // Start is called before the first frame update
    void Start()
    {       
        if (isLoadingScreen)
        {
            _sceneToLoad = Singleton.Instance.selectedMap.levelScene.Name;
            mapText.text = Singleton.Instance.selectedMap.levelName;
            mapDescriptionText.text = Singleton.Instance.selectedMap.levelDescription;
            backgroundImage.sprite = Singleton.Instance.selectedMap.levelLoadingBackground;
            loadingMusic.clip = Singleton.Instance.selectedMap.selectionMusic;
            loadingMusic.Play();
        }

        progressText.text = "0%";
        progressBar.fillAmount = 0;

        //start async operation
        StartCoroutine(LoadScene(waitForKeypress));
    }

    IEnumerator LoadScene(bool waitForKey)
    {
        yield return new WaitForSeconds(delay);

        //create async operation
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(_sceneToLoad);

        if (waitForKey)
        {
            asyncOperation.allowSceneActivation = false;

            while (!asyncOperation.isDone)
            {
                //Output the current progress
                loadingText.text = "Loading . .";
                progressText.text = (asyncOperation.progress * 100) + "%";
                progressBar.fillAmount = asyncOperation.progress;

                // Check if the load has finished
                if (asyncOperation.progress >= 0.9f)
                {
                    //Change the Text to show the Scene is ready
                    progressBar.fillAmount = 1;
                    loadingText.text = "Press any key to continue";
                    progressText.text = "100%";
                    //Wait to you press any key to activate the Scene
                    if (Input.anyKey)
                        //Activate the Scene
                        asyncOperation.allowSceneActivation = true;
                }

                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            //Output the current progress
            asyncOperation.allowSceneActivation = true;
                      
            while (!asyncOperation.isDone)
            {
                loadingText.text = "Initializing . .";
                progressText.text = (asyncOperation.progress * 100) + "%";
                progressBar.fillAmount = asyncOperation.progress;
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
