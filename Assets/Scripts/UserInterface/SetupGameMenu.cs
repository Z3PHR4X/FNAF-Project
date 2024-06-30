using UnityEngine;

namespace Zephrax.FNAFGame.UserInterface
{
    public class SetupGameMenu : MonoBehaviour
    {
        public void StartGame()
        {
            Singleton.Instance.canRetryNight = false;
            Singleton.Instance.ChangeScene("LoadingScreen");
        }
    }
}