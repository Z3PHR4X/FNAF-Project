using UnityEngine;

namespace Zephrax.FNAFGame.UserInterface.Menus
{
    public class VictoryButton : MonoBehaviour
    {
        public void ReturnToMenu()
        {
            if (Singleton.Instance.selectedNight == 7)
            {
                Singleton.Instance.ChangeScene("GameWin");
            }
            else
            {
                Singleton.Instance.ChangeScene("MainMenu");
            }
        }
    }
}
