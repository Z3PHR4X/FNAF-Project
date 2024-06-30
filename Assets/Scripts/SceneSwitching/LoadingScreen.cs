using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Zephrax.FNAFGame.SceneSwitching
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private float delay = 0f;
        [SerializeField] private bool isLoadingScreen = true;
        [SerializeField] private bool waitForKeypress = true;
        [SerializeField] private Image backgroundImage, controlSchemeImage, controlSchemeBackground, progressBar;
        [SerializeField] private TMP_Text loadingText, progressText, mapText, mapDescriptionText;
        [SerializeField] private AudioSource loadingMusic;

        private float fadeDuration = 1f;
        private Animator fadeAnimator;


        public string _sceneToLoad;

        private void Awake()
        {
            loadingMusic.volume = (PlayerPrefs.GetFloat("audioMusicVolume") * PlayerPrefs.GetFloat("audioMasterVolume"));
        }

        // Start is called before the first frame update
        void Start()
        {
            //fadeAnimator = GameObject.FindWithTag("SceneTransition").GetComponent<Animator>();
            if (isLoadingScreen)
            {
                //Level details
                _sceneToLoad = Singleton.Instance.selectedMap.levelScene.Name;
                mapText.text = $"{Singleton.Instance.selectedMap.levelName} - Night {Singleton.Instance.selectedNight}";
                mapDescriptionText.text = Singleton.Instance.selectedMap.levelDescription;
                if (Singleton.Instance.discord.enabledRichPresence)
                {
                    Singleton.Instance.discord.UpdateStatus($"Preparing Night {Singleton.Instance.selectedNight}", $"Loading the next nightshift..", "mainmenu", "Loading..");
                }

                //ControlScheme
                if (Singleton.Instance.selectedMap.controlScheme != null)
                {
                    controlSchemeImage.sprite = Singleton.Instance.selectedMap.controlScheme;
                }
                else
                {
                    controlSchemeImage.enabled = false;
                    controlSchemeBackground.enabled = false;
                }
                //Level background
                if (Singleton.Instance.selectedMap.levelLoadingBackground.Length > 1)
                {
                    backgroundImage.sprite = Singleton.Instance.selectedMap.levelLoadingBackground[Singleton.Instance.selectedNight - 1];
                }
                else
                {
                    backgroundImage.sprite = Singleton.Instance.selectedMap.levelLoadingBackground[0];
                }
                //Music
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
                //print(asyncOperation.allowSceneActivation);

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
                        {
                            //Activate the Scene
                            print($"Changing scene to: {_sceneToLoad}");
                            asyncOperation.allowSceneActivation = true;
                            //print(asyncOperation.allowSceneActivation);
                        }
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
                print($"Changing scene to: {_sceneToLoad}");
            }
        }

        //TODO: rethink this one
        IEnumerator LoadScene(bool waitForKey, bool fade)
        {
            fadeAnimator.SetTrigger("FadeIn");
            yield return new WaitForSeconds(fadeDuration);
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
                            if (fade)
                            {
                                fadeAnimator.SetTrigger("FadeOut");
                                yield return new WaitForSeconds(fadeDuration);
                            }
                        print($"Changing scene to: {_sceneToLoad}");
                        asyncOperation.allowSceneActivation = true;
                    }

                    yield return new WaitForEndOfFrame();
                }
            }
            else
            {
                //Output the current progress
                while (!asyncOperation.isDone)
                {
                    loadingText.text = "Initializing . .";
                    progressText.text = (asyncOperation.progress * 100) + "%";
                    progressBar.fillAmount = asyncOperation.progress;
                    yield return new WaitForEndOfFrame();
                }
                while (asyncOperation.isDone)
                {
                    fadeAnimator.SetTrigger("FadeOut");
                    yield return new WaitForSeconds(fadeDuration);
                    print($"Changing scene to: {_sceneToLoad}");
                    asyncOperation.allowSceneActivation = true;
                }
            }
        }
    }
}