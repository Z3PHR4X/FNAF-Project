using UnityEngine;
using UnityEngine.UI;
using Zephrax.FNAFGame.Gameplay;

namespace Zephrax.FNAFGame.UserInterface.InGame
{
    public class DisplayPower : MonoBehaviour
    {
        [SerializeField] private Text powerText;
        int powerReserve;

        // Start is called before the first frame update
        void Start()
        {
            powerReserve = Player.Instance.powerManager.powerReserve;
            UpdatePowerDisplay(powerReserve);
        }

        // Update is called once per frame
        void Update()
        {
            if(powerReserve != Player.Instance.powerManager.powerReserve)
            {
                powerReserve = Player.Instance.powerManager.powerReserve;
                UpdatePowerDisplay(powerReserve);
            }
        }

        void UpdatePowerDisplay(int power)
        {
            float displayPower = (((float)(power))*10)/100;
            powerText.text = $"Power reserve: {displayPower}%";
        }
    }
}