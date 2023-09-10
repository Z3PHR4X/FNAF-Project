using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreenScript : MonoBehaviour
{
    public bool isDeprecated = true;
    [SerializeField] private bool isLoadingScreen = true;
    [SerializeField] private Image progressBar;
    [SerializeField] private Text loadingText;
    [SerializeField] private Text progressText;
    [SerializeField] private Text levelText;
    [SerializeField] private Text descriptionText;
    [SerializeField] private Image backgroundImage;

    [SerializeField] private Sprite[] backgrounds;
    [SerializeField] private string[] levelNames;
    [TextArea] public string[] levelDescriptions;
    
    private int _sceneToLoad;
    
    // Start is called before the first frame update
    void Start()
    {
        _sceneToLoad = PlayerPrefs.GetInt("selectedLevel");

        if (isLoadingScreen)
        {
            levelText.text = levelNames[_sceneToLoad - 2];
            descriptionText.text = levelDescriptions[_sceneToLoad - 2];
            backgroundImage.sprite = backgrounds[_sceneToLoad - 2];
        }

        //start async operation
        StartCoroutine(LoadScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LoadScene()
    {
        //create async operation
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(_sceneToLoad);
        asyncOperation.allowSceneActivation = false;
        
        while (!asyncOperation.isDone)
        {
            //Output the current progress
            loadingText.text = "Loading: ";
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
}
