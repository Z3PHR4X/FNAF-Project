using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Zephrax.FNAFGame.UserInterface
{
    public class GameOverScreen : MonoBehaviour
    {
        private int delay;
        public string sceneToLoad;
        [SerializeField] private GameObject continueText;

        // Start is called before the first frame update
        void Start()
        {
            continueText.SetActive(false);
            if (Singleton.Instance.discord.enabledRichPresence)
            {
                Singleton.Instance.discord.UpdateStatus($"{Singleton.Instance.deathReason}", $"Failed to survive the night..", "gameover", "Game Over");
            }
            StartCoroutine("LoadScene", sceneToLoad);  
        }

        IEnumerator LoadScene(string _sceneToLoad)
        {
            yield return new WaitForSeconds(delay);

            //create async operation
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);

            asyncOperation.allowSceneActivation = false;

            while (!asyncOperation.isDone)
            {
                // Check if the load has finished
                if (asyncOperation.progress >= 0.9f)
                {
                    //Change the Text to show the Scene is ready
                    continueText.SetActive(true);
                    //Wait to you press any key to activate the Scene
                    if (Input.anyKey)
                        //Activate the Scene
                        asyncOperation.allowSceneActivation = true;
                }

                yield return new WaitForEndOfFrame();
            }
        }

       
    }
}
