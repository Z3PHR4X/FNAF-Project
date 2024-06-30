using UnityEngine;

namespace Zephrax.FNAFGame.UserInterface.Menus
{
    public class NewGameButton : MonoBehaviour
    {
        [SerializeField] GameObject newGameDialog;
        [SerializeField] MainMenuButtons mainMenuButtons;
        [SerializeField] GameObject menuButtons;

       public void StartNewGame()
        {
            if (Singleton.Instance.completedNight > 0)
            {
                menuButtons.SetActive(false);
                newGameDialog.SetActive(true);
            }
            else
            {
                mainMenuButtons.StartNewGame();
            }
        }
    }
}