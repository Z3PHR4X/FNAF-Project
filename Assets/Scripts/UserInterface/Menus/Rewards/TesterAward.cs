using UnityEngine;

namespace Zephrax.FNAFGame.Menus.Rewards
{
    public class TesterAward : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            if (PlayerPrefs.GetInt("TesterAwardUnlocked") == 1)
            {
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            };
        }
    }
}