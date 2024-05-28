using UnityEngine;
using UnityEngine.SceneManagement;

namespace Zephrax.FNAFGame.SceneSwitching
{
    public class SceneChangerSM : StateMachineBehaviour
    {
        private string newScene;

        private void OnStateEnter()
        {
            newScene = Singleton.Instance.sceneToLoad;
            ChangeScene(newScene);
        }

        public void ChangeScene(string sceneName)
        {
            Debug.Log($"Loading scene: {sceneName}");
            SceneManager.LoadScene(sceneName);
        }
    }
}