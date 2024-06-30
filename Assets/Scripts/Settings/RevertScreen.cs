using TMPro;
using UnityEngine;

namespace Zephrax.FNAFGame.Settings
{
    public class RevertScreen : MonoBehaviour
    {
        public int waitTime;
        [SerializeField] private TMP_Text countdownText;

        // Update is called once per frame
        void Update()
        {
            countdownText.text = $"{waitTime.ToString()} second(s)";
        }
    }
}