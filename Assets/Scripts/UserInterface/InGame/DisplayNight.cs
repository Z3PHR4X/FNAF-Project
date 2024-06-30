using TMPro;
using UnityEngine;

namespace Zephrax.FNAFGame.UserInterface.InGame
{
    public class DisplayNight : MonoBehaviour
    {
        [SerializeField] private TMP_Text nightUi;
        [SerializeField] private bool isStartNightScreen = false;

        // Start is called before the first frame update
        void Start()
        {
            int _Night = Singleton.Instance.selectedNight;
            string nightText = "";
            if (isStartNightScreen)
            {
                switch (_Night)
                {
                    case 1:
                        nightText = "1st Night";
                        break;

                    case 2:
                        nightText = "2nd Night";
                        break;

                    case 3:
                        nightText = "3rd Night";
                        break;

                    default:
                        nightText = _Night + "th Night";
                        break;

                }
            }
            else
            {
                nightText = $"Night {_Night}";
            }

            nightUi.text = nightText;
        }
    }
}