using UnityEngine;

namespace Zephrax.FNAFGame.SceneSwitching
{
    public class ChangeScene : MonoBehaviour
    {
        private Animator transitionAnimator;
        private CanvasGroup canvasGroup;

        // Start is called before the first frame update
        void Start()
        {
            transitionAnimator = GetComponent<Animator>();
            transitionAnimator.SetTrigger("out");
        }

        public void ChangeTheScene(string newScene)
        {
            Time.timeScale = 1.0f;
            Singleton.Instance.sceneToLoad = newScene;
            transitionAnimator.SetTrigger("in");
        }
    }
}