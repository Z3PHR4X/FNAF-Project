using UnityEngine;

namespace Zephrax.FNAFGame.Tools
{
    public class ReturnToMenuButton : MonoBehaviour
    {
        public void ReturnToMenu()
        {
            Singleton.Instance.ChangeScene("MainMenu");
        }

        public void LoadNewScene(string sceneName)
        {
            Singleton.Instance.ChangeScene(sceneName);
        }
    }

}