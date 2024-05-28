using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Zephrax.FNAFGame.Gameplay
{
    public class NightComplete : MonoBehaviour
    {
        private string sceneToLoad;
        public int loadDelay = 5;

        // Start is called before the first frame update
        void Start()
        {
            ReturnToMenu();
        }

        public void ReturnToMenu()
        {
            if (Singleton.Instance.selectedNight == 5 || Singleton.Instance.selectedNight == 7)
            {
                sceneToLoad = "GameWin";
            }
            else
            {
                sceneToLoad = "MainMenu";
            }

            //Singleton.Instance.ChangeScene(sceneToLoad, true);
            StartCoroutine(ChangeScene(sceneToLoad, loadDelay));
        }

        private IEnumerator ChangeScene(string nextScene, int delay)
        {
            yield return new WaitForSeconds(delay);

            //create async operation
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);

            while (!asyncOperation.isDone)
            {

                yield return new WaitForEndOfFrame();
            }
        }

    }
}