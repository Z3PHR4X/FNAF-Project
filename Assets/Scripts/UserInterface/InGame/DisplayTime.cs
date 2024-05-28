using UnityEngine;
using UnityEngine.UI;
using Zephrax.FNAFGame.Gameplay;

namespace Zephrax.FNAFGame.UserInterface.InGame
{
    public class DisplayTime : MonoBehaviour
    {
        [SerializeField] private Text hourText;
        private int hour,displayHour;

        // Start is called before the first frame update
        void Start()
        {
            hour = GameManagerV2.Instance.hour;
            UpdateHourDisplay(hour);
        }

        // Update is called once per frame
        void Update()
        {
            if (GameManagerV2.Instance.hour != hour)
            {
                hour = GameManagerV2.Instance.hour;
                UpdateHourDisplay(hour);
            }
        }

        void UpdateHourDisplay(int hour)
        {
            if (hour > 0) displayHour = hour;
            else displayHour = 12;
            hourText.text = $"{displayHour} AM";
        }
    }
}
