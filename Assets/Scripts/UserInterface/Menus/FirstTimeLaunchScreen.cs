using UnityEngine;

namespace Zephrax.FNAFGame.UserInterface.Menus
{
    public class FirstTimeLaunchScreen : MonoBehaviour
    {

        private bool returningPlayer;

        // Start is called before the first frame update
        void Start()
        {
            returningPlayer = (PlayerPrefs.GetString("returningPlayer") == "true");
            gameObject.SetActive(!returningPlayer);
        }

        public void Acknowledge()
        {
            PlayerPrefs.SetString("returningPlayer", "true");
            gameObject.SetActive(false);
            PlayerPrefs.Save();
        }
    }
}